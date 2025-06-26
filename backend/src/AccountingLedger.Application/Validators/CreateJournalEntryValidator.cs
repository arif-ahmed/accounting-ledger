
using AccountingLedger.Application.Commands;
using FluentValidation;

namespace AccountingLedger.Application.Validators;

public class CreateJournalEntryValidator : AbstractValidator<CreateJournalEntryCommand>
{
    public CreateJournalEntryValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(255);

        RuleFor(x => x.Lines)
            .NotEmpty().WithMessage("At least one journal line is required.")
            .Must(lines => lines.Count >= 2).WithMessage("At least two lines required (debit and credit).");

        RuleForEach(x => x.Lines).ChildRules(line =>
        {
            line.RuleFor(l => l.AccountId).GreaterThan(0);
            line.RuleFor(l => l.Debit).GreaterThanOrEqualTo(0);
            line.RuleFor(l => l.Credit).GreaterThanOrEqualTo(0);
            line.RuleFor(l => l)
                .Must(l => l.Debit == 0 || l.Credit == 0)
                .WithMessage("Only one of debit or credit can be non-zero per line.");
        });

        RuleFor(x => x)
            .Must(command =>
            {
                var debitSum = command.Lines.Sum(l => l.Debit);
                var creditSum = command.Lines.Sum(l => l.Credit);
                return debitSum == creditSum;
            })
            .WithMessage("Total Debit must equal Total Credit.");

    }
}
