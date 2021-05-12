using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Convention.WebApi.Api.Areas.Administration.Dtos;

namespace Convention.WebApi.Api.Areas.Administration
{
    [ApiController, Area("admin"), Route("[area]")]
    [Authorize, EnableCors("AllowAdminPanel")]
    public class AvenueController : ControllerBase
    {
        private IAdminServices adminServices;

        public AvenueController(IAdminServices adminServices)
        {
            this.adminServices = adminServices;
        }

        [Authorize("create:avenues")]
        [HttpPost, Route("avenues")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AvenueDto))]
        public async Task<ObjectResult> PostAvenueAsync(CreateAvenueDto createDto)
        {
            var avenueDto = await adminServices.CreateAvenue(createDto);
            return StatusCode(StatusCodes.Status201Created, avenueDto);
        }
    }
}
