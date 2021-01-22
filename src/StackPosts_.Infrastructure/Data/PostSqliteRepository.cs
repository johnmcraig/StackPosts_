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

        public async Task Add(Post entity)
        {
            string sql = "INSERT INTO Posts (Body, Title, DatePosted, Score) VALUES (@Body, @Title, @DatePosted, @Score)";

            try
            {
                var post = new
                {
                    entity.Title,
                    entity.Body,
                    entity.DatePosted,
                    entity.Score
                };

                await _sqliteDataAccess.SaveData(sql, post, ConnectionString);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting an entity. See the following: {ex.Message}");
                throw new Exception("An error occurred while inserting a record", ex);
            }
        }

        public async Task<Reply> AddReply(Reply entity)
        {
            string sql = "INSERT INTO Replies (PostId, Body, DateReplied, Score, Deleted) " +
                         "VALUES (@PostId, @Body, @DateReplied, @Score, @Deleted);" +
                         " SELECT @PostId, @Body, @DateReplied, @Score, @Deleted";

            try
            {
                var reply = new
                {
                    entity.Body,
                    entity.DateReplied,
                    entity.PostId,
                    entity.Score,
                    entity.Deleted
                };

                await _sqliteDataAccess.SaveData(sql, reply, ConnectionString);

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error inserting entity! See the following: {ex.Message}");
                throw new Exception("An error occurred while inserting a record", ex);
            }
        }

        public async Task Delete(int id)
        {
            string sql = "DELETE FROM Posts WHERE Id = @Id";

            try
            {
                await _sqliteDataAccess.SaveData(sql, new { @Id = id }, ConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting an entity. See the following: {ex.Message}");
                throw new Exception("An error occurred while deleting a record", ex);
            }
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            string sql = "SELECT p.* FROM Posts AS p WHERE Id = @Id; " +
                         "SELECT r.* FROM Replies AS r WHERE PostId = @Id";

            try
            {
                using (var connection = new SqliteConnection(_config
                    .GetConnectionString(ConnectionString)))
                {
                    using (var multi = await connection.QueryMultipleAsync(sql,
                        new
                        {
                            @Id = id
                        }))
                    {
                        var post = multi.Read<Post>().SingleOrDefault();
                        var replies = multi.Read<Reply>().ToList();

                        if (post != null)
                        {
                            post.Replies = replies;
                        }

                        return post;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving the record. See the following: {ex.Message}");
                throw new Exception("An error occurred while retrieving a record", ex);
            }
        }

        public async Task<IEnumerable<Post>> ListAllAsync()
        {
            string sql = "SELECT p.*, r.* FROM Posts AS p LEFT JOIN Replies AS r ON p.Id = r.PostId ORDER BY p.DatePosted";

            try
            {
                using (var connection = new SqliteConnection(_config
                    .GetConnectionString(ConnectionString)))
                {
                    connection.Open();

                    var postDictionary = new Dictionary<int, Post>();

                    var results = await connection.QueryAsync<Post, Reply, Post>(sql, map: (p, r) =>
                    {
                        if (!postDictionary.TryGetValue(p.Id, out Post post))
                        {
                            post = p;
                            post.Replies = new List<Reply>();
                            postDictionary.Add(post.Id, post);
                        }

                        post.Replies.Add(r);
                        return post;

                    }, splitOn: "Id");

                    return results.Distinct().ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving records. See the following: {ex.Message}");
                throw new Exception("An error occurred while retrieving records", ex);
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
                _logger.LogError($"An error occurred while retriving a record. See the following: {ex.Message}");
                throw new Exception("An error occurred while checking the status of a record", ex);
            }
        }

        public async Task Update(Post entity)
        {
            string sql = "UPDATE Posts SET Title = @Title, Body = @Body WHERE Id= @Id";
            await _sqliteDataAccess.SaveData(sql, entity, ConnectionString);
        }
    }
}
