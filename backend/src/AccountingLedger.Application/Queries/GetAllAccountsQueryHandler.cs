using MediatR;


namespace AccountingLedger.Application.Queries;

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    public GetAllAccountsQueryHandler()
    {

    }
    public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new List<AccountDto>()); // Placeholder for actual implementation
    }
}
