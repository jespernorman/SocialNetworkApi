using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Models;
using SocialNetworkApi.Repositorys;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly string dbPath = Environment.CurrentDirectory + "/databas/SocialNetwork.db";

        /// <summary>
        /// Injecting repositories
        /// </summary>
        private readonly PostRepository postRepository;
        private readonly LikeRepository likeRepository;
        private readonly UserRepository userRepository;

        public PostsController()
        {
            postRepository = new PostRepository(dbPath);
            likeRepository = new LikeRepository(dbPath);
            userRepository = new UserRepository(dbPath);
        }

        // GET: api/values
        /// <summary>
        /// Returns all PostItem objects
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public ActionResult<IEnumerable<PostItemResponse>> Get(string username)
        {

            if (IsAuthenticatedUser(username))
            {
                var postItems = postRepository.GetAllPosts();
                var postItemResponses = new List<PostItemResponse>();

                foreach (var postItem in postItems)
                {
                    postItem.ListOfLikes = likeRepository.GetAllLikes(postItem.PostItemId);
                    postItemResponses.Add(GetPostItemResponse(postItem));
                }

                return postItemResponses;
            }

            return Unauthorized();
        }

        /// <summary>
        /// Returns the PostItem object matching postItemId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        // GET api/values/5
        [HttpGet("{id}/{username}")]
        public ActionResult<PostItemResponse> Get(int id, string username)
        {
            if (IsAuthenticatedUser(username))
            {
                var postItem = postRepository.GetPostItemById(id);
                return GetPostItemResponse(postItem);
            }
            return Unauthorized();
        }

        // POST api/values
        /// <summary>
        /// Creates a PostItem
        /// </summary>
        /// <param name="post"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost("{username}")]
        public ActionResult CreatePost(PostItemRequest post, string username)
        {
            if (IsAuthenticatedUser(username))
            {
                var currentUser = userRepository.GetByUserName(username);
                var minCharacters = 5;
                var maxCharacters = 400;

                if (post.ItemMessage.Length < minCharacters)
                {
                    return BadRequest("Message was to short");
                }
                if (post.ItemMessage.Length > maxCharacters)
                {
                    return BadRequest("Message was too long");
                }
                else
                {
                    if (postRepository.AddPost(currentUser.UserId, post))
                    {
                        return Ok();
                    }
                    return BadRequest("Could not add item!");
                }
            }

            return Unauthorized();
        }

        // PUT api/values/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPut("{id}/{username}")]                         
        public ActionResult<PostItemResponse> UpdatePost(int id , string username, PostItemRequest post)
        {
            if (IsAuthenticatedUser(username))
            {
                var postItem = postRepository.UpdatePostItem(id, post);
                return GetPostItemResponse(postItem);
            }

            return Unauthorized();
        }

        // DELETE api/values/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpDelete("{id}/{username}")]
        public ActionResult Delete(int id, string username)
        {
            if(IsAuthenticatedUser(username))
            {
                if (postRepository.DeletePostItem(id))
                {
                    return NoContent();
                }

                return BadRequest("Delete of postItemId:" + id + " could not be performed!");               
            }

            return Unauthorized();

        }

        /// <summary>
        /// Checks if the user exists
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private bool IsAuthenticatedUser(string username)
        {
            var user = new User(dbPath);

            if (user.GetUserByName(username) != null)
                return true;

            return false;
        }

        /// <summary>
        /// Maps the PostItem object to an PostItemResponse object
        /// </summary>
        /// <param name="postItem"></param>
        /// <returns></returns>
        private static PostItemResponse GetPostItemResponse(PostItem postItem)
        {
            return new PostItemResponse
            {
                PostItemId = postItem.PostItemId,
                UserId = postItem.UserId,
                ItemMessage = postItem.ItemMessage,
                CreateDate = postItem.CreateDate,
                ModifiedDate = postItem.ModifiedDate
            };
        }
    }
}
