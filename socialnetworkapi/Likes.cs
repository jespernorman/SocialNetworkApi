using System;
using System.Collections.Generic;

namespace SocialNetworkApi
{
    public class Likes
    {
        public int LikeCounter { get; set; }

        public List<int> ListOfLikes = new List<int>();

        public Likes()
        {

        }

        public void AddLike(int postItemId)
        {
            ListOfLikes.Add(postItemId);
        }
    }
}
