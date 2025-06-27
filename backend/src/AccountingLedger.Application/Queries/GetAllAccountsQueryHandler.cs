
using AccountingLedger.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedger.Application.Queries;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    private readonly LedgerDbContext _context;
    public GetAllAccountsQueryHandler(LedgerDbContext context)
    {
        _context = context;
    }
    public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        // var accounts = await _accountRepository.GetAllAsync(cancellationToken);
        //return accounts.Select(a => new AccountDto
        //{
        //    Id = a.Id,
        //    Name = a.Name,
        //    Type = a.Type
        //}).ToList();

        var accounts = await _context.Accounts.ToListAsync(); // Placeholder for actual query handling
        var result = accounts.Select(acc => new AccountDto 
        {
            Id = acc.Id,
            Name = acc.Name,
            Type = acc.Type.ToString()
        }).ToList();

        return result;
    }
}
