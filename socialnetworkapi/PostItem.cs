using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace SocialNetworkApi
{
    public class PostItem
    {
        /// <summary>
        /// Holds the id of the post
        /// </summary>
        public int PostItemId { get; set; }

        /// <summary>
        /// userId that created the post
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Contains the posted message
        /// </summary>
        public string ItemMessage { get; set; }

        /// <summary>
        /// The create date of the post
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Modified date
        /// </summary>
        public DateTime ModifiedDate { get; set; }


        /// <summary>
        /// Property to User (one - one relation)
        /// </summary>
        public User UserCreator { get; set; }


        /// <summary>
        /// Collection of like objects (one - many relation)
        /// </summary>
        public List<Like> ListOfLikes = new List<Like>();

        [JsonIgnore]
        public PostRepository PostRepository { get; set; }

        public PostItem(string dbPath)
        {
            PostRepository = new PostRepository(dbPath);
        }

        public PostItem()
        {
            
        }

        /// <summary>
        /// Returns all posts
        /// </summary>
        /// <param name="dbPath"></param>
        /// <returns></returns>
        public List<PostItem> GetAllPosts(string dbPath)
        {
            var postItems = PostRepository.LoadAllPosts();

            foreach (var postItem in postItems)
            {
                var like = new Like();
                var LikeRepository = new LikeRepository(dbPath);

                postItem.ListOfLikes = LikeRepository.GetAllLikes(postItem.PostItemId);
            }

            return postItems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="itemMessage"></param>
        /// <returns></returns>
        public bool CreatePost(int userId, string itemMessage)
        {
            return PostRepository.AddPost(userId, itemMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postItemId"></param>
        /// <param name="userId"></param>
        /// <param name="itemMessage"></param>
        /// <returns></returns>
        public PostItem UpdatePost(int postItemId, int userId, string itemMessage)
        {
            return PostRepository.UpdatePostItem(postItemId, itemMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postItemId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeletePost(int postItemId, int userId)
        {
            return PostRepository.DeletePostItem(postItemId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postItemId"></param>
        /// <returns></returns>
        public PostItem GetPostById(int postItemId)
        {
            return PostRepository.GetPostItemById(postItemId);
        }
    }
}
