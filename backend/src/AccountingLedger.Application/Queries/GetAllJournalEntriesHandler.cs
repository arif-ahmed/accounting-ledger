using AccountingLedger.Application.Interfaces;
using MediatR;

namespace AccountingLedger.Application.Queries;

public class GetAllJournalEntriesHandler : IRequestHandler<GetAllJournalEntriesQuery, List<JournalEntryDto>>
{
    private readonly IJournalEntryRepository _journalEntryRepository;
    public GetAllJournalEntriesHandler(IJournalEntryRepository journalEntryRepository)
    {
        _journalEntryRepository = journalEntryRepository;
    }

    public async Task<List<JournalEntryDto>> Handle(GetAllJournalEntriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _journalEntryRepository.GetJournalEntriesAsync();
        return result; 
    }
}
