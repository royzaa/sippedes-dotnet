using Microsoft.EntityFrameworkCore;
using sippedes.Cores.Entities;

namespace sippedes.Cores.Database;

public class AppDbContext : DbContext
{

    public DbSet<CivilData> CivilDatas => Set<CivilData>();
    public DbSet<Otp> Otps => Set<Otp>();
    public DbSet<UserCredential> UserCredentials => Set<UserCredential>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<AdminData> AdminDatas => Set<AdminData>();

    public DbSet<DeviceTokenNotification> DeviceTokenNotifications => Set<DeviceTokenNotification>();
    public DbSet<Letter> Letters => Set<Letter>();
    public DbSet<LetterCategory> LetterCategories => Set<LetterCategory>();
    public DbSet<TrackingStatus> TrackingStatus => Set<TrackingStatus>();
    
    public DbSet<Legalized> Legalizeds { get; set; }
    
    public DbSet<WitnessSignature> WitnessSignatures { get; set; }


    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
}