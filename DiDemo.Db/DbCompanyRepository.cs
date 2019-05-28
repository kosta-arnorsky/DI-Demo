using DiDemo.Abstractions;
using DiDemo.Services;
using System.Collections.Generic;
using System.Data;

namespace DiDemo.Db
{
    public class DbCompanyRepository : ICompanyRepository
    {
        private IDbConnection _connection;
        private readonly ILogger _logger;

        public DbCompanyRepository(IDbConnection connection, ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public Dictionary<string, string> GetCompany(long id)
        {
            _logger.Log($"Query company ID {id}.");

            using (var command = _connection.CreateCommand())
            {
                // a DB query
                switch (id)
                {
                    case 2:
                        return new Dictionary<string, string>
                        {
                            {  "name", "Microsoft" },
                            {  "stockId", "MSFT" }
                        };
                    case 3:
                        return new Dictionary<string, string>
                        {
                            {  "name", "Google" },
                            {  "stockId", "GOOGL" }
                        };
                    case 5:
                        return new Dictionary<string, string>
                        {
                            {  "name", "Facebook" },
                            {  "stockId", "FB" }
                        };
                    default:
                        return null;
                }
            }
        }
    }
}
