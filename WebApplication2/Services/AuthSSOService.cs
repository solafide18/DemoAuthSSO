using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebApplication2.Services
{
    public class AuthSSOService
    {
        public AuthSSOService()
        {

        }

        public bool IsValidCookies()
        {
            bool result = true;

            return result;
        }

        public void CekValidToken()
        {
            using(var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client
            }
        }
    }
}