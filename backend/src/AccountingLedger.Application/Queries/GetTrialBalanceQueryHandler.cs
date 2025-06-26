using AccountingLedger.Application.Interfaces;
using MediatR;


namespace AccountingLedger.Application.Queries;

public class GetTrialBalanceQueryHandler : IRequestHandler<GetTrialBalanceQuery, List<TrialBalanceDto>>
{
    private readonly ITrialBalanceRepository _repository;

    public GetTrialBalanceQueryHandler(ITrialBalanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TrialBalanceDto>> Handle(GetTrialBalanceQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetTrialBalanceAsync(
            request.AccountNameFilter,
            request.SortBy,
            request.SortOrder,
            request.PageNumber,
            request.PageSize
        );
    }
}
