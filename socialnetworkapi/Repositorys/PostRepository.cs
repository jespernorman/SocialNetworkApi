using System;
using System.Collections.Generic;
using Dapper;
using Microsoft.Data.Sqlite;
using SocialNetworkApi.Models;

namespace SocialNetworkApi.Repositorys
{
    public class PostRepository
    {
        public string DBPath { get; set; }

        public PostRepository(string dbPath)
        {
            DBPath = dbPath;
        }

        /// <summary>
        /// Loads all posts stores in database
        /// <summary>
        public List<PostItem> GetAllPosts()
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
        /// <summary>
        /// Adds post in database
        /// <summary>
        /// <param name="userId"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public bool AddPost(int userId, PostItemRequest post) 
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int insertedRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                insertedRows = connection.Execute("INSERT INTO PostItem (UserId, ItemMessage, CreateDate, ModifiedDate) VALUES (@UserId, @ItemMessage, @CreateDate, @ModifiedDate)",
                    new { UserId = userId, ItemMessage = post.ItemMessage, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now });

            }

            if (insertedRows > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates a post the user has made
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public PostItem UpdatePostItem(int id, PostItemRequest post)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;
            var modifiedDate = DateTime.Now;
            int updatedRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                updatedRows = connection.Execute("UPDATE PostItem SET ItemMessage = @itemMessage, ModifiedDate = @modifiedDate WHERE PostItemId = @postItemId", new { postItemId = id, itemMessage = post.ItemMessage, modifiedDate });
            }

            if (updatedRows > 0)
            {
                return GetPostItemById(id);
            }

            return null;
        }

        /// <summary>
        /// deletes a post from datbase
        /// <summary>
        /// <param name="postItemId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns a post by its postitemid
        /// <summary>
        /// <param name="postItemId"></param>
        /// <returns></returns>
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