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

        public void AddLike()
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
                var listOfPosts = connection.Query<Like>("SELECT * FROM Like").AsList();
                return listOfPosts;
            }
        }
    }
}
