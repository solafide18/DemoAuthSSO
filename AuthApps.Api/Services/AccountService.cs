using AuthApps.Api.Models.RequestModels;
using AuthApps.Api.Models.ViewModels;
using AuthApps.Data;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace AuthApps.Api.Services
{
    public class AccountService
    {
        public AccountService()
        {

        }
        public UserInfoModel AuthenticateUser(UserLoginModel userModel)
        {
            UserInfoModel result = null;
            try
            {
                using (DbAuthAppContext db = new DbAuthAppContext())
                {
                    result = db.user_login.Where(q => q.username == userModel.user_name && q.pasword == userModel.password)
                        .Select(s => new UserInfoModel
                        {
                            user_name = s.username,
                            message = "user found",
                            mac_address = s.mac_address
                        }
                    ).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result = new UserInfoModel { error = true, message = ex.ToString() };
            }
            return result;
        }

        public void UpdateUserLogin(string user, bool isLogin)
        {
            try
            {
                using (DbAuthAppContext db = new DbAuthAppContext())
                {
                    var userData = db.user_login.Where(q => q.username == user).FirstOrDefault();
                    userData.is_login = isLogin;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string GenerateJSONWebToken(UserInfoModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Jwt_Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.user_name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("macaddress", userInfo.mac_address ?? ""),
                new Claim("role", "user")
            };

            var token = new JwtSecurityToken(
                issuer: ConfigurationManager.AppSettings["Jwt_Issuer"],
                audience: ConfigurationManager.AppSettings["Jwt_Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(int.Parse(ConfigurationManager.AppSettings["SessionTimeOut"] ?? "60")),
                signingCredentials: credentials);
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        public string RefreshJSONWebToken(IList<Claim> claimsSession)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["Jwt_Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, claimsSession[0]?.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("macaddress", claimsSession[2].Value ?? ""),
                new Claim("role", "user")
            };

            var token = new JwtSecurityToken(
                issuer: ConfigurationManager.AppSettings["Jwt_Issuer"],
                audience: ConfigurationManager.AppSettings["Jwt_Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(int.Parse(ConfigurationManager.AppSettings["SessionTimeOut"] ?? "60")),
                signingCredentials: credentials);
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }
    }
}