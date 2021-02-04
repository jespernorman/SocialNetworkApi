using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Models;
using SocialNetworkApi.Repositorys;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly string dbPath = Environment.CurrentDirectory + "/databas/SocialNetwork.db";

         /// <summary>
        /// Injecting repositories
        /// </summary>
        private readonly UserRepository userRepository;

        public UsersController()
        {
            userRepository = new UserRepository(dbPath);
        }

        /// <summary>
        /// Adds a user to the system
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="emailAdress"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser(UserRequest userRequest)
        {
            if (userRepository.GetByUserName(userRequest.UserName) == null)
            {
                var user = new User();
                if (user.IsValidEmail(userRequest.EmailAdress))
                {
                    if (userRepository.CreateUser(userRequest))
                    {
                        return Ok();
                    }
                    return BadRequest("User could not be added.");
                }

                return BadRequest("Invalid emailadress!");
            }
            
            return BadRequest("User already exists.");          
        }
    }
}
