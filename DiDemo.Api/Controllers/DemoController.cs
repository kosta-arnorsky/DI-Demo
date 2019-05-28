using DiDemo.Api.Extensions;
using DiDemo.Services.CompanyServices;
using Microsoft.AspNetCore.Mvc;

namespace DiDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ICompanyPriceProvider _companyPriceProvider;

        public DemoController(ICompanyPriceProvider companyPriceProvider)
        {
            // Example 1, part 2: DI
            _companyPriceProvider = companyPriceProvider;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            var companyPrice = _companyPriceProvider.GetCompany(id);
            if (companyPrice == null)
            {
                return NotFound();
            }

            return companyPrice.ToStringFormat();
        }
    }
}
