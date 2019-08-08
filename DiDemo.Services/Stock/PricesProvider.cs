using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace DiDemo.Services.Stock
{
    public class PricesProvider : IPricesProvider
    {
        private readonly PricesProviderOptions _options;
        private readonly IStockRepository _stockRepository;

        public PricesProvider(
            IOptions<PricesProviderOptions> options, // BOOKMARK: 3.1 explicit dependencies
            IStockRepository stockRepository)
        {
            _options = options.Value;
            _stockRepository = stockRepository;
        }

        public IReadOnlyCollection<decimal> GetLastPrices(string stockId)
        {
            var prices = _stockRepository.GetPrices(stockId);
            if (prices != null)
            {
                // BOOKMARK: 3.2 compare to ConfigurationManager.AppSettings["MaxCountOfPrices"]
                prices = prices.Take(_options.MaxCountOfPrices).ToArray();
            }

            return prices;
        }
    }
}
