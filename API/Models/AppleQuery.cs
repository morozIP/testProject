using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySqlConnector;

namespace WebApplication1.Models
{
    public class AppleQuery
    {
        public DB Db { get; }

        public AppleQuery(DB db)
        {
            Db = db;
        }

        public async Task<ApplePost> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Type`, `Model`, `Info` FROM `apple` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<ApplePost>> LatestPostsAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Type`, `Model`, `Info` FROM `apple` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            using var txn = await Db.Connection.BeginTransactionAsync();
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `apple`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<ApplePost>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<ApplePost>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new ApplePost(Db)
                    {
                        Id = reader.GetInt32(0),
                        Type = reader.GetString(1),
                        Model = reader.GetString(2),
                        Info = reader.GetString(3),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}
