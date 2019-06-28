using DiDemo.Abstractions;
using DiDemo.Services.CompanyServices;
using System;
using System.Data;
using System.Linq;

namespace DiDemo.Data
{
    public class DbCompanyRepository : ICompanyRepository
    {
        private static readonly Company[] _fakeStorage =
        {
            new Company
            {
                Id = 2,
                Name = "MICROSOFT",
                StockId = "MSFT"
            },
            new Company
            {
                Id = 3,
                Name = "GOOGLE",
                StockId = "GOOGL"
            },
            new Company
            {
                Id = 5,
                Name = "FACEBOOK",
                StockId = "FB"
            }
        };

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
                // A DB query
                return _fakeStorage.FirstOrDefault(c => c.Id == id);
            }
        }

        public Company FindCompany(string name)
        {
            _logger.Log($"Query company name {name}.");

            using (var command = _connection.CreateCommand())
            {
                // A DB query
                return _fakeStorage.FirstOrDefault(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            }
        }
    }
}
