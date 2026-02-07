using Microsoft.EntityFrameworkCore;
using Models.Entities;
namespace Persistance
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AccountUser> AccountUsers { get; set; }
    }
}
