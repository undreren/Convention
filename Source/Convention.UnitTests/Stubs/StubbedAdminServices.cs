using Convention.WebApi.Api.Areas.Administration;
using Convention.WebApi.Api.Areas.Administration.Dtos;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Convention.UnitTests.Stubs
{
    public class StubbedAdminServices : IAdminServices
    {
        public Func<CreateAvenueDto, Task<AvenueDto>> CreateAvenueFunc { get; set; }
            = createDto => throw new AssertionException("CreateAvenueAsync was not configured on stub");

        public async Task<AvenueDto> CreateAvenue(CreateAvenueDto createDto)
        {
            return await CreateAvenueFunc(createDto);
        }
    }
}
