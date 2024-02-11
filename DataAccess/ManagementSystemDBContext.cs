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

        public virtual DbSet<Tasks> task { get; set; }
        public virtual DbSet<Users> user { get; set; }

    }
}
