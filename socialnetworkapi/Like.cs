using System;
using System.Collections.Generic;

namespace SocialNetworkApi
{
    public class Like
    {
        public int LikeCounter { get; set; }

        public List<int> ListOfLikes = new List<int>();

        public Like()
        {

        }

        public void AddLike(int postItemId, int userId, string dbPath)
        {
            ListOfLikes.Add(postItemId);
        }

        public void blBSLBSS ()
        {

        }
    }
}
