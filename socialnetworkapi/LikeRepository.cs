using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace SocialNetworkApi
{
    public class LikeRepository
    {
        private string DBPath { get; set; }

        public LikeRepository(string dbPath)
        {
            DBPath = dbPath;
        }

        public void AddLike(int postItemId, int userId)
        {

        }

        public List<Like> GetAllLikes(int postItemId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var listOfPosts = connection.Query<Like>("SELECT * FROM Like").AsList();
                return listOfPosts;
            }
        }

        public List<Like> GetLikesByUser(int postItemId, int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var listOfLikes = connection.Query<Like>("SELECT * FROM Like").AsList();
                return listOfLikes;
            }
        }
        public void AddLikeToPost(int postItemId, int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int insertedRow = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var query = "INSERT INTO Like(UserId, PostItemId) VALUES(@UserId, @PostItemId";
                var dp = new DynamicParameters();

                dp.Add("@UserId", userId);
                dp.Add("@PostItemId", postItemId);

                insertedRow = connection.Execute(query, dp);
            }
        }
        public void RemoveLike(int postItemId, int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int delRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                delRows = connection.Execute(@"DELETE FROM LIKE WHERE PostItemId = PostItemId", new { PostItemId = postItemId });
                delRows = connection.Execute(@"DELETE FROM LIKE WHERE UserId = UserId", new { UserId = userId });
            }
        }
    }
}
