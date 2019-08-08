using DiDemo.Services.Stock;
using System;
using System.Collections.Generic;
using System.Data;

namespace DiDemo.Data
{
    public class DbStockRepository : IStockRepository
    {
        private readonly IDbConnection _connection;
        private readonly Random _random;

        public DbStockRepository(IDbConnection connection)
        {
            _connection = connection;
            _random = new Random();
        }

        public IReadOnlyCollection<decimal> GetPrices(string stockId, int maxCount)
        {
            using (var command = _connection.CreateCommand())
            {
                // A DB query
                switch (stockId)
                {
                    case "MSFT":
                    case "FB":
                    case "AAPL":
                        return GetRandomPrices(maxCount);
                    default:
                        return null;
                }
            }
        }

        private IReadOnlyCollection<decimal> GetRandomPrices(int maxCount)
        {
            decimal basePrice = _random.Next(10000);

            var count = _random.Next(2, maxCount + 1);
            var prices = new List<decimal>(count);
            for (int i = 0; i <= count; i++)
            {
                prices.Add((basePrice + _random.Next(10) - 5) / 100);
            }

            return prices;
        }
    }
}
