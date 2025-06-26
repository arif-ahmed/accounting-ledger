using AccountingLedger.Application.Queries;
using AccountingLedger.Domain.Entities;

namespace AccountingLedger.Application.Interfaces;

public interface IJournalEntryRepository : IRepository<JournalEntry>
{
    Task<int> AddJournalEntryAsync(JournalEntry entry, List<JournalEntryLine> lines);
    Task<List<JournalEntryDto>> GetJournalEntriesAsync();
}
