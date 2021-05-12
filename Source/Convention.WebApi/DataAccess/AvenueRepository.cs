using Convention.WebApi.DataAccess.Models;
using Convention.WebApi.Services;
using System;
using System.Threading.Tasks;

namespace Convention.WebApi.DataAccess
{
    public class AvenueRepository : IAvenueRepository
    {
        private ConventionDbContext dbContext;

        public AvenueRepository(ConventionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Guid> CreateAvenue(string name)
        {
            var entry = await dbContext.Avenues.AddAsync(new Avenue { Name = name });
            dbContext.SaveChanges();
            return entry.Entity.Id;
        }
    }
}
