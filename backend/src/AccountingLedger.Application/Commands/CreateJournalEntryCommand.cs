using AccountingLedger.Application.Interfaces;
using AccountingLedger.Application.Queries;
using MediatR;

namespace AccountingLedger.Application.Commands;
public class CreateJournalEntryCommand : IRequest<int>
{
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public List<JournalEntryLineDto> Lines { get; set; } = new();
}

public class CreateJournalEntryHandler : IRequestHandler<CreateJournalEntryCommand, int>
{
    private readonly IJournalEntryRepository _repository;

    public CreateJournalEntryHandler(IJournalEntryRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<int> Handle(CreateJournalEntryCommand request, CancellationToken cancellationToken)
    {
        int journalEntryId = await _repository.AddJournalEntryAsync(new Domain.Entities.JournalEntry
        {
            Date = request.Date,
            Description = request.Description
        }, request.Lines.Select(line => new Domain.Entities.JournalEntryLine
        {
            AccountId = line.AccountId,
            Debit = line.Debit,
            Credit = line.Credit
        }).ToList());

        return journalEntryId;
    }
}