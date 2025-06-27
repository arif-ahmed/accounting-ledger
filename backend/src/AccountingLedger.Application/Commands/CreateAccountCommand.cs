using AccountingLedger.Infrastructure.Data;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedger.Application.Commands;

public class CreateAccountCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
{
    private readonly LedgerDbContext _context;

    public CreateAccountCommandHandler(LedgerDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var idParam = new SqlParameter("@Id", System.Data.SqlDbType.Int)
        {
            Direction = System.Data.ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC usp_AddAccount @Name = {0}, @Type = {1}, @Id = @Id OUT",
            request.Name,
            request.Type,
            idParam
        );

        // Simulate account creation logic
        // Console.WriteLine($"Creating account: {request.Name} of type {request.Type}");
        return (int)idParam.Value; // Simulate returning a new account ID
    }
}
