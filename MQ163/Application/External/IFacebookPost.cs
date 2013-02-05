using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Collections.Generic;

namespace MQ163.External.Facebook
{
    public interface IFacebookPost
    {
        System.Collections.Generic.IEnumerable<IFacebookProfile> Likes
        {
            get;
            set;
        }

        System.Collections.Generic.IEnumerable<IFacebookProfile> Reposts
        {
            get;
            set;
        }

        IEnumerable<FacebookComment> Comments
        {
            get;
            set;
        }

        string Id
        {
            get;
            set;
        }

        string PostPicture
        {
            get;
            set;
        }

        string PostText
        {
            get;
            set;
        }

        IEnumerable<IFacebookProfile> Tags
        {
            get;
            set;
        }

        IFacebookPost GetPostData(string postId);
    }
}
