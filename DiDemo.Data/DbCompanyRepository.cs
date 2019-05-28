using DiDemo.Abstractions;
using DiDemo.Services.CompanyServices;
using System.Data;

namespace DiDemo.Data
{
    public class DbCompanyRepository : ICompanyRepository
    {
        private readonly IDbConnection _connection;
        private readonly ILogger _logger;

        public DbCompanyRepository(IDbConnection connection, ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public Company GetCompany(long id)
        {
            _logger.Log($"Query company ID {id}.");

            using (var command = _connection.CreateCommand())
            {
                // a DB query
                switch (id)
                {
                    case 2:
                        return new Company
                        {
                            Name = "MICROSOFT",
                            StockId = "MSFT"
                        };
                    case 3:
                        return new Company
                        {
                            Name = "GOOGLE",
                            StockId = "GOOGL"
                        };
                    case 5:
                        return new Company
                        {
                            Name = "FACEBOOK",
                            StockId = "FB"
                        };
                    default:
                        return null;
                }
            }
        }
    }
}
