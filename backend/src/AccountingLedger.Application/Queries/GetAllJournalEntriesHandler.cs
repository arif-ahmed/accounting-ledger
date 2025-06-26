using AccountingLedger.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AccountingLedger.Application.Queries;

public class GetAllJournalEntriesHandler : IRequestHandler<GetAllJournalEntriesQuery, List<JournalEntryDto>>
{
    private readonly IJournalEntryRepository _journalEntryRepository;
    private readonly IMapper _mapper;
    public GetAllJournalEntriesHandler(IJournalEntryRepository journalEntryRepository, IMapper mapper)
    {
        _journalEntryRepository = journalEntryRepository;
        _mapper = mapper;
    }

    public async Task<List<JournalEntryDto>> Handle(GetAllJournalEntriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _journalEntryRepository.GetJournalEntriesAsync();
        return _mapper.Map<List<JournalEntryDto>>(result); 
    }
}
