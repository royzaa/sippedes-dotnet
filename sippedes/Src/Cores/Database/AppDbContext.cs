using Microsoft.EntityFrameworkCore;
using sippedes.Cores.Entities;

namespace sippedes.Cores.Database;

public class AppDbContext : DbContext
{

    public DbSet<CivilData> CivilDatas => Set<CivilData>();

    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}