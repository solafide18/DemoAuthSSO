﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthApps.Api.Models.RequestModels
{
    public class UserLoginModel
    {
        public string user_name { get; set; }
        public string password { get; set; }
        public string mas_address { get; set; }
    }
}