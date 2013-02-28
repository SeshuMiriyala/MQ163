using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Facebook;
using MQ163.External.Facebook;

namespace MQ163.Application.External
{
    public class FacebookAgent
    {
        private FacebookClient client = new FacebookClient();
        private string baseUrl = "https://graph.facebook.com";
        private bool isPosted = false;
        public string AccessToken { get; private set; }
        public bool IsLogged { get; set; }

        public string FacebookLogin()
        {
            dynamic parameters = new ExpandoObject();
            string appId = ConfigurationManager.AppSettings["Facebook:AppID"].ToString();
            string appSecret = ConfigurationManager.AppSettings["Facebook:AppSecret"].ToString();

            parameters.client_id = appId;
            parameters.response_type = "token";
            parameters.display = "popup";
            parameters.scope = "manage_pages";
            return (client.GetLoginUrl(parameters));
        }

        public IEnumerable<IFacebookPost> GetAllFeeds()
        {
            var json = new JavaScriptSerializer();
            List<IFacebookPost> postsList = new List<IFacebookPost>();
            string Url = string.Format("{0}/MQ163/feed?filter={2}&access_token={1}", baseUrl, AccessToken, "app_2305272732");

            using (var webClient = new WebClient())
            {
                string data = webClient.DownloadString(Url);
                var feeds = (Dictionary<string, object>)json.DeserializeObject(data);
                object[] feedsArray = (object[])feeds.FirstOrDefault(p => p.Key == "data").Value;
                foreach (object feed in feedsArray)
                {
                    IFacebookPost post = null;
                    Dictionary<string, object> feed2 = (Dictionary<string, object>)feed;
                    if (feed2.Keys.Contains("message") && null != feed2["message"] && "photo" == feed2["type"].ToString())
                    {
                        post = new FacebookPost();
                        post.PostText = feed2["message"].ToString();
                        post.Id = feed2["id"].ToString();
                        post.PostPicture = feed2["picture"].ToString();
                        post.Likes = GetAllLikesForPost(feed2["id"].ToString());
                        post.Comments = GetAllCommentsForPost(feed2["id"].ToString());
                        postsList.Add(post);
                    }
                }
            }
            return postsList;
        }

        public bool AddPost(IFacebookPostData postData)
        {
            string accessToken = GetPageAccessToken();
            string albumID = GetAlbumID();
            var fb = new FacebookClient();
            string error = null;
            postData.AccessToken = accessToken;
            string path = string.Format("{0}/photos?access_token={1}", albumID, accessToken);
            dynamic publishResponse = client.PostTaskAsync(path, postData.GetPostObject());

            while (publishResponse.Status == TaskStatus.WaitingForActivation) ;
            if (publishResponse.Status == TaskStatus.RanToCompletion)
                return true;
            else if (publishResponse.Status == TaskStatus.Faulted)
            {
                error = (((Exception)publishResponse.Exception).InnerException).Message;
                return false;
            }
            return false;
        }

        private string GetAlbumID()
        {
            string pageName = ConfigurationManager.AppSettings["Facebook:PageName"].ToString();
            string albumName = ConfigurationManager.AppSettings["Facebook:AlbumName"].ToString();
            var json = new JavaScriptSerializer();
            string albumId = null;
            string oauthUrl1 = string.Format("https://graph.facebook.com/{1}/albums?access_token={0}", AccessToken, pageName);

            using (var webClient = new WebClient())
            {
                string data = webClient.DownloadString(oauthUrl1);
                var albums = (Dictionary<string, object>)json.DeserializeObject(data);
                object[] albums2 = (object[])albums.FirstOrDefault(p => p.Key == "data").Value;
                foreach (object album in albums2)
                {
                    Dictionary<string, object> album2 = (Dictionary<string, object>)album;
                    if (album2["name"].ToString() == "Test_Album")
                    {
                        albumId = album2["id"].ToString();
                        break;
                    }
                }
            }
            return albumId;
        }

        private string GetPageAccessToken()
        {
            dynamic me = client.Get("/me/accounts");
            dynamic pages1 = me.data;
            object[] pages21 = null;// (object[])pages1.FirstOrDefault(p => p.Key == "data").Value;
            string accessToken = null;
            string pageId1 = null;
            foreach (dynamic page in pages1)
            {
                if (page.name == "MQ163")
                {
                    pageId1 = page.id;
                    accessToken = page.access_token;
                    break;
                }
            }
            return accessToken;
        }

