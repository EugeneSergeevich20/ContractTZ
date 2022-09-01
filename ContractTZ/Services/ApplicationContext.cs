using ContractTZ1.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractTZ.Services
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractStage> ContractStages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            Database.EnsureCreated();
        }

    }
}
