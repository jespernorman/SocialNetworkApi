using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace SocialNetworkApi
{
    public class PostRepository
    {
        public List<PostItem> ListOfPosts = new List<PostItem>();
        private string DBPath { get; set; }

        public PostRepository(string dbPath)
        {
            DBPath = dbPath;
        }

        public List<PostItem> LoadAllPosts()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var postsLists = new List<PostItem>();
            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                ListOfPosts = connection.Query<PostItem>("SELECT * FROM Post_Item").AsList();
            }
            return ListOfPosts;
        }

        public bool AddPost(User user, string itemMessage) 
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int insertedRow = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var query = "INSERT INTO Post_Item(User_Id, UserName, Item_Message, Create_date) VALUES(@User_Id, @UserName, @Item_Message @Create_Date";  //Vilken är in i databasen nu igen @ eller icke?

                var dp = new DynamicParameters();
                dp.Add("@User_Id", user.UserId);
                dp.Add("@UserName", user.UserName, DbType.AnsiString, ParameterDirection.Input, 255);
                dp.Add("@Item_Message", itemMessage, DbType.AnsiString, ParameterDirection.Input, 255);
                dp.Add("@Create_Date", DateTime.Now);
                
                insertedRow = connection.Execute(query, dp);
            }

            if (insertedRow > 0)
            {
                return true;
            }

            return false;
        }

        public bool UpdatePostItem(int postItemId, string itemMessage)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int updatedRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                updatedRows = connection.Execute("UPDATE Post_Item SET Item_Message = @Item_Message WHERE Post_Id = @postId", new { postItemId, itemMessage });
            }

            if (updatedRows > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeletePostItem(int postItemId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int delRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                delRows = connection.Execute(@"DELETE FROM Post WHERE Id = @Id", new { Id = postItemId });
            }

            if (delRows > 0)
            {
                return true;
            }

            return false;
        }

    }
}
