using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace MQ163.External.Facebook
{
    public interface IFacebookProfile
    {
        string Id
        {
            get;
            set;
        }

        string FirstName
        {
            get;
            set;
        }

        string LastName
        {
            get;
            set;
        }

        string ProfilePicture
        {
            get;
            set;
        }
    
        void GetPublicDetails();
    }
}
