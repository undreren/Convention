using Convention.WebApi.DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Convention.UnitTests.DataAccess
{
    [TestFixture]
    public class AvenueRepositoryTests
    {
        private DbConnection connection;
        private ConventionDbContext dbContext;
        private AvenueRepository repository;

        [SetUp]
        public void SetUp()
        {
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ConventionDbContext>()
                .UseSqlite(connection)
                .Options;

            dbContext = new ConventionDbContext(options);
            dbContext.Database.EnsureCreated();
            repository = new AvenueRepository(dbContext);
        }

        [Test]
        public async Task Create_adds_avenue_to_database()
        {
            // Given
            var avenueName = "The Malicius Mallard Pub";

            // When
            var actualId = await repository.CreateAvenue(avenueName);

            // Then
            var avenues = await dbContext.Avenues.ToListAsync();

            Assert.AreEqual(1, avenues.Count, "More than one avenue was added to database");
            Assert.Multiple(() =>
            {
                var avenue = avenues.Single();
                Assert.AreEqual(avenue.Id, actualId, "Wrong id returned");
                Assert.AreEqual(avenue.Name, avenueName, "The avenue was created with wrong name");
            });
        }
    }
}
