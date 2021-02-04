using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkApi.Models;
using SocialNetworkApi.Repositorys;
using SocialNetworkApi.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SocialNetworkApi.Controllers
{
    [Route("api/[controller]")]
    public class LikesController : Controller
    {
        private readonly string dbPath = Environment.CurrentDirectory + "/databas/SocialNetwork.db";

        /// <summary>
        /// Injecting repositories
        /// </summary>
        private readonly PostRepository postRepository;
        private readonly LikeRepository likeRepository;
        private readonly UserRepository userRepository;

        public LikesController()
        {
            postRepository = new PostRepository(dbPath);
            likeRepository = new LikeRepository(dbPath);
            userRepository = new UserRepository(dbPath);
        }

        /// <summary>
        /// Adds like to a specific post from user
        /// </summary>
        /// <param name="likeRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddLike(LikeRequest likeRequest)
        {
            if (Authenticate.IsAuthenticatedUser(userRepository, likeRequest.UserName))
            {
                var currentUser = userRepository.GetByUserName(likeRequest.UserName);
                var currentPostItem = postRepository.GetPostItemById(likeRequest.PostItemId);

                if (currentUser != null && currentPostItem != null)
                {
                    if (!currentPostItem.ListOfLikes.Any(x => x.PostItemId == likeRequest.PostItemId && x.UserId == currentUser.UserId))
                    {
                        if (likeRepository.AddLikeToPost(currentPostItem, currentUser))
                        {
                            return Ok();
                        }

                        return BadRequest("Could not add like to database.");
                    }
                }

                return BadRequest("Could not like due to bad data!");
            }

            return Unauthorized();
        }

        /// <summary>
        /// Deletes like for a specific postitem and user
        /// </summary>
        /// <param name="likeRequest"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeleteLike(LikeRequest likeRequest)
        {
            if (Authenticate.IsAuthenticatedUser(userRepository, likeRequest.UserName))
            {
                var currentUser = userRepository.GetByUserName(likeRequest.UserName);
                var currentPostItem = postRepository.GetPostItemById(likeRequest.PostItemId);

                if (currentUser != null && currentPostItem != null)
                {
                    if (currentPostItem.ListOfLikes.Any(x => x.PostItemId == likeRequest.PostItemId && x.UserId == currentUser.UserId))
                    {
                        if (likeRepository.DeleteLike(currentPostItem, currentUser))
                        {
                            return Ok();
                        }
                    }
                }

                return BadRequest("Could not delete like due to bad data!");
            }

            return Unauthorized();
        }
    }
}
