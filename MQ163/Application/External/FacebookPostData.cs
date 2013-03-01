
using System.Dynamic;
using System.IO;
using Facebook;
using MQ163.External.Facebook;
namespace MQ163.Application.External
{
    public class FacebookPostData : IFacebookPostData
    {
        #region IFacebookPostData Members

        public string Message { get; set; }
        public string TaggedUserEmail { get; set; }
        public string PictureUrl { get; set; }
        public string AccessToken { get; set; }

        public dynamic GetPostObject(string userID)
        {
            dynamic parameters = new ExpandoObject();
            parameters.message = Message;
            parameters.tags = new[] { new { tag_uid = userID, x = 1, y = 1 } }; ;
            parameters.url = PictureUrl;
            parameters.source = new FacebookMediaObject
                {
                    ContentType = "image/jpeg",
                    FileName = Path.GetFileName(PictureUrl)
                }.SetValue(File.ReadAllBytes(PictureUrl));
            parameters.scope = "publish_stream,photo_upload";
            parameters.access_token = AccessToken;

            return parameters;
        }

        #endregion
    }
}