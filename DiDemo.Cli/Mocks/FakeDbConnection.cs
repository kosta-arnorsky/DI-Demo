using DiDemo.Abstractions;
using System.Data;

namespace DiDemo.Cli.Mocks
{
    internal partial class FakeDbConnection : IDbConnection
    {
        private readonly ILogger _logger;

        public FakeDbConnection(ILogger logger)
        {
            _logger = logger;
            _logger.Log("DB connection has been created.");
        }

        public string ConnectionString { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public int ConnectionTimeout => throw new System.NotImplementedException();

        public string Database => throw new System.NotImplementedException();

        public ConnectionState State => throw new System.NotImplementedException();

        public IDbTransaction BeginTransaction()
        {
            return new FakeDbTransaction(_logger);
        }

        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new System.NotImplementedException();
        }

        public void ChangeDatabase(string databaseName)
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public IDbCommand CreateCommand()
        {
            return new FakeDbCommand();
        }

        public void Dispose()
        {
            _logger.Log("DB connection has been disposed.");
        }

        public void Open()
        {
            throw new System.NotImplementedException();
        }
    }
}
