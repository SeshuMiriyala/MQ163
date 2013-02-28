
using System.Collections.Generic;
using MQ163.Application.External;
using MQ163.External.Facebook;
namespace MQ163.Application.Facade
{
    public class FacebookFacade
    {
        private IFacebookPage page = null;
        public void Activate()
        {
            page = new FacebookPage();
        }

        public IEnumerable<IFacebookPost> GetAllPosts()
        {
            if (null == page)
            {
                Activate();
            }
            return page.GetAllPosts();
        }

        public bool AddPost(string message, string picUrl, string taggedUserName)
        {
            IFacebookPostData data = new FacebookPostData();
            data.Message = message;
            data.PictureUrl = picUrl;
            data.Tags = new[] { new { tag_uid = taggedUserName, x = 1, y = 1 } };

            return page.AddPost(data);
        }
    }
}