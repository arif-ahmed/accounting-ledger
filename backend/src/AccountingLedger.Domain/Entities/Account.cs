using AccountingLedger.Domain.Entities.Enums;

namespace AccountingLedger.Domain.Entities;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public AccountType Type { get; set; } 
}
