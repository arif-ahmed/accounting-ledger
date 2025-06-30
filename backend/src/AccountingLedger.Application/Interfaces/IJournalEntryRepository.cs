using AccountingLedger.Domain.Entities;

namespace AccountingLedger.Application.Interfaces;

public interface IJournalEntryRepository : IRepository<JournalEntry>
{
    Task<int> AddJournalEntryAsync(JournalEntry entry, List<JournalEntryLine> lines);
    Task<List<JournalEntry>> GetJournalEntriesAsync();
}
