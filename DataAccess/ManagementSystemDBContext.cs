using DataAccess.Task;
using DataAccess.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ManagementSystemDBContext : IdentityDbContext<Users>
    {
        public ManagementSystemDBContext()
        {
        }
        public ManagementSystemDBContext(DbContextOptions<ManagementSystemDBContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ManagmentStstemDB;TrustServerCertificate=True;Integrated Security=true");
            }

            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Tasks> task { get; set; }
        public virtual DbSet<Users> user { get; set; }

    }
}
