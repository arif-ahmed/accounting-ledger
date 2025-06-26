using AccountingLedger.Application.Interfaces;
using AccountingLedger.Application.Queries;
using AccountingLedger.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AccountingLedger.Infrastructure.Repositories;

public class TrialBalanceRepository : ITrialBalanceRepository
{
    private readonly LedgerDbContext _context;

    public TrialBalanceRepository(LedgerDbContext context)
    {
        _context = context;
    }

    public async Task<List<TrialBalanceDto>> GetTrialBalanceAsync(
        string? accountNameFilter = null,
        string? sortBy = null,
        string? sortOrder = "ASC",
        int pageNumber = 1,
        int pageSize = 10)
    {
        var results = new List<TrialBalanceDto>();

        using var conn = _context.Database.GetDbConnection();
        await conn.OpenAsync();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = "usp_GetTrialBalance";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@AccountNameFilter", accountNameFilter ?? (object)DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@SortBy", sortBy ?? (object)DBNull.Value));
        cmd.Parameters.Add(new SqlParameter("@SortOrder", sortOrder ?? "ASC"));
        cmd.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
        cmd.Parameters.Add(new SqlParameter("@PageSize", pageSize));

        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            results.Add(new TrialBalanceDto
            {
                AccountName = reader["AccountName"].ToString() ?? string.Empty,
                Debit = Convert.ToDecimal(reader["Debit"]),
                Credit = Convert.ToDecimal(reader["Credit"]),
            });
        }

        await conn.CloseAsync();
        return results;
    }


}

[Keyless]
public class TrialBalanceResult
{
    public string AccountName { get; set; } = string.Empty;
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
}

