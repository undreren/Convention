using Convention.UnitTests.Stubs;
using Convention.WebApi.Api.Areas.Administration.Dtos;
using Convention.WebApi.Services;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Convention.UnitTests.Services
{
    [TestFixture]
    public class DomainAdminServicesTests
    {
        private StubbedAvenueRepository stubbedRepository;
        private DomainAdminServices services;

        [SetUp]
        public void SetUp()
        {
            stubbedRepository = new();
            services = new(stubbedRepository);
        }

        [Test]
        public async Task Create_creates_avenue_through_repository()
        {
            // Given
            var createDto = new CreateAvenueDto { Name = "The Ritz" };
            var expectedId = Guid.NewGuid();

            stubbedRepository.CreateAvenueFunc = _ =>
            {
                return Task.FromResult(expectedId);
            };

            // When
            var avenueDto = await services.CreateAvenue(createDto);

            // Then
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedId, avenueDto?.Id, "Unexpected id on returned dto");
                Assert.AreEqual(createDto.Name, avenueDto?.Name, "Unexpected Name on returned dto");
            });
        }
    }
}
