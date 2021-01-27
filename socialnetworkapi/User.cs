using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkApi
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string EmailAdress { get; set; }
        public DateTime CreateDate { get; set; }

        public List<User> UserList = new List<User>();
        private UserRepository UserRepository { get; set; }

                                                                                                //Användarnamn måste vara unika. Dem kan bara innehålla alfanumeriska karaktärer
                                                                                                //Detaljerad email adressvalidering kommer läggas till senare.I denna första version skall
                                                                                                //bara grundläggande validering användas välj en metod som inte är strikt vi vill inte                                                                                   //blockera potentiella användare

        public User(string dbPath)
        {
            var userRepository = new UserRepository(dbPath);
            //LoadAllUsers();
        }
        public User()
        {

        }

        public void LoadAllUsers()
        {
            UserList = UserRepository.GetAll();
        }

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

        public User GetUserByName(string userName, string dbPath)
        {
            var user = new User(dbPath);
            var userId = user.UserId;

            if(UserList.Any(x => x.UserName == userName && x.UserId == userId))
            {
                return user;
            }
            else
            {
                return null;
            }
           
        }
    }
}
