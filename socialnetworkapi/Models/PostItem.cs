using System;
using System.Collections.Generic;

namespace SocialNetworkApi.Models
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

        public PostItem()
        {

        }
    }
}
