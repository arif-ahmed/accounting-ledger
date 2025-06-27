namespace AccountingLedger.Domain.Entities;

public class JournalEntryLine
{
    public int Id { get; set; }
    public int JournalEntryId { get; set; }
    public int AccountId { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }

    public virtual JournalEntry? JournalEntry { get; set; }
    public virtual Account? Account { get; set; } 
}