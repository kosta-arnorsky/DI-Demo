using System.Collections.Generic;

namespace DiDemo.Services.Stock
{
    public interface IPricesProvider
    {
        /// <summary>
        /// Returns last three prices of the stock.
        /// </summary>
        IReadOnlyCollection<decimal> GetLastPrices(string stockId);
    }
}