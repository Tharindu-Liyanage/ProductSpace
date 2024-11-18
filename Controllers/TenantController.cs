using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductSpace.Services.TenantService;
using ProductSpace.Services.TenantService.DTOs;

namespace ProductSpace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantServce _tenantService;

        public TenantController(ITenantServce tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost]
        public IActionResult Post(CreateTenantRequsest requsest)
        {
           var result = _tenantService.CreateTenant(requsest);
            return Ok(result);
        }

       
    }
}
