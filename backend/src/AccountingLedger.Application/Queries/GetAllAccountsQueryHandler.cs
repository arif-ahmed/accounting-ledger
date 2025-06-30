using AccountingLedger.Application.Interfaces;
using MediatR;

namespace AccountingLedger.Application.Queries;
public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    private readonly IAccountRepository _accountRepository;
    public GetAllAccountsQueryHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }
    public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        await _accountRepository.GetAccountsViaSPAsync();
        return new List<AccountDto>();
    }
}
