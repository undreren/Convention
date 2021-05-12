using Convention.WebApi.Api.Areas.Administration;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Convention.UnitTests.Stubs;
using Microsoft.AspNetCore.Http;
using Convention.WebApi.Api.Areas.Administration.Dtos;

namespace Convention.UnitTests.ApiControllers.Areas.Administration
{
    [TestFixture]
    public class AvenueControllerTests
    {
        private StubbedAdminServices adminServices;
        private AvenueController controller;

        [SetUp]
        public void SetUp()
        {
            adminServices = new StubbedAdminServices();
            controller = new AvenueController(adminServices);
        }

        [Test]
        public async Task PostAvenueAsync_creates_avenue_through_AdminServices()
        {
            // Given
            var createDto = new CreateAvenueDto { Name = "A Brand New Avenue" };
            var expectedId = Guid.NewGuid();
            adminServices.CreateAvenueFunc = (name) =>
            {
                return Task.FromResult(new AvenueDto(expectedId, createDto.Name));
            };

            // When
            var response = await controller.PostAvenueAsync(createDto);

            // Then
            Assert.Multiple(() =>
            {
                Assert.AreEqual(StatusCodes.Status201Created, response?.StatusCode, "Unexpected status code");
                Assert.IsInstanceOf<AvenueDto>(response?.Value);
                var dto = (AvenueDto)response.Value;
                Assert.AreEqual(expectedId, dto.Id, "Did not return expected id of new Avenue");
                Assert.AreEqual(createDto.Name, dto.Name, "Name of returned dto does not match input");
            });
        }
    }
}
