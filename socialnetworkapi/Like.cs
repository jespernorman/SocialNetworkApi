using System;
using System.Collections.Generic;

namespace SocialNetworkApi
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
        /// UserName of the User that made a like
        /// </summary>
        public int UserName { get; set; }

        /// </summary>
        /// UserId of the User that made a like
        /// </summary>
        public int UserId { get; set; }

        public LikeRepository LikeRepository { get; set; }

        public Like()
        {

        }

        public Like(string dbPath)
        {
            LikeRepository = new LikeRepository(dbPath);
        }

        /// </summary>
        /// Adds like to a Post from a User
        /// </summary>
        /// <param name="postItemId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AddLike(int postItemId,int userId)
        {
            return LikeRepository.AddLikeToPost(postItemId, userId);
            
        }

        /// </summary>
        /// Deletes a like that a User has made
        /// </summary>
        /// <param name="postItemId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DeleteLike(int postItemId, int userId)
        {
            return LikeRepository.DeleteLike(postItemId, userId);

        }

        /// </summary>
        /// Get likes of a specifik post by its postId
        /// </summary>
        /// <param name="postItemId"></param>
        /// <returns></returns>
        public List<Like> GetLikesByPostId(int postItemId)
        {
            return LikeRepository.GetLikesByPostItemId(postItemId);
        }
    }
}