using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Facebook;

namespace Facebook1.Controllers
{
    public class UserProfileController : ApiController
    {
        public object GetFacebookUserData(string key)
        {
            var client = new FacebookClient(key);
            return client.Get("me");
        }
    }
}
