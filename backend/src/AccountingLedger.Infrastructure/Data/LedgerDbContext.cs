using AccountingLedger.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedger.Infrastructure.Data;
public class LedgerDbContext : DbContext
{
    public LedgerDbContext(DbContextOptions<LedgerDbContext> options) : base(options)
    {
    }
    public DbSet<JournalEntry> JournalEntries { get; set; }
    public DbSet<JournalEntryLine> JournalEntryLines { get; set; }
    public DbSet<Account> Accounts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JournalEntry>()
            .HasMany(e => e.Lines)
            .WithOne()
            .HasForeignKey(l => l.JournalEntryId);

        modelBuilder.Entity<Account>()
            .Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}
