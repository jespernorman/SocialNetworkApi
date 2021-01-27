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

        public LikeRepository LikeRepository { get; set; }

        public Like()
        {

        }

        public Like(string dbPath)
        {
            LikeRepository = new LikeRepository(dbPath);
        }

        public bool AddLike(int postItemId,int userId)
        {
            return LikeRepository.AddLikeToPost(postItemId, userId);
            
        }

        public bool DeleteLike(int postItemId, int userId)
        {
            return LikeRepository.DeleteLike(postItemId, userId);

        }

        public List<Like> GetLikesByPostId(int postItemId)
        {
            return LikeRepository.GetLikesByPostItemId(postItemId);
        }
    }
}