namespace IF3250_2022_24_APPTS_Backend.Data;
using Microsoft.EntityFrameworkCore;
using IF3250_2022_24_APPTS_Backend.Entities;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<User> user { get; set; }
    public DbSet<JobOpening> job_opening { get; set; }
    public DbSet<JobApplication> job_application { get; set; }
}