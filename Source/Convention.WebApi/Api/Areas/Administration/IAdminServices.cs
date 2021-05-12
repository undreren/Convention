using Convention.WebApi.Api.Areas.Administration.Dtos;
using System.Threading.Tasks;

namespace Convention.WebApi.Api.Areas.Administration
{
    public interface IAdminServices
    {
        Task<AvenueDto> CreateAvenue(CreateAvenueDto createDto);
    }
}
