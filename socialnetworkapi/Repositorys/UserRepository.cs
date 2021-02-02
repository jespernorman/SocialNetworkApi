using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using SocialNetworkApi.Models;

namespace SocialNetworkApi.Repositorys
{
    public class UserRepository
    {
        private string DBPath { get; set; }

        public UserRepository(string dbPath)
        {
            DBPath = dbPath;
        }


        /// <summary>
        /// returns a list of all users in the program
        /// <summary>
        public List<User> GetAll()
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var UserList = new List<User>();

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                UserList = connection.Query<User>("SELECT * FROM User").AsList();
            }

            return UserList;
        }

        /// <summary>
        /// Gets a User by UserId
        /// <summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetById(int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var user = new User(DBPath);

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                user = connection.QueryFirst<User>("SELECT * FROM User WHERE UserId=@userId", new { userId });
            }

            return user;
        }

        /// <summary>
        /// Gets a User by UserName
        /// <summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetByUserName(string userName)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            var user = new User(DBPath);

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<User>("SELECT * FROM User WHERE UserName=@UserName", new { UserName = userName });
            }
        }

        /// <summary>
        /// Adds a User to the database
        /// <summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        public bool AddUser(string userName, string passWord, string emailAdress)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int insertedRow = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                var query = "INSERT INTO User(UserName, PassWord, EmailAdress, CreateDate) VALUES(@UserName, @PassWord, @EmailAdress, @CreateDate)"; 

                var dp = new DynamicParameters();
                dp.Add("@UserName", userName, DbType.AnsiString, ParameterDirection.Input, 255);
                dp.Add("@PassWord", passWord);
                dp.Add("@EmailAdress", emailAdress);
                dp.Add("@CreateDate", DateTime.Now);

                insertedRow = connection.Execute(query, dp);

            }

            if (insertedRow > 0)
            {
                return true;
            }

            return false;

        }

        /// <summary>
        /// Updates a users information in database
        /// <summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        public bool UpdateUser(int userId, string userName, string passWord, string emailAdress)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int updatedRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                updatedRows = connection.Execute("UPDATE User SET UserName = @userName, PassWord, EmailAdress = @PassWord WHERE UserId = @userId", new { userName, PassWord = passWord, EmailAdress = emailAdress, userId });
            }

            if (updatedRows > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Deletes a users from the database
        /// <summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteUser(int userId)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = DBPath;

            int delRows = 0;

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                delRows = connection.Execute(@"DELETE FROM User WHERE UserId = @Id", new { Id = userId });
            }

            if (delRows > 0)
                return true;

            return false;
        }

    }
}
