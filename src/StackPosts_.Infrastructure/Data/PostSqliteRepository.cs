using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackPosts_.Core.Entities;
using StackPosts_.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackPosts_.Infrastructure.Data
{
    public class PostSqliteRepository : IGenericRepository<Post>, IPostRepository
    {
        private readonly ISqlDataAccess _sqliteDataAccess;
        private readonly ILogger<PostSqliteRepository> _logger;
        private readonly IConfiguration _config;
        private readonly string ConnectionString = "DefaultConnection";

        public PostSqliteRepository(ISqlDataAccess sqliteDataAccess, ILogger<PostSqliteRepository> logger,
           IConfiguration config )
        {
            _sqliteDataAccess = sqliteDataAccess;
            _logger = logger;
            _config = config;
        }

        public void Add(Post entity)
        {
            string sql = "INSERT INTO Posts (Body, Title, DateCreated, Score) VALUES @Body, @Title, @DateCreated, @Score";

            try
            {
                var post = new
                {
                    entity.Id,
                    entity.Title,
                    entity.Body,
                    entity.DatePosted,
                    entity.Score,
                    entity.Deleted
                };

                _sqliteDataAccess.SaveData(sql, post, ConnectionString);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting entity! See the following: {ex.Message}");
                throw;
            }
        }

        public void AddReply(Reply entity)
        {
            string sql = "INSERT INTO Replies (PostId, Body, DateReplied, Score)" +
                         " SELECT @PostId, @Body, @DateReplied, @Score";

            try
            {
                var reply = new
                {
                    entity.Body,
                    entity.DateReplied,
                    entity.PostId,
                    entity.Score
                };

                _sqliteDataAccess.SaveData(sql, reply, ConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting entity! See the following: {ex.Message}");
                throw;
            }
        }

        public void Delete(Post entity)
        {
            string sql = "DELETE FROM Posts WHERE @Id = Id";

            try
            {
                var post = new
                {
                    entity.Id
                };

                _sqliteDataAccess.SaveData(sql, post, ConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting an entity. See the following: {ex.Message}");
                throw;
            }
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            string sql = "SELECT Id, Title, Body, DateCreated FROM Posts WHERE @id = Id";

            try
            {
                var post = await _sqliteDataAccess.LoadData<Post, dynamic>(sql, new { @Id = id }, ConnectionString);

                return post.SingleOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while retriving the record. See the following: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Post>> ListAllAsync()
        {
            string sql = "SELECT * FROM Posts";

            try
            {
                var posts = await _sqliteDataAccess.LoadData<Post, dynamic>(sql, new { }, ConnectionString);

                return posts.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while retriving records. See the following: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> PostExists(int id)
        {
            string sql = "SELECT CASE WHEN EXISTS (SELECT Id FROM Posts WHERE Id = @Id)" +
                         "THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS Result";

            try
            {
                using (var connection = new SqliteConnection(_config
                    .GetConnectionString(ConnectionString)))
                {
                    var isExists = await connection.QueryFirstAsync<bool>(sql,
                        new
                        {
                            @Id = id
                        });

                    return isExists;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while retriving a record. See the following: {ex.Message}");
                throw;
            }
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }
    }
}
