using System;
using System.Collections.Generic;

namespace SocialNetworkApi
{
    public class Like
    {
        public int LikeId { get; set; }
        public int PostItemId { get; set; }
        public int UserName { get; set; }
        public int UserId { get; set; }

        //public List<Like> ListOfLikes = new List<Like>();

        public Like()
        {

        }

        public void AddLike(Like like, string dbPath)
        {
            var likeRepository = new LikeRepository(dbPath);
            likeRepository.AddLikeToPost(like);
            
        }

        public void RemoveLike()
        {

        }
    }
}
