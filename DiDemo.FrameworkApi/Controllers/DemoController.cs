using DiDemo.Formatting;
using DiDemo.Services.CompanyServices;
using System.Net;
using System.Web.Http;

namespace DiDemo.FrameworkApi.Controllers
{
    public class DemoController : ApiController
    {
        private readonly ICompanyPriceProvider _companyPriceProvider;

        public DemoController(ICompanyPriceProvider companyPriceProvider)
        {
            _companyPriceProvider = companyPriceProvider;
        }

        public IHttpActionResult Get(long id)
        {
            var companyPrice = _companyPriceProvider.GetPrice(id);
            if (companyPrice == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }

            return Ok(companyPrice.ToStringFormat());
        }
    }
}
