using AccountingLedger.Domain.Entities.Enums;
using AccountingLedger.Domain.Entities;
using AccountingLedger.Infrastructure.Data;

namespace AccountingLedger.API.Utils;

public static class DbInitializer
{
    public static void Initialize(LedgerDbContext context)
    {
        if (!context.Accounts.Any())
        {
            var accounts = new List<Account>
            {
                new Account { Name = "Cash", Type = AccountType.Asset },
                new Account { Name = "Accounts Payable", Type = AccountType.Liability },
                new Account { Name = "Owner's Equity", Type = AccountType.Equity },
                new Account { Name = "Service Revenue", Type = AccountType.Revenue },
                new Account { Name = "Rent Expense", Type = AccountType.Expense },
            };

            context.Accounts.AddRange(accounts);
        }

        if (!context.JournalEntries.Any())
        {
            var journalEntries = new List<JournalEntry>
            {
                new JournalEntry
                {
                    Date = new DateTime(2025, 6, 1),
                    Description = "Initial Capital Investment",
                    Lines = new List<JournalEntryLine>
                    {
                        new JournalEntryLine {  JournalEntryId = 1, AccountId = 1, Debit = 10000m, Credit = 0m }, // Cash
                        new JournalEntryLine { JournalEntryId = 1, AccountId = 3, Debit = 0m, Credit = 10000m }  // Owner's Equity
                    }
                },
                new JournalEntry
                {
                    Date = new DateTime(2025, 6, 5),
                    Description = "Office Rent Payment",
                    Lines = new List<JournalEntryLine>
                    {
                        new JournalEntryLine { JournalEntryId = 2, AccountId = 5, Debit = 1200m, Credit = 0m }, // Rent Expense
                        new JournalEntryLine { JournalEntryId = 2, AccountId = 1, Debit = 0m, Credit = 1200m }  // Cash
                    }
                },
                new JournalEntry
                {
                    Date = new DateTime(2025, 6, 10),
                    Description = "Service Income",
                    Lines = new List<JournalEntryLine>
                    {
                        new JournalEntryLine { JournalEntryId = 3, AccountId = 1, Debit = 3000m, Credit = 0m }, // Cash
                        new JournalEntryLine { JournalEntryId = 3, AccountId = 4, Debit = 0m, Credit = 3000m }  // Service Revenue
                    }
                }
            };

            context.JournalEntries.AddRange(journalEntries);
        }

        context.SaveChanges();
    }
}
