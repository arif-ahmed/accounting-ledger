using AccountingLedger.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace AccountingLedger.Application.Queries;
public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    public GetAllAccountsQueryHandler(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _accountRepository.GetAccountsViaSPAsync();
        return _mapper.Map<List<AccountDto>>(accounts);
    }
}
