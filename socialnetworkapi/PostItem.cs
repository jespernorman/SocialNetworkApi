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
        public int Id { get; internal set; }

        public List<int> ListOfLikes = new List<int>();


        public PostRepository PostRepository { get; set; }


        public PostItem(string dbPath)
        {
            PostRepository = new PostRepository(dbPath);
        }

        public List<PostItem> GetAllPosts()
        {
            return PostRepository.LoadAllPosts();
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

        public void LikePost(int postItemId)
        {
            var likes = new Likes();
            likes.AddLike(postItemId);

        }

        public bool DeleteLike()
        {

        }

    }
}
