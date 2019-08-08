using Microsoft.Extensions.Options;
using System.Linq;

namespace DiDemo.Services.Stock
{
    public class PriceProvider : IPriceProvider
    {
        private readonly PriceProviderOptions _options;
        private readonly IStockRepository _stockRepository;

        public PriceProvider(
            IOptions<PriceProviderOptions> options, // BOOKMARK: 3.1 explicit dependencies
            IStockRepository stockRepository)
        {
            _options = options.Value;
            _stockRepository = stockRepository;
        }

        public decimal? GetAveragePrice(string stockId)
        {
            // BOOKMARK: 3.2 compare to ConfigurationManager.AppSettings["MaxCountOfPrices"]
            var prices = _stockRepository
                .GetPrices(stockId, _options.MaxCountOfPrices);

            return prices != null && prices.Any()
                ? prices.Average()
                : (decimal?)null;
        }
    }
}
