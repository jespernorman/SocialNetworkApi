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
        // GET: api/values
        [HttpGet("GetAllPosts")]
        public IEnumerable<PostItem> GetAllPostItems(string userName)
        {
            var postitem = new PostItem(dbPath);

            var listOfPosts = new List<PostItem>();

            new PostItem { Id = 1, CreateDate = DateTime.Now, ModifiedDate = DateTime.Now, UserId = 1, ItemMessage = "First text message" };
        
            return listOfPosts;
        }

        // GET api/values/5
        [HttpGet("GetPostItemBYId{id}")]
        public string GetPostItemById(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("CreatePostItem{PostItem}/{UserName}")]
        public string CreatePostItem(PostItem post, string userName)
        {
            return "value";
        }

        // PUT api/values/5
        [HttpPut("UpdatePostItem{PostItem}/{UserName}")]//ska username ha stort u eller ej?
        public string UpdatePostItem(PostItem post, string userName)
        {
            return "value";
        }

        // DELETE api/values/5
        [HttpDelete("DeletePostItem{id}/{UserName}")]
        public string DeletePostItem(int id, string userName)
        {
            return "value";
        }

        [HttpPost("LikePost{PostItem}/{UserName}")]
        public string LikePost(PostItem post, string userName)
        {
            return "value";
        }

        [HttpDelete("DeleteLike{id}/{UserName}")]
        public string DeleteLike(int id, string userName)
        {
            return "value";
        }
    }
}
