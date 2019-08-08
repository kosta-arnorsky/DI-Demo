using System.Collections.Generic;

namespace DiDemo.Services.Stock
{
    public interface IPriceProvider
    {
        /// <summary>
        /// Returns an average price of the stock.
        /// </summary>
        decimal? GetAveragePrice(string stockId);
    }
}