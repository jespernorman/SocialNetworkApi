using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    public class SocialNetworkController : Controller
    {
        public SocialNetworkController()
        {

        }

        //private const string dbPath = "/../../../Database/SocialNetwork.db";
        private string dbPath = Environment.CurrentDirectory + "/databas/SocialNetwork.db";

        // GET: api/values
        [HttpGet("GetAllPosts")]
        public ActionResult<IEnumerable<PostItem>> GetAllPostItems(string userName)
        {

            if (IsAuthenticatedUser(userName))
            {
                var postitem = new PostItem(dbPath);
                var listOfPosts = postitem.GetAllPosts(dbPath);
                return listOfPosts;
            }

            return Unauthorized();
        }

        // GET api/values/5
        [HttpGet("GetPostItemById{PostItemId}/{UserName}")]
        public ActionResult<PostItem> GetPostItemById(int postItemId, string userName)
        {
            if (IsAuthenticatedUser(userName))
            {
                var postItem = new PostItem(dbPath);
                return postItem.GetPostById(postItemId);
            }
            return Unauthorized();
        }

        // POST api/values
        [HttpPost("CreatePostItem{itemMessage}/{userName}")]
        public ActionResult CreatePostItem(string itemMessage, string userName)
        {

            if (IsAuthenticatedUser(userName))
            {
                var postItem = new PostItem(dbPath);
                var user = new User(dbPath);
                var currentUser = user.GetUserByName(userName);
                if (postItem.CreatePost(currentUser.UserId, itemMessage))
                {
                    return Ok();
                }
                return BadRequest("Could not add item!");
            }

            return Unauthorized();
        }

        // PUT api/values/5
        [HttpPut("UpdatePostItem{inputPostitem}/{userName}")]                         
        public ActionResult<PostItem> UpdatePostItem(PostItem inputPostitem, string userName)
        {

            if (IsAuthenticatedUser(userName))
            {
                var postItem = new PostItem(dbPath);
                var user = new User(dbPath);
                var currentUser = user.GetUserByName(userName);
                return postItem.UpdatePost(inputPostitem.PostItemId, currentUser.UserId, inputPostitem.ItemMessage);
            }

            return Unauthorized();

        }

        // DELETE api/values/5
        [HttpDelete("DeletePostItem{postItemId}/{userName}")]
        public ActionResult DeletePostItem(int postItemId, string userName)
        {
            if(IsAuthenticatedUser(userName))
            {
                var user = new User(dbPath);
                var currentUser = user.GetUserByName(userName);

                var postItem = new PostItem(dbPath);

                if (postItem.DeletePost(currentUser.UserId, postItemId))
                {
                    return NoContent();
                }

                return BadRequest("Delete of postItemId:" + postItemId + " could not be performed!");               
            }

            return Unauthorized();

        }

        [HttpPost("LikePost{postItemId}/{userName}")]
        public ActionResult LikePost(int postItemId, string userName) 
        {
            if (IsAuthenticatedUser(userName))
            {
                var user = new User(dbPath);
                var currentUser = user.GetUserByName(userName);

                var like = new Like(dbPath);

                if (like.AddLike(postItemId, currentUser.UserId))
                {
                    return Ok();
                }

                return BadRequest("Could not like postitem with id:" + postItemId);
            }

            return Unauthorized();
        }

        [HttpDelete("DeleteLike{postItemId}/{userName}")]
        public ActionResult DeleteLike(int postItemId, string userName)
        {
            if (IsAuthenticatedUser(userName))
            {
                var user = new User(dbPath);
                var currentUser = user.GetUserByName(userName);

                var like = new Like(dbPath);

                if (like.DeleteLike(postItemId, currentUser.UserId))
                {
                    return Ok();
                }

                return BadRequest("Could not delete like on postItemId:" + postItemId + " and user:" + currentUser.UserName);
            }

            return Unauthorized();
        }

        private bool IsAuthenticatedUser(string userName)
        {
            var user = new User(dbPath);

            if (user.GetUserByName(userName) != null)
                return true;

            return false;
        }
    }
}
