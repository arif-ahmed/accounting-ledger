using MediatR;

namespace AccountingLedger.Application.Queries;

public class GetAllJournalEntriesHandler : IRequestHandler<GetAllJournalEntriesQuery, List<JournalEntryDto>>
{
    public GetAllJournalEntriesHandler()
    {

    }

    public async Task<List<JournalEntryDto>> Handle(GetAllJournalEntriesQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new List<JournalEntryDto>()); // Placeholder for actual implementation
    }
}
