using DiDemo.Data;
using DiDemo.Formatting;
using DiDemo.Logging;
using DiDemo.Services.CompanyServices;
using DiDemo.Services.Stock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace DiDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoInjectionController : ControllerBase
    {
        private readonly SqlConnection _dbConnection;
        private readonly ICompanyPriceProvider _companyPriceProvider;

        public NoInjectionController()
        {
            const int maxCountOfPrices = 3; // Read from config

            // BOOKMARK: 1.2 no DI
            _dbConnection = new SqlConnection("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=Demo;Integrated Security=SSPI;");
            _companyPriceProvider = new CompanyPriceProvider(
                new CompanyService(new DbCompanyRepository(_dbConnection, new ConsoleLogger())),
                new PricesProvider(
                    Options.Create(new PricesProviderOptions
                    {
                        MaxCountOfPrices = maxCountOfPrices
                    }),
                    new DbStockRepository(_dbConnection)));
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            var companyPrice = _companyPriceProvider.GetPrice(id);
            RegisterForDispose();
            if (companyPrice == null)
            {
                return NotFound();
            }

            return companyPrice.ToStringFormat();
        }

        private void RegisterForDispose()
        {
            HttpContext.Response.RegisterForDispose(_dbConnection);
        }
    }
}
