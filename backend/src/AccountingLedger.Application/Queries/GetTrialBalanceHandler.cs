using AccountingLedger.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedger.Application.Queries;

public class GetTrialBalanceHandler : IRequestHandler<GetTrialBalanceQuery, List<TrialBalanceDto>>
{
    private readonly LedgerDbContext _context;

    public GetTrialBalanceHandler(LedgerDbContext context)
    {
        _context = context;
    }

    public async Task<List<TrialBalanceDto>> Handle(GetTrialBalanceQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.JournalEntryLines.Join(
            _context.Accounts,
            line => line.JournalEntryId,
            account => account.Id,
            (line, account) => new { line, account }
        )
        .GroupBy(grp => grp.account.Id)
        .Select(grp => new TrialBalanceDto
        {
            AccountName = grp.Key.ToString(),
            Debit = grp.Sum(x => x.line.Debit),
            Credit = grp.Sum(x => x.line.Credit)
        }).ToListAsync();

        return result;
    }
}
