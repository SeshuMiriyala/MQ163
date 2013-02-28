
namespace MQ163.External.Facebook
{
    public interface IFacebookPostData
    {
        string Message { get; set; }
        object Tags { get; set; }
        string PictureUrl { get; set; }
        string AccessToken { get; set; }

        dynamic GetPostObject();
    }
}
