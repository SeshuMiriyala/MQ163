using MQ163.External.Facebook;

namespace MQ163.Application.External
{
    public class FacebookProfile : IFacebookProfile
    {
        public FacebookProfile()
        {

        }

        #region IFacebookProfile Members

        public string Id
        {
            get;
            set;
        }

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string ProfilePicture
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        #endregion

    }
}