using AccountingLedger.Application.Interfaces;
using AccountingLedger.Application.Queries;
using AccountingLedger.Domain.Entities;
using AccountingLedger.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

    public async Task<List<JournalEntryDto>> GetJournalEntriesAsync()
    {
        var result = new List<JournalEntryDto>();

        using var conn = _context.Database.GetDbConnection();
        await conn.OpenAsync();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = "usp_GetJournalEntries";
        cmd.CommandType = CommandType.StoredProcedure;

        using var reader = await cmd.ExecuteReaderAsync();

        Dictionary<int, JournalEntryDto> journalMap = new();

        while (await reader.ReadAsync())
        {
            int journalId = Convert.ToInt32(reader["JournalEntryId"]);

            if (!journalMap.TryGetValue(journalId, out var journal))
            {
                journal = new JournalEntryDto
                {
                    Id = journalId,
                    Date = Convert.ToDateTime(reader["Date"]),
                    Description = reader["Description"].ToString() ?? "",
                    Lines = new List<JournalEntryLineDto>()
                };
                journalMap[journalId] = journal;
            }

            journal.Lines.Add(new JournalEntryLineDto
            {
                AccountId = Convert.ToInt32(reader["AccountId"]),
                AccountName = reader["AccountName"].ToString() ?? "",
                Debit = Convert.ToDecimal(reader["Debit"]),
                Credit = Convert.ToDecimal(reader["Credit"])
            });
        }

        result = journalMap.Values.ToList();
        return result;
    }
}
