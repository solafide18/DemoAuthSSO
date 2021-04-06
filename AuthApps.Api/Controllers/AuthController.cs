using AuthApps.Api.Models.RequestModels;
using AuthApps.Api.Models.ResponseModels;
using AuthApps.Api.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthApps.Api.Controllers
{
    public class AuthController : ApiController
    {
        private readonly AccountService _accountService;
        public AuthController()
        {
            _accountService = new AccountService();
        }
        [HttpPost]
        public IHttpActionResult SignIn([FromBody] UserLoginModel userModel)
        {
            var user = _accountService.AuthenticateUser(userModel);
            IHttpActionResult Response = Unauthorized();
            if (user != null)
            {
                var tokenStr = _accountService.GenerateJSONWebToken(user);
                Response = Ok(new TokenInfoModels { token = tokenStr, session_time_out = int.Parse(ConfigurationManager.AppSettings["SessionTimeOut"]??"60") });
            }
            return Response;
        }
    }
}
