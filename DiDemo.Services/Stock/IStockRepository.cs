using System.Collections.Generic;

namespace DiDemo.Services.Stock
{
    public interface IStockRepository
    {
        IReadOnlyCollection<decimal> GetPrices(string stockId);
    }
}
