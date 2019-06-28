using System.Data;

namespace DiDemo.Cli.Mocks
{
    internal partial class FakeDbConnection : IDbConnection
    {
        private class FakeDbCommand : IDbCommand
        {
            public string CommandText { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
            public int CommandTimeout { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
            public CommandType CommandType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
            public IDbConnection Connection { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

            public IDataParameterCollection Parameters => throw new System.NotImplementedException();

            public IDbTransaction Transaction { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
            public UpdateRowSource UpdatedRowSource { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

            public void Cancel()
            {
                throw new System.NotImplementedException();
            }

            public IDbDataParameter CreateParameter()
            {
                throw new System.NotImplementedException();
            }

            public void Dispose()
            {
            }

            public int ExecuteNonQuery()
            {
                throw new System.NotImplementedException();
            }

            public IDataReader ExecuteReader()
            {
                throw new System.NotImplementedException();
            }

            public IDataReader ExecuteReader(CommandBehavior behavior)
            {
                throw new System.NotImplementedException();
            }

            public object ExecuteScalar()
            {
                throw new System.NotImplementedException();
            }

            public void Prepare()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
