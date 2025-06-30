using AccountingLedger.Application.Queries;

namespace AccountingLedger.Application.Interfaces;

public interface ITrialBalanceRepository
{
    Task<List<TrialBalanceDto>> GetTrialBalanceAsync();
}
