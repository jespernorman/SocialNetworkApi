using System;
using System.Collections.Generic;
using SocialNetworkApi.Repositorys;

namespace SocialNetworkApi.Models
{
    public class Like
    {
        /// </summary>
        /// Id of the specifik Like
        /// </summary>
        public int LikeId { get; set; }

        /// </summary>///
        /// Id of specifikPost
        /// </summary>       
        public int PostItemId { get; set; }

        /// </summary>
        /// UserId of the User that made a like
        /// </summary>
        public int UserId { get; set; }

        public Like()
        {

        }
    }
}