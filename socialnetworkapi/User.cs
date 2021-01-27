using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SocialNetworkApi
{
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EmailAdress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateDate { get; set; }

        [JsonIgnore]
        public string PassWord { get; set; }

        [JsonIgnore]
        public List<User> UserList = new List<User>();
        [JsonIgnore]
        private UserRepository UserRepository { get; set; }

                                                                                                //Användarnamn måste vara unika. Dem kan bara innehålla alfanumeriska karaktärer
                                                                                                //Detaljerad email adressvalidering kommer läggas till senare.I denna första version skall
                                                                                                //bara grundläggande validering användas välj en metod som inte är strikt vi vill inte                                                                                   //blockera potentiella användare

        public User(string dbPath)
        {
            UserRepository = new UserRepository(dbPath);
            LoadAllUsers();
        }
        public User()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadAllUsers()
        {
            UserList = UserRepository.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        public bool CreateUser(string userName, string passWord, string emailAdress)
        {
            if (UserList.Any(x => x.UserName == UserName))
            {
                return false;
            }
            else
            {
                UserRepository.AddUser(userName, passWord, emailAdress);
                LoadAllUsers();
                return true;
            }
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public User GetUserByName(string userName)
        {
            return UserRepository.GetByUserName(userName);
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserById(int userId)
        {
            return UserRepository.GetById(userId);
        }
    }
}