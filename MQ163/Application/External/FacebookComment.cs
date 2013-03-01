using System;

namespace MQ163.External.Facebook
{
    public class FacebookComment
    {
        /// <summary>
        /// Facebook Profile of the User who commented on the post
        /// </summary>
        public IFacebookProfile User
        {
            get;
            set;
        }

        /// <summary>
        /// Commented Text
        /// </summary>
        public string CommentText
        {
            get;
            set;
        }

        /// <summary>
        /// Is User liked the post
        /// </summary>
        public Nullable<bool> IsSupportive
        {
            get;
            set;
        }

        /// <summary>
        /// Facebook Comment ID
        /// </summary>
        public string Id
        {
            get;
            set;
        }

        /// <summary>
        /// Comment Creation Date and Time
        /// </summary>
        public DateTime CreatedDateAndTime
        {
            get;
            set;
        }
    }
}
