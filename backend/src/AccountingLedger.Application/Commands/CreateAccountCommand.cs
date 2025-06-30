
using MediatR;


namespace AccountingLedger.Application.Commands;

public class CreateAccountCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
{
    public CreateAccountCommandHandler()
    {

    }

    public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(0); // Placeholder for actual implementation
    }
}
