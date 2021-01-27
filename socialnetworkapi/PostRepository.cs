using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace SocialNetworkApi
{
    public class PostRepository
    {
        public string DBPath { get; set; }

        public PostRepository(string dbPath)
        {
            DBPath = dbPath;
        }

        public List<PostItem> LoadAllPosts()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var listOfPosts = connection.Query<PostItem>("SELECT * FROM PostItem").AsList();

                var newListOfPost = new List<PostItem>();

                foreach(var postItem in listOfPosts)
                {
                    //Load full PostItem object
                    newListOfPost.Add(GetPostItemById(postItem.PostItemId));
                }

                return newListOfPost;
            }
        }

        public bool AddPost(int userId, string itemMessage) 
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int insertedRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                insertedRows = connection.Execute("INSERT INTO PostItem (UserId, ItemMessage, CreateDate) VALUES (@UserId, @ItemMessage, @CreateDate)",
                    new { UserId = userId, ItemMessage = itemMessage, CreateDate = DateTime.Now });

            }

            if (insertedRows > 0)
            {
                return true;
            }

            return false;
        }

        public PostItem UpdatePostItem(int postItemId, string itemMessage)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;
            var modifiedDate = DateTime.Now;
            int updatedRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                updatedRows = connection.Execute("UPDATE PostItem SET ItemMessage = @itemMessage, ModifiedDate = @modifiedDate WHERE PostItemId = @postItemId", new { postItemId, itemMessage, modifiedDate });
            }

            if (updatedRows > 0)
            {
                return GetPostItemById(postItemId);
            }

            return null;
        }

        public bool DeletePostItem(int postItemId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int delRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                delRows = connection.Execute(@"DELETE FROM PostItem WHERE PostItemId = @Id", new { Id = postItemId });
            }

            if (delRows > 0)
            {
                return true;
            }

            return false;
        }

        public PostItem GetPostItemById(int postItemId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                //Load the postItem
                var postItem = connection.QueryFirst<PostItem>("SELECT * FROM PostItem WHERE PostItemId=@postItemId", new { postItemId });

                //Get the user that created the post
                var userRepository = new UserRepository(DBPath);
                postItem.UserCreator = userRepository.GetById(postItem.UserId);

                //Get likes
                var like = new Like(DBPath);
                postItem.ListOfLikes = like.GetLikesByPostId(postItem.PostItemId);

                return postItem;
            }
        }
    }
}