using Convention.WebApi.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Convention.WebApi.DataAccess
{
    public class ConventionDbContext : DbContext
    {
        public DbSet<Avenue> Avenues { get; set; }

        public ConventionDbContext(DbContextOptions<ConventionDbContext> contextOptions)
            : base(contextOptions)
        {
            
        }
    }
}
