using DiDemo.Services.Stock;
using System.Linq;

namespace DiDemo.Services.CompanyServices
{
    public class CompanyPriceProvider : ICompanyPriceProvider
    {
        private readonly ICompanyService _companyService;
        private readonly IPricesProvider _pricesProvider;

        public CompanyPriceProvider(
            ICompanyService companyService,
            IPricesProvider pricesProvider)
        {
            _companyService = companyService;
            _pricesProvider = pricesProvider;
        }

        public CompanyPrice GetPrice(long id)
        {
            var company = _companyService.GetCompany(id);
            if (company != null)
            {
                var companyPrice = new CompanyPrice
                {
                    Name = company.Name
                };

                var prices = _pricesProvider.GetLastPrices(company.StockId);
                if (prices != null && prices.Any())
                {
                    companyPrice.Price = prices.Average();
                }

                return companyPrice;
            }

            return null;
        }
    }
}
