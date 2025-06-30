using AccountingLedger.Application.Interfaces;
using AccountingLedger.Domain.Entities;
using AccountingLedger.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedger.Infrastructure.Repositories;
public class JournalEntryRepository : Repository<JournalEntry>, IJournalEntryRepository
{
    public JournalEntryRepository(LedgerDbContext context) : base(context)
    {
    }

    public async Task<int> AddJournalEntryAsync(JournalEntry entry, List<JournalEntryLine> lines)
    {
        var idParam = new SqlParameter("@JournalEntryId", System.Data.SqlDbType.Int)
        {
            Direction = System.Data.ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC usp_AddJournalEntry @Date = {0}, @Description = {1}, @JournalEntryId = @JournalEntryId OUT",
            entry.Date, entry.Description, idParam);

        int journalEntryId = (int)idParam.Value;

        foreach (var line in lines)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC usp_AddJournalEntryLine @JournalEntryId = {0}, @AccountId = {1}, @Debit = {2}, @Credit = {3}",
                journalEntryId, line.AccountId, line.Debit, line.Credit);
        }

        return journalEntryId;
    }

    public async Task<List<JournalEntry>> GetJournalEntriesAsync()
    {
        return await _context.JournalEntries
            .Include(j => j.Lines)
            .ThenInclude(l => l.Account)
            .ToListAsync();
    }
}
