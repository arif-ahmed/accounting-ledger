using AccountingLedger.Application.Queries;

namespace AccountingLedger.Application.Interfaces;

public interface ITrialBalanceRepository
{
    Task<List<TrialBalanceDto>> GetTrialBalanceAsync(
        string? accountNameFilter = null,
        string? sortBy = null,
        string? sortOrder = "ASC",
        int pageNumber = 1,
        int pageSize = 10);
}
