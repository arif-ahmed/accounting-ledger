using MediatR;

namespace AccountingLedger.Application.Queries;
public class GetTrialBalanceQuery : IRequest<List<TrialBalanceDto>> { }
