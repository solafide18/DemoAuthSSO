using AuthApps.Api.Models.RequestModels;
using AuthApps.Api.Models.ResponseModels;
using AuthApps.Api.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
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
                Response = Ok(
                    new TokenInfoModel
                    {
                        token = tokenStr,
                        session_time_out = int.Parse(ConfigurationManager.AppSettings["SessionTimeOut"] ?? "60"),
                        status_code = "200",
                        status_desc = "sign in"
                    });
            }
            _accountService.UpdateUserLogin(user.user_name, true);
            return Response;
        }
        [HttpPost]
        [Authorize]
        [Route("api/Auth/SignOut")]
        public IHttpActionResult SignOut()
        {
            UserTokenModel model = new UserTokenModel();
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IList<Claim> claims = identity.Claims.ToList();
                    model.user_name = claims[0]?.Value;
                    model.status_code = "200";
                    model.status_desc = "sign out";
                }
                _accountService.UpdateUserLogin(model.user_name, false);
            }
            return Ok(model);
        }
        [HttpPost]
        [Authorize]
        [Route("api/Auth/ValidateToken")]
        public IHttpActionResult ValidateToken()
        {
            UserTokenModel model = new UserTokenModel();
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IList<Claim> claims = identity.Claims.ToList();
                    string tokenStr = _accountService.RefreshJSONWebToken(claims);

                    model.user_name = claims[0]?.Value;
                    model.token = tokenStr;
                    model.session_time_out = int.Parse(ConfigurationManager.AppSettings["SessionTimeOut"] ?? "60");
                    model.status_code = "200";
                    model.status_desc = "success";
                    _accountService.UpdateUserLogin(model.user_name, true);
                }
            }
            return Ok(model);
        }

        [HttpPost]
        [Authorize]
        [Route("api/Auth/RefreshToken")]
        public IHttpActionResult RefreshToken()
        {
            string tokenStr = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                var identity = User.Identity as ClaimsIdentity;
                List<Claim> claims = identity.Claims.ToList();
                tokenStr = _accountService.RefreshJSONWebToken(claims);
                _accountService.UpdateUserLogin(claims[0]?.Value, true);
            }
            return Ok(
                new TokenInfoModel
                {
                    token = tokenStr,
                    session_time_out = int.Parse(ConfigurationManager.AppSettings["SessionTimeOut"] ?? "60"),
                    status_code = "200",
                    status_desc = "success"
                });
        }
    }
}
