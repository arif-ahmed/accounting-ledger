using MediatR;


namespace AccountingLedger.Application.Queries;

public class GetTrialBalanceHandler : IRequestHandler<GetTrialBalanceQuery, List<TrialBalanceDto>>
{
    public GetTrialBalanceHandler()
    {

    }

    public async Task<List<TrialBalanceDto>> Handle(GetTrialBalanceQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new List<TrialBalanceDto>()); // Placeholder for actual implementation
    }
}
