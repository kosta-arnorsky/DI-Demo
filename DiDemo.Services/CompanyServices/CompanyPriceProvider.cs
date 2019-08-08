using DiDemo.Services.Stock;

namespace DiDemo.Services.CompanyServices
{
    public class CompanyPriceProvider : ICompanyPriceProvider
    {
        private readonly ICompanyService _companyService;
        private readonly IPriceProvider _priceProvider;

        public CompanyPriceProvider(
            ICompanyService companyService,
            IPriceProvider priceProvider)
        {
            _companyService = companyService;
            _priceProvider = priceProvider;
        }

        public CompanyPrice GetPrice(long id)
        {
            CompanyPrice companyPrice = null;

            var company = _companyService.GetCompany(id);
            if (company != null)
            {
                companyPrice = new CompanyPrice
                {
                    Name = company.Name,
                    Price = _priceProvider.GetAveragePrice(company.StockId)
                };
            }

            return companyPrice;
        }
    }
}
