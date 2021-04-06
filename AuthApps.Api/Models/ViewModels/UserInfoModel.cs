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
    }
}