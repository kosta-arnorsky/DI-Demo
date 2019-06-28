using DiDemo.Abstractions;
using System.Data;

namespace DiDemo.Cli.Mocks
{
    internal partial class FakeDbConnection
    {
        private class FakeDbTransaction : IDbTransaction
        {
            private bool _commited;
            private readonly ILogger _logger;
            private readonly object _sync = new object();

            public FakeDbTransaction(ILogger logger)
            {
                _logger = logger;
                _logger.Log("Transaction has started.");
            }

            public IDbConnection Connection => throw new System.NotImplementedException();

            public IsolationLevel IsolationLevel => throw new System.NotImplementedException();

            public void Commit()
            {
                lock (_sync)
                {
                    if (!_commited)
                    {
                        _commited = true;
                        _logger.Log("Transaction has been commited.");
                    }
                }
            }

            public void Dispose()
            {
                lock (_sync)
                {
                    if (!_commited)
                    {
                        Rollback();
                    }
                }
            }

            public void Rollback()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
