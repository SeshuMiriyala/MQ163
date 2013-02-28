using System.Collections.Generic;
using MQ163.External.Facebook;

namespace MQ163.Application.External
{
    public class FacebookPage : IFacebookPage
    {
        private FacebookAgent fbAgent = null;

        public FacebookPage()
        {
            fbAgent = new FacebookAgent();
            fbAgent.FacebookLogin();
        }

        #region IFacebookPage Members

        public bool AddPost(IFacebookPostData postObject)
        {
            if (fbAgent.IsLogged)
            {
                //return fbAgent.GetAllFeeds();
            }
            return false;
        }

        public IEnumerable<IFacebookPost> GetAllPosts()
        {
            if (fbAgent.IsLogged)
            {
                return fbAgent.GetAllFeeds();
            }
            return null;
        }

        #endregion
    }
}