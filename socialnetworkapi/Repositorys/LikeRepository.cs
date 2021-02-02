using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using SocialNetworkApi.Models;

namespace SocialNetworkApi.Repositorys
{
    public class LikeRepository
    {
        private string DBPath { get; set; }

        public LikeRepository(string dbPath)
        {
            DBPath = dbPath;
        }

        /// <summary>
        /// Returns all likes stores in database
        /// <summary>
        /// <param name="postItemId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets likes a specifik user has made
        /// <summary>
        /// <param name="postItemId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get likes on a specifik post
        /// <summary>
        /// <param name="postItemId"></param>
        /// <returns></returns>
        public List<Like> GetLikesByPostItemId(int postItemId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                return connection.Query<Like>("SELECT * FROM Like where PostItemId = @postItemId", new { postItemId }).AsList();
            }
        }

        /// <summary>
        /// Adds a like to a specifik post
        /// <summary>
        /// <param name="postItemId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddLikeToPost(int postItemId, int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;
            
            int insertedRow = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var query = "INSERT INTO Like (UserId, PostItemId) VALUES(@UserId, @PostItemId)";
                var dp = new DynamicParameters();

                dp.Add("@UserId", userId);
                dp.Add("@PostItemId", postItemId);

                insertedRow = connection.Execute(query, dp);
            }

            if (insertedRow > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Deletes a like
        /// <summary>
        /// <param name="postItemId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteLike(int postItemId, int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int delRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                delRows = connection.Execute(@"DELETE FROM LIKE WHERE PostItemId=@postItemId and UserId=@userId", new { postItemId , userId });
            }

            if (delRows > 0)
            {
                return true;
            }

            return false;
        }
    }
}
