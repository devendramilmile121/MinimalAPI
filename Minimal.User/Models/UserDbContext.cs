using Microsoft.EntityFrameworkCore;

namespace Minimal.User.Models.Entity;
public class UserDbContext: DbContext
{
    public UserDbContext()
    {
        // required for not throwing exception of design pattern
    }
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("AppDb");
        optionsBuilder.UseSqlServer(connectionString);
    }
}

