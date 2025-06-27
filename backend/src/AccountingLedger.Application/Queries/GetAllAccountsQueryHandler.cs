
using MediatR;

namespace AccountingLedger.Application.Queries;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    // private readonly IAccountRepository _accountRepository;
    public GetAllAccountsQueryHandler()
    {
        //_accountRepository = accountRepository;
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
        return new();
    }
}
