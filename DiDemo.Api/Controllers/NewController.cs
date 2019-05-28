using DiDemo.Abstractions;
using DiDemo.Api.Extensions;
using DiDemo.Services.CompanyServices;
using Microsoft.AspNetCore.Mvc;

namespace DiDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewController : ControllerBase
    {
        private readonly ICompanyPriceProvider _companyPriceProvider;
        private readonly ILogger _logger;

        // Example 1, part 4: reuse
        public NewController(ICompanyPriceProvider companyPriceProvider, ILogger logger /* Example 1, part 5: add dependency */)
        {
            _companyPriceProvider = companyPriceProvider;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            _logger.Log("NewController is called");

            var companyPrice = _companyPriceProvider.GetCompany(id);
            if (companyPrice == null)
            {
                return NotFound();
            }

            return companyPrice.ToStringFormat();
        }
    }
}
