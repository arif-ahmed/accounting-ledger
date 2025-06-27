namespace AccountingLedger.Application.Queries;
public class TrialBalanceDto
{
    public string AccountName { get; set; } = string.Empty;
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
}