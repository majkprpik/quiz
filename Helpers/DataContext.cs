using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        // if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        // {
        //     options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        // }
        // else
        // {
        //     options.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_WebApiDatabase"));
        // }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { }
}