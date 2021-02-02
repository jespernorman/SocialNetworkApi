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
    public class LikesController : Controller
    {
        private readonly string dbPath = Environment.CurrentDirectory + "/databas/SocialNetwork.db";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postItemId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost("{id}/{username}")]
        public ActionResult AddLike(int id, string username)
        {
            if (IsAuthenticatedUser(username))
            {
                var user = new User(dbPath);
                var currentUser = user.GetUserByName(username);

                var like = new Like(dbPath);

                if (like.AddLike(id, currentUser.UserId))
                {
                    return Ok();
                }

                return BadRequest("Could not like postitem with id:" + id);
            }

            return Unauthorized();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postItemId"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpDelete("{id}/{username}")]
        public ActionResult DeleteLike(int id, string username)
        {
            if (IsAuthenticatedUser(username))
            {
                var user = new User(dbPath);
                var currentUser = user.GetUserByName(username);

                var like = new Like(dbPath);

                if (like.DeleteLike(id, currentUser.UserId))
                {
                    return Ok();
                }

                return BadRequest("Could not delete like on postItemId:" + id + " and user:" + currentUser.UserName);
            }

            return Unauthorized();
        }

        /// <summary>
        /// Checks if the user exists
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool IsAuthenticatedUser(string userName)
        {
            var user = new User(dbPath);

            if (user.GetUserByName(userName) != null)
                return true;

            return false;
        }
    }
}