        private IEnumerable<FacebookComment> GetAllCommentsForPost(string postId)
        {
            string Url = string.Format("{0}/{2}/comments?access_token={1}", baseUrl, AccessToken, postId);
            var json = new JavaScriptSerializer();
            List<FacebookComment> commentsList = new List<FacebookComment>();

            using (var webClient = new WebClient())
            {
                string data = webClient.DownloadString(Url);
                var comments = (Dictionary<string, object>)json.DeserializeObject(data);
                object[] commentsArray = (object[])comments.FirstOrDefault(p => p.Key == "data").Value;
                if (commentsArray.Count() > 0)
                {
                    foreach (object comment in commentsArray)
                    {
                        FacebookComment facebookComment = null;
                        Dictionary<string, object> comment2 = (Dictionary<string, object>)comment;
                        if (comment2.Keys.Contains("message") && null != comment2["message"])
                        {
                            facebookComment = new FacebookComment();
                            facebookComment.CommentText = comment2["message"].ToString();
                            facebookComment.Id = comment2["id"].ToString();
                            facebookComment.CreatedDateAndTime = null != comment2["created_time"] ? Convert.ToDateTime(comment2["created_time"].ToString()) : DateTime.MinValue;
                            Dictionary<string, object> commentedUser = (Dictionary<string, object>)comment2["from"];
                            if (commentedUser.Keys.Contains("name") && null != commentedUser["name"])
                            {
                                facebookComment.User = GetUserProfile(commentedUser["id"].ToString());
                            }
                            facebookComment.IsSupportive = (null != comment2["user_likes"] ? Convert.ToBoolean(comment2["user_likes"].ToString()) : false);
                        }
                    }
                }
            }
            return commentsList;
        }

        private IEnumerable<IFacebookProfile> GetAllLikesForPost(string postId)
        {

            string Url = string.Format("{0}/{2}/likes?access_token={1}", baseUrl, AccessToken, postId);
            var json = new JavaScriptSerializer();
            List<IFacebookProfile> profilesList = new List<IFacebookProfile>();

            using (var webClient = new WebClient())
            {
                string data = webClient.DownloadString(Url);
                var likes = (Dictionary<string, object>)json.DeserializeObject(data);
                object[] likesArray = (object[])likes.FirstOrDefault(p => p.Key == "data").Value;
                if (likesArray.Count() > 0)
                {
                    IFacebookProfile profile = null;
                    foreach (object like in likesArray)
                    {
                        Dictionary<string, object> like2 = (Dictionary<string, object>)like;
                        if (like2.Keys.Contains("name") && null != like2["name"])
                        {
                            profile = GetUserProfile(like2["id"].ToString());
                        }
                        profilesList.Add(profile);
                    }
                }
            }
            return profilesList;
        }

        private IFacebookProfile GetUserProfile(string profileId)
        {
            string Url = string.Format("{0}/{2}?access_token={1}", baseUrl, AccessToken, profileId);
            var json = new JavaScriptSerializer();
            IFacebookProfile profile = null;

            using (var webClient = new WebClient())
            {
                string data = webClient.DownloadString(Url);
                var userProfile = (Dictionary<string, object>)json.DeserializeObject(data);
                if (userProfile.Count() > 0)
                {
                    profile = new FacebookProfile();
                    profile.Id = userProfile["id"].ToString();
                    profile.FirstName = userProfile["first_name"].ToString();
                    profile.LastName = userProfile["last_name"].ToString();
                    profile.ProfilePicture = GetProfilePictureURL(userProfile["id"].ToString());
                    profile.UserName = userProfile["username"].ToString();
                }
            }
            return profile;
        }

        private string GetProfilePictureURL(string profileId)
        {
            string Url = string.Format("{0}/{2}/picture?access_token={1}", baseUrl, AccessToken, profileId);
            var json = new JavaScriptSerializer();
            string profilePictureUrl = null;

            using (var webClient = new WebClient())
            {
                profilePictureUrl = webClient.DownloadString(Url);
            }
            return profilePictureUrl;
        }
    }
}