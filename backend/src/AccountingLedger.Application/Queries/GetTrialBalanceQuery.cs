using MediatR;

namespace AccountingLedger.Application.Queries;
public class GetTrialBalanceQuery : IRequest<List<TrialBalanceDto>> 
{
    public string? AccountNameFilter { get; set; }
    public string? SortBy { get; set; }       // "accountname", "debit", "credit"
    public string? SortOrder { get; set; }    // "asc", "desc"
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
