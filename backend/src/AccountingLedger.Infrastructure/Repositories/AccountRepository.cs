using AccountingLedger.Application.Interfaces;
using AccountingLedger.Application.Queries;
using AccountingLedger.Domain.Entities;
using AccountingLedger.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedger.Infrastructure.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    public AccountRepository(LedgerDbContext context) : base(context)
    {

    }
    public async Task<List<AccountDto>> GetAccountsViaSPAsync()
    {
        var accounts = await _context.Accounts.FromSqlRaw("EXEC usp_GetAccounts").ToListAsync();
        return accounts.Select(a => new AccountDto { Id = a.Id, Name = a.Name, Type = a.Type.ToString()  }).ToList();

    }
    public async Task<int> AddAccountViaSPAsync(Account account)
    {
        var idParam = new SqlParameter("@Id", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
        await _context.Database.ExecuteSqlRawAsync("EXEC usp_AddAccount @Name, @Type, @Id OUT",
            new SqlParameter("@Name", account.Name),
            new SqlParameter("@Type", account.Type),
            idParam);
        return (int)idParam.Value;
    }
}
