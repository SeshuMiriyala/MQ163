using System;
using System.Collections.Generic;
using MQ163.External.Facebook;

namespace MQ163.Application.External
{
    internal class FacebookPage : IFacebookPage
    {
        private FacebookAgent fbAgent = null;

        public FacebookPage()
        {
            fbAgent = new FacebookAgent();
            //fbAgent.FacebookLogin();
        }

        public FacebookPage(string accessToken)
        {
            if (null == fbAgent)
                fbAgent = new FacebookAgent();
            fbAgent.AccessToken = accessToken;
        }

        #region IFacebookPage Members

        /// <summary>
        /// Posts the data on the page
        /// </summary>
        /// <param name="postObject">Data to be posted</param>
        /// <returns>Returns True: If posted successfully. 
        /// Exception: If post is unsuccessfull</returns>
        public bool AddPost(IFacebookPostData postObject)
        {
            try
            {
                if (fbAgent.IsLogged)
                {
                    return fbAgent.AddPost(postObject);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets all the News Feeds of the MQ163 page with type "Picture"
        /// </summary>
        /// <returns>Returns all the posts of the page</returns>
        public IEnumerable<IFacebookPost> GetAllPosts()
        {
            try
            {
                if (fbAgent.IsLogged)
                {
                    return fbAgent.GetAllFeeds();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}