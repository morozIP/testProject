using System;
using MySqlConnector;

namespace WebApplication1
{
    public class DB : IDisposable
    {        
        public MySqlConnection Connection { get; }

        public DB(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
        
    }
}
