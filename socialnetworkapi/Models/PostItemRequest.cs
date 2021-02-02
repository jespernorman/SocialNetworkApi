using System;
namespace SocialNetworkApi.Models
{
    public class PostItemRequest
    {
        public PostItemRequest()
        {
        }

        /// <summary>
        /// Contains the posted message
        /// </summary>
        public string ItemMessage { get; set; }

    }
}
