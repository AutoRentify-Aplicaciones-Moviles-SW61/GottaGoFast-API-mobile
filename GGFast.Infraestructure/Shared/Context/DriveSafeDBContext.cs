using GottaGoFast.Domain.Publishing.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GottaGoFast.Infraestructure.Shared.Context;

public class DriveSafeDBContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DriveSafeDBContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public DriveSafeDBContext(DbContextOptions<DriveSafeDBContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    public DbSet<User> Users { get; set; }
    
    
    public DbSet<Rent> Rents { get; set; }
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql(_configuration["ConnectionStrings:DriveSafeDB"],
                serverVersion);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<User>().ToTable("Users");
        
        builder.Entity<Rent>().ToTable("Rents");
        
    }
}
