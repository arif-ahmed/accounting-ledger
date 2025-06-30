using AccountingLedger.Application.Interfaces;
using AccountingLedger.Domain.Entities;
using AccountingLedger.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedger.Infrastructure.Repositories;

public class AccountRepository : Repository<Account>, IAccountRepository
{
    private readonly LedgerDbContext _context;
    public AccountRepository(LedgerDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<List<Account>> GetAccountsViaSPAsync()
    {
        return await _context.Accounts.FromSqlRaw("EXEC GetAccounts").ToListAsync();
    }
    public async Task<int> AddAccountViaSPAsync(Account account)
    {
        var idParam = new SqlParameter("@Id", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
        await _context.Database.ExecuteSqlRawAsync("EXEC AddAccount @Name, @Description, @Id OUT",
            new SqlParameter("@Name", account.Name),
            new SqlParameter("@Description", account.Type),
            idParam);
        return (int)idParam.Value;
    }
}
