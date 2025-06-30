using AccountingLedger.Application.Interfaces;
using MediatR;


namespace AccountingLedger.Application.Queries;

public class GetTrialBalanceHandler : IRequestHandler<GetTrialBalanceQuery, List<TrialBalanceDto>>
{
    private readonly ITrialBalanceRepository _trialBalanceRepository;
    public GetTrialBalanceHandler(ITrialBalanceRepository trialBalanceRepository)
    {
        _trialBalanceRepository = trialBalanceRepository;
    }

    public async Task<List<TrialBalanceDto>> Handle(GetTrialBalanceQuery request, CancellationToken cancellationToken)
    {
        var result = await _trialBalanceRepository.GetTrialBalanceAsync();
        return result;
    }
}
