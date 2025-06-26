using AccountingLedger.Application.Commands;
using FluentValidation;

namespace AccountingLedger.Application.Validators;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    private static readonly string[] AllowedTypes = new[]
    {
        "Asset", "Liability", "Equity", "Revenue", "Expense"
    };

    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Account name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Account type is required.")
            .Must(type => AllowedTypes
                .Any(t => string.Equals(t, type, StringComparison.OrdinalIgnoreCase)))
            .WithMessage($"Account type must be one of: {string.Join(", ", AllowedTypes)}");
    }
}

