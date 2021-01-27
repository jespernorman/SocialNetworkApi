using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SocialNetworkApi
{
    public class PostItem
    {
        public int PostItemId { get; set; }
        public User UserCreator { get; set; }
        public string ItemMessage { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public List<PostItem> listOfPosts = new List<PostItem>();  
        public List<Like> ListOfLikes = new List<Like>();


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

        public PostItem CreatePost(PostItem postItem)
        {
            return PostRepository.AddPost(postItem);
        }

        public bool UpdatePost(int postItemId, int userId, string itemMessage, string dbPath)
        {
            var postItem = new PostItem(dbPath);
            postItem.GetPostByUser(postItemId, userId, dbPath);

            PostRepository.UpdatePostItem(postItemId, itemMessage);
            return true;
        }

        public void DeletePost(int postItemId, int userId, string dbPath)
        {
            var postItem = new PostItem(dbPath);
            var postRepository = new PostRepository(dbPath);

            postItem.GetPostByUser(postItemId, userId, dbPath);
            postRepository.DeletePostItem(postItemId);
        }

        public bool LikePost(int postItemId, string userName, int userId, string dbPath)
        {
            var likeRepository = new LikeRepository(dbPath);
            var like = new Like();

            if (ListOfLikes.Any(x => x.UserId == userId && x.PostItemId == postItemId && like.LikeId == like.LikeId))               
            {
                return false;
            }
            else
            {
                ListOfLikes.Add(like);
                like.AddLike(like, dbPath);
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

        public bool DeleteLike(int postItemId, int userId, string dbPath)
        {
            var likeRepository = new LikeRepository(dbPath);
            var like = new Like();

            if (ListOfLikes.Any(x => x.UserId == userId && x.PostItemId == postItemId && like.LikeId == like.LikeId))
            {
                likeRepository.RemoveLike(postItemId, userId, like.LikeId);
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetPostByUser(int postItemId,int userId, string dbPath)
        {
            var postItems = PostRepository.LoadAllPosts();
            var user = new User(dbPath);

            if (postItems.Any(x => x.UserId == userId && x.PostItemId == postItemId))
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public PostItem GetPostById(PostItem postitem, string dbPath)
        {
            var postItem = new PostItem(dbPath);

            if(listOfPosts.Contains(postItem))
            {
                return postItem;
            }
            else
            {
                return null;
            }
        }
    }
}
