using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly string dbPath = Environment.CurrentDirectory + "/databas/SocialNetwork.db";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(string userName, string passWord, string emailAdress)
        {
            var user = new User(dbPath);

            if (user.UserList.Any(x => x.UserName == userName))
            {
                return BadRequest("Username is already taken");
            }
            else
            {
                if (user.IsValidEmail(emailAdress) != true)
                {
                    return BadRequest("Emailadress was not valid.");
                }
                else
                {
                    user.CreateUser(userName, passWord, emailAdress);
                }

                return Ok();
            }
        }
    }
}
