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

        private const string dbPath = "../../../Database/SocialNetwork.db";

        // GET: api/values
        [HttpGet("GetAllPosts")]
        public IEnumerable<PostItem> GetAllPostItems(string userName)
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
        [HttpGet("GetPostItemBYId{id}/{UserName}")]
        public string GetPostItemById(int id, string userName)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("CreatePostItem{PostItem}/{UserName}")]
        public void CreatePostItem(PostItem post, string userName)          //void?
        {
            var user = new User(dbPath);
            user.GetUserByName(userName, dbPath);

            string itemMessage = "";
            //Console.WriteLine("Skriv in vad du vill posta");
            //string itemMessage = Console.ReadLine();

            var postItem = new PostItem(dbPath);
            postItem.CreatePost(user, itemMessage);
        }

        // PUT api/values/5
        [HttpPut("UpdatePostItem{PostItem}/{UserName}")]                //ska username ha stort u eller ej?     //Åter igen void eller vad?
        public void UpdatePostItem(PostItem post, string userName, int userId)
        {
            var postItem = new PostItem(dbPath);

            //Console.WriteLine("Mata in post ID:t på posten du vill redigera");

            int postItemId = 0;

            //string postItemIdText = Console.ReadLine();
            //int postItemId = int.Parse(postItemIdText);

            //Console.WriteLine("Skriv in det du vill skriva istället");
            //var itemMessage = Console.ReadLine();
            var itemMessage = "";

            postItem.UpdatePost(postItemId, userId, itemMessage, dbPath);
        }

        // DELETE api/values/5
        [HttpDelete("DeletePostItem{id}/{UserName}")]
        public void DeletePostItem(int postItemId, int userId, string userName)
        {
            var user = new User(dbPath);
            user.GetUserByName(userName, dbPath);

            var postItem = new PostItem(dbPath);
            postItem.GetPostByUser(postItemId, userId, dbPath);

            postItem.DeletePost(postItemId);

        }

        [HttpPost("LikePost{PostItem}/{UserName}")]
        public void LikePost(int postItemId, int userId)       //PostItem?
        {
            var postItem = new PostItem(dbPath);
            var Like = new Like();

            //Like.GetLikeOnPost(dbPath, userId, postitemId);

            postItem.LikePost(postItemId, userId);

            var like = new Like();
            like.AddLike(postItemId, userId, dbPath);

        }

        [HttpDelete("DeleteLike{id}/{UserName}")]
        public void DeleteLike(int postItemId, int userId)
        {
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
