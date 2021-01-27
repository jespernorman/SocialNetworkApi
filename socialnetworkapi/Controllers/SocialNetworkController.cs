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

        private const string dbPath = "socialnetworkapi/Database/SocialNetwork.db";

        // GET: api/values
        [HttpGet("GetAllPosts")]
        public IEnumerable<PostItem> GetAllPostItems(string userName, int userId)
        {

            if (IsAuthenticatedUser(userName))
            {
                var postitem = new PostItem(dbPath);
                var listOfPosts = postitem.GetAllPosts(dbPath);
                return listOfPosts;
            }

            return null;
        }

        // GET api/values/5
        [HttpGet("GetPostItemBYId{PostItemId}/{UserName}")]
        public void GetPostItemById(int postItemId, string userName)
        {
            var postItem = new PostItem(dbPath);
            postItem.GetPostById(postItem, dbPath);
        }

        // POST api/values
        [HttpPost("CreatePostItem{PostItem}/{ItemMessage}/{UserName}")]
        public void CreatePostItem(string itemMessage, string userName)          //void?
        {
            var user = new User(dbPath);
            var postItem = new PostItem(dbPath);
            user.GetUserByName(userName, dbPath);

            postItem.CreatePost(user, itemMessage);
        }

        // PUT api/values/5
        [HttpPut("UpdatePostItem{PostItem}/{UserName}")]                         //ska username ha stort u eller ej?     //Åter igen void eller vad?
        public void UpdatePostItem(PostItem post, string userName, int userId)
        {
            var postItem = new PostItem(dbPath);

            int postItemId = 0;
            var itemMessage = "";

            postItem.UpdatePost(postItemId, userId, itemMessage, dbPath);
        }

        // DELETE api/values/5
        [HttpDelete("DeletePostItem{PostItemId}/{UserName}")]
        public void DeletePostItem(int postItemId, int userId, string userName)
        {
            var user = new User(dbPath);
            user.GetUserByName(userName, dbPath);

            var postItem = new PostItem(dbPath);
            postItem.GetPostByUser(postItemId, userId, dbPath);

            postItem.DeletePost(userId, postItemId, dbPath);

        }

        [HttpPost("LikePost{PostItemId}/{UserName}")]
        public void LikePost(int postItemId, string userName, int userId)       //PostItem?
        {
            var postItem = new PostItem(dbPath);
            var Like = new Like();
            var user = new User(dbPath);

            postItem.LikePost(postItemId,userName, userId, dbPath);

            var like = new Like();
            like.AddLike(like, dbPath);
        }

        [HttpDelete("DeleteLike{PostItemId}/{UserName}")]
        public void DeleteLike(int postItemId, string userName, int userId)
        {
            var user = new User(dbPath);


            var postItem = new PostItem(dbPath);
            postItem.DeleteLike(postItemId, userId, dbPath);
        }

        private bool IsAuthenticatedUser(string userName)
        {
            var user = new User(dbPath);

            if (user.GetUserByName(userName, dbPath) != null)
                return true;

            return false;
        }
    }
}
