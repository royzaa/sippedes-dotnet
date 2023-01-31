using livecode_net_advanced.Cores.Entities;
using Microsoft.EntityFrameworkCore;

namespace livecode_net_advanced.Cores.Database;

public class AppDbContext : DbContext
{


    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}