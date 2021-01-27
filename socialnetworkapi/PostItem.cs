using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace SocialNetworkApi
{
    public class PostItem
    {
        public int PostItemId { get; set; }
        public int UserId { get; set; }
        public string ItemMessage { get; set; }
        public DateTime CreateDate { get; set; }
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

        public bool CreatePost(int userId, string itemMessage)
        {
            return PostRepository.AddPost(userId, itemMessage);
        }

        public PostItem UpdatePost(int postItemId, int userId, string itemMessage)
        {
            return PostRepository.UpdatePostItem(postItemId, itemMessage);
        }

        public bool DeletePost(int postItemId, int userId)
        {
            return PostRepository.DeletePostItem(postItemId);
        }

        public PostItem GetPostById(int postItemId)
        {
            return PostRepository.GetPostItemById(postItemId);
        }
    }
}
