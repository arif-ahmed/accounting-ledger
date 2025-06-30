using MediatR;

namespace AccountingLedger.Application.Queries;

public class GetAllJournalEntriesQuery : IRequest<List<JournalEntryDto>> { }
