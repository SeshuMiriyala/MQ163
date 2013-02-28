using System;

namespace MQ163.External.Facebook
{
    public class FacebookComment
    {
        public IFacebookProfile User
        {
            get;
            set;
        }

        public string CommentText
        {
            get;
            set;
        }

        public Nullable<bool> IsSupportive
        {
            get;
            set;
        }

        public string Id
        {
            get;
            set;
        }

        public DateTime CreatedDateAndTime
        {
            get;
            set;
        }
    }
}
