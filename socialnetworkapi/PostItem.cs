using System;
using System.Collections.Generic;

namespace SocialNetworkApi
{
    public class PostItem
    {
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime Like { get; set; }
        public DateTime LikedByUserId { get; set; }
        public int PostItemId { get; set; }
        public int UserId { get; set; }
        public string ItemMessage { get; set; }

        public List<Like> ListOfLikes = new List<Like>();


        public PostRepository PostRepository { get; set; }


        public PostItem(string dbPath)
        {
            PostRepository = new PostRepository(dbPath);
        }

        public List<PostItem> GetAllPosts()
        {
            var postItems = PostRepository.LoadAllPosts();

            foreach (var postItem in postItems)
            {
                var like = new Like();
                postItem.ListOfLikes = like.GetAllLikes(postItem.PostItemId);
            }

            return postItems;
        }

        public bool CreatePost(User user, string itemMessage)
        {
            return PostRepository.AddPost(user, itemMessage);
        }

        public bool UpdatePost(int postItemId, string itemMessage)
        {
            return PostRepository.UpdatePostItem(postItemId, itemMessage); 

        }

        public bool DeletePost(int postItemId)
        {
            return PostRepository.DeletePostItem(postItemId);
        }

        public void LikePost(int postItemId, int userId)
        {
            var likes = new Like();
            likes.AddLike(postItemId);

        }

        public bool DeleteLike()
        {
            return true;
        }

    }
}
