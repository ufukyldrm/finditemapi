using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;

namespace Tarla
{
    public class ApplicationDatabaseObject : IDisposable

    {

        public MySqlConnection Connection { get; }

        public ApplicationDatabaseObject(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();

    }
}
