using AccountingLedger.Application.Interfaces;
using AccountingLedger.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingLedger.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly LedgerDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(LedgerDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<int> AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();

        var prop = typeof(T).GetProperty("Id");
        return prop != null ? (int)(prop.GetValue(entity) ?? 0) : 0;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(object id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
