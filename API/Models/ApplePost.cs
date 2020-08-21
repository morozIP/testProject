using System.Data;
using System.Threading.Tasks;
using MySqlConnector;

namespace WebApplication1.Models
{
    public class ApplePost
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string Info { get; set; }

        internal DB Db { get; set; }

        public ApplePost()
        {
        }

        internal ApplePost(DB db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `apple` (`Type`, `Model`, `Info`) VALUES (@type, @model, @info);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            Id = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `apple` SET `Type` = @type, `Model` = @model, `Info` = @info WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `apple` WHERE `Id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@type",
                DbType = DbType.String,
                Value = Type,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@model",
                DbType = DbType.String,
                Value = Model,
            }); 
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@info",
                DbType = DbType.String,
                Value = Info,
            });
        }
    }
}
