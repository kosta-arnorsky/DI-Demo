using DiDemo.Data;
using DiDemo.Services.Stock;
using System.Collections.Generic;
using System.Data;

namespace DiDemo.Api.Staging
{
    public class StockRepositoryMock : IStockRepository
    {
        private readonly DbStockRepository _dbRepository;

        public StockRepositoryMock(IDbConnection connection)
        {
            _dbRepository = new DbStockRepository(connection);
        }

        public IReadOnlyCollection<decimal> GetPrices(string stockId)
        {
            switch (stockId)
            {
                // BOOKMARK: 4.2 fake a specific case
                case "FB":
                    return new decimal[] { 777.11m, 888.22m, 999.33m };
                default:
                    return _dbRepository.GetPrices(stockId);
            }
        }
    }
}
