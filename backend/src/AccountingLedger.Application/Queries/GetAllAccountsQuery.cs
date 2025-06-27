using MediatR;

namespace AccountingLedger.Application.Queries;

public class GetAllAccountsQuery : IRequest<List<AccountDto>> { }