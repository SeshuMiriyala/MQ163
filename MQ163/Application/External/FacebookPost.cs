using System.Collections.Generic;
using MQ163.External.Facebook;

namespace MQ163.Application.External
{
    public class FacebookPost : IFacebookPost
    {
        #region IFacebookPost Members

        public IEnumerable<IFacebookProfile> Likes
        {
            get;
            set;
        }

        public IEnumerable<IFacebookProfile> Reposts
        {
            get;
            set;
        }

        public IEnumerable<FacebookComment> Comments
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public string PostPicture
        {
            get;
            set;
        }

        public string PostText
        {
            get;
            set;
        }

        public IEnumerable<IFacebookProfile> Tags
        {
            get;
            set;
        }

        #endregion
    }
}