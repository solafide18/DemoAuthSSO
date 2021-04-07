using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthApps.Api.Models.ViewModels
{
    public class UserInfoModel
    {
        public string user_name { get; set; }
        public string mac_address { get; set; }
        public string message { get; set; }
        public bool error { get; set; }

        public bool is_login { get; set; }

        public string sis_login
        {
            get
            {
                string ret = "0";

                if (is_login)
                {
                    ret = "1";
                }

                return ret;
            }
        }
    }
}