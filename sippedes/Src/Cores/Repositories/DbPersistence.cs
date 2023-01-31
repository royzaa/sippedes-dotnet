using livecode_net_advanced.Cores.Database;
using Microsoft.EntityFrameworkCore;

namespace livecode_net_advanced.Cores.Repositories;

public class DbPersistence : IPersistence
{
    private readonly AppDbContext _context;

    public DbPersistence(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();

    }

    public async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func)
    {
        var strategy = _context.Database.CreateExecutionStrategy();
        var result = await strategy.ExecuteAsync(async () =>
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await func();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw;
            }
        });
        
        return result;
    }
}