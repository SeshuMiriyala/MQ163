
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
        public object Tags { get; set; }
        public string PictureUrl { get; set; }
        public string AccessToken { get; set; }

        public dynamic GetPostObject()
        {
            dynamic parameters = new ExpandoObject();
            parameters.message = Message;
            parameters.tags = Tags;
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