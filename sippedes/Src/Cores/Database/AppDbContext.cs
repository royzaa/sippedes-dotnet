using Microsoft.EntityFrameworkCore;

namespace sippedes.Cores.Database;

public class AppDbContext : DbContext
{


    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}