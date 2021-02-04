using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using SocialNetworkApi.Repositorys;

namespace SocialNetworkApi.Models
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

        public string PassWord { get; set; }

        public List<User> UserList = new List<User>();
                                                                            
        public User()
        {

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