using AccountingLedger.Application.Interfaces;
using AccountingLedger.Application.Queries;
using AccountingLedger.Infrastructure.Data;

namespace AccountingLedger.Infrastructure.Repositories;

public class TrialBalanceRepository : ITrialBalanceRepository
{
    private readonly LedgerDbContext _context;

    public TrialBalanceRepository(LedgerDbContext context)
    {
        _context = context;
    }

    public Task<List<TrialBalanceDto>> GetTrialBalanceAsync()
    {
        // Placeholder for actual implementation
        return Task.FromResult(new List<TrialBalanceDto>());
    }
}
