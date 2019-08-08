using DiDemo.Abstractions;
using DiDemo.Formatting;
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

        // BOOKMARK: 1.4 reuse
        public NewController(ICompanyPriceProvider companyPriceProvider, ILogger logger /* BOOKMARK: 1.5 add dependency */)
        {
            _companyPriceProvider = companyPriceProvider;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            _logger.Log("NewController is called");

            var companyPrice = _companyPriceProvider.GetPrice(id);
            if (companyPrice == null)
            {
                return NotFound();
            }

            return companyPrice.ToStringFormat();
        }
    }
}
