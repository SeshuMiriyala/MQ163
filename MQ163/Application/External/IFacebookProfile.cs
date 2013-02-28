
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

        string UserName
        {
            get;
            set;
        }

        void GetPublicDetails();
    }
}
