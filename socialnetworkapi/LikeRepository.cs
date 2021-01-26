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
                var listOfLikes = connection.Query<Like>("SELECT * FROM Like").AsList();                //WHERE UserId = UserId"????
                return listOfLikes;
            }
        }
        public void AddLikeToPost(Like like)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;
            

            int insertedRow = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var query = "INSERT INTO Like(UserId, PostItemId) VALUES(@UserId, @PostItemId";
                var dp = new DynamicParameters();

                dp.Add("@UserId", like.UserId);
                dp.Add("@PostItemId", like.PostItemId);

                insertedRow = connection.Execute(query, dp);
            }
        }
        public void RemoveLike(int likeId, int postItemId, int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int delRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                delRows = connection.Execute(@"DELETE FROM LIKE WHERE LikeId = LikeId", new { LikeId = likeId });                       //Delete row?
                delRows = connection.Execute(@"DELETE FROM LIKE WHERE PostItemId = PostItemId", new { PostItemId = postItemId });
                delRows = connection.Execute(@"DELETE FROM LIKE WHERE UserId = UserId", new { UserId = userId });
            }
        }
    }
}
