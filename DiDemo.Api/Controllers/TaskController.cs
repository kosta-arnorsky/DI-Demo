using DiDemo.Api.Services.Background;
using Microsoft.AspNetCore.Mvc;

namespace DiDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IBackgroundCompanyPriceService _companyPriceService;

        public TaskController(IBackgroundCompanyPriceService companyPriceService)
        {
            _companyPriceService = companyPriceService;
        }

        [HttpPost("{companyId}")]
        public ActionResult Add([FromBody] long companyId)
        {
            _companyPriceService.ScheduleRetrievePrice(companyId);

            return Ok();
        }

        // For the demo purposes only, for an easier call from a browser
        [HttpGet("{companyId}")]
        public ActionResult<string> AddDemo(long companyId)
        {
            _companyPriceService.ScheduleRetrievePrice(companyId);

            return $"Price retrieval of the company ID {companyId}  has been scheduled.";
        }
    }
}
