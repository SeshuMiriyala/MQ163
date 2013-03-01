
using System;
using System.Collections.Generic;
using MQ163.Application.External;
using MQ163.External.Facebook;
namespace MQ163.Application.Facade
{
    public class FacebookFacade
    {
        private IFacebookPage page = null;

        /// <summary>
        /// Activates the FaceBook Page
        /// </summary>
        public void Activate()
        {
            page = new FacebookPage();
        }

        /// <summary>
        /// Activates the FaceBook Page with new Access Token
        /// </summary>
        /// <param name="accessToken">New Access Token</param>
        private void Activate(string accessToken)
        {
            page = new FacebookPage(accessToken);
        }

        /// <summary>
        /// Gets information of all the posts of type "picture" of the Page MQ163
        /// </summary>
        /// <returns>IEnumerable of all the Posts</returns>
        public IEnumerable<IFacebookPost> GetAllPosts()
        {
            try
            {
                if (null == page)
                {
                    Activate();
                }
                return page.GetAllPosts();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Adds the post to the Page MQ163
        /// </summary>
        /// <param name="message">Message of the POst</param>
        /// <param name="picUrl">Picture URL</param>
        /// <param name="taggedUserEmail">emial ID of the User tagged in the Post</param>
        /// <returns></returns>
        public bool AddPost(string message, string picUrl, string taggedUserEmail)
        {
            try
            {
                IFacebookPostData data = new FacebookPostData();
                data.Message = message;
                data.PictureUrl = picUrl;
                data.TaggedUserEmail = taggedUserEmail;

                return page.AddPost(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the Facebook Agent to use the new access Token
        /// </summary>
        /// <param name="accessToken">New Access Token to be used</param>
        public void UpdateAccessToken(string accessToken)
        {
            try
            {
                Activate(accessToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}