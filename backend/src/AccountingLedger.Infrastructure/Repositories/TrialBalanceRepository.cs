using AccountingLedger.Application.Interfaces;
using AccountingLedger.Application.Queries;
using AccountingLedger.Infrastructure.Data;
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

    public async Task<List<TrialBalanceDto>> GetTrialBalanceAsync()
    {
        var results = new List<TrialBalanceDto>();

        using var conn = _context.Database.GetDbConnection();
        await conn.OpenAsync();

        using var cmd = conn.CreateCommand();
        cmd.CommandText = "usp_GetTrialBalance";
        cmd.CommandType = CommandType.StoredProcedure;

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new TrialBalanceDto
            {
                AccountName = reader["AccountName"].ToString() ?? string.Empty,
                Debit = reader["Debit"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Debit"]),
                Credit = reader["Credit"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Credit"]),
            });
        }

        // Placeholder for actual implementation
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

