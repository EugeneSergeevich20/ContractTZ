using ContractTZ1.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractTZ.Models
{
    public class ApplicationContext : DbContext
    { 

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractStage> ContractStages { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
