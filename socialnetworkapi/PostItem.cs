using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkApi
{
    public class PostItem
    {
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Like { get; set; }                      //Ska väl inte vara datetime?
        public DateTime LikedByUserId { get; set; }
        public int PostItemId { get; set; }
        public int UserId { get; set; }
        public string ItemMessage { get; set; }

        public List<PostItem> listOfPosts = new List<PostItem>();  
        public List<Like> ListOfLikes = new List<Like>();


        public PostRepository PostRepository { get; set; }


        public PostItem(string dbPath)
        {
            PostRepository = new PostRepository(dbPath);
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

        public bool CreatePost(User user, string itemMessage)
        {
            return PostRepository.AddPost(user, itemMessage);
        }

        public bool UpdatePost(int postItemId, string itemMessage)
        {
            return PostRepository.UpdatePostItem(postItemId, itemMessage); 

        }

        public bool DeletePost(int postItemId) // USer?
        {
            return PostRepository.DeletePostItem(postItemId);
        }

        public bool LikePost(int postItemId, int userId)
        {

            if (postItemId == PostItemId && userId == UserId == Like == true)
            {
                return false;
            }
            else
            {
                return true;
            }

            //if(ListOfLikes.Any(x => x.UserId == userId && x.PostItemId == postItemId))
            //{
            //    return false;
            //}
            //else
            //{

            //}

            //var user = new User(dbPath);
            //var listOfTheLikes = LikeRepository.GetLikesByUser(postitemId, userId);

            //likes.AddLike(postItemId);
        }

        public bool DeleteLike()
        {
            return true;
        }

        public bool GetPostByUser(int postItemId,int userId, string dbPath)
        {
            var postItems = PostRepository.LoadAllPosts();
            var user = new User(dbPath);

            if (postItems.Any(x => x.UserId == userId && x.PostItemId == postItemId))
            {
                return true;
            }
            else
            {
                return false;
            }


        }

    }
}
