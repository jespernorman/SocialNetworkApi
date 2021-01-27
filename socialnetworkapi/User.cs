using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SocialNetworkApi
{
    public class User
    {
        /// <summary>
        /// Id of the User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// UserName for the User
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// EmailAdress of the User
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// Date and time when the User was added to the program
        /// </summary>
        public DateTime CreateDate { get; set; }

        [JsonIgnore]
        public string PassWord { get; set; }

        [JsonIgnore]
        public List<User> UserList = new List<User>();
        [JsonIgnore]
        private UserRepository UserRepository { get; set; }
                                                                            
        public User(string dbPath)
        {
            UserRepository = new UserRepository(dbPath);
            LoadAllUsers();
        }
        public User()
        {

        }

        /// <summary>
        /// Loads all Users
        /// </summary>
        public void LoadAllUsers()
        {
            UserList = UserRepository.GetAll();
        }

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        public bool CreateUser(string userName, string passWord, string emailAdress)
        {

            UserRepository.AddUser(userName, passWord, emailAdress);

            LoadAllUsers();
            return true;
            
        }

        /// <summary>
        /// Login varification 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public bool Login(string userName, string passWord)
        {

            if (UserList.Any(x => x.UserName == userName && x.PassWord == passWord))
            {
                var loggedInUser = UserList.FirstOrDefault(x => x.UserName == userName && x.PassWord == passWord);
                // Set the properties with the logged in user
                UserId = loggedInUser.UserId;
                CreateDate = loggedInUser.CreateDate;
                UserName = loggedInUser.UserName;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Find a User in the program by his/hers Username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserByName(string userName)
        {
            return UserRepository.GetByUserName(userName);
           
        }

        /// <summary>
        /// Find a User in the program by his/hers UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserById(int userId)
        {
            return UserRepository.GetById(userId);
        }

        /// <summary>
        /// Email varification
        /// <summary>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        public bool IsValidEmail(string emailAdress)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(emailAdress);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}