

using AccountingLedger.Domain.Entities;

namespace AccountingLedger.Application.Interfaces;

public interface IAccountRepository : IRepository<Account>
{
    Task<List<Account>> GetAccountsViaSPAsync();
    Task<int> AddAccountViaSPAsync(Account account);
}
