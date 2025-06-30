using AccountingLedger.Application.Interfaces;
using AccountingLedger.Domain.Entities;
using AccountingLedger.Domain.Entities.Enums;
using MediatR;

namespace AccountingLedger.Application.Commands;

public class CreateAccountCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
{
    private readonly IAccountRepository _accountRepository;
    public CreateAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse<AccountType>(request.Type, true, out var accountType))
        {
            throw new ArgumentException($"Invalid account type: {request.Type}");
        }

        var account = new Account
        {
            Name = request.Name,
            Type = accountType
        };

        var id = await _accountRepository.AddAccountViaSPAsync(account);

        return id;
    }
}
