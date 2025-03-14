using Microsoft.EntityFrameworkCore;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace SurfSessions;


public class AppDbContext : DbContext
{
    
    private static string? _connectionPassword = ConfigurationManager.AppSettings.Get("SQLServerPassword");
    private static string _connectionString = $"Server=Localhost\\SQLEXPRESS; Database=master; Trusted_Connection=True; User id =sa; Password={_connectionPassword}; TrustServerCertificate=true; MultipleActiveResultSets=true";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }

}