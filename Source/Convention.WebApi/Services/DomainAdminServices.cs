using Convention.WebApi.Api.Areas.Administration;
using Convention.WebApi.Api.Areas.Administration.Dtos;
using System;
using System.Threading.Tasks;

namespace Convention.WebApi.Services
{
    public class DomainAdminServices : IAdminServices
    {
        private IAvenueRepository repository;

        public DomainAdminServices(IAvenueRepository repository)
        {
            this.repository = repository;
        }

        public async Task<AvenueDto> CreateAvenue(CreateAvenueDto createDto)
        {
            var id = await repository.CreateAvenue(createDto.Name);
            return new AvenueDto(id, createDto.Name);
        }
    }
}
