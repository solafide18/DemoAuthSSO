﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthApps.Api.Models.ResponseModels
{
    public class UserTokenModel : DefaultResponseModel
    {
        public string user_name { get; set; }
        public string token { get; set; }
        public int session_time_out { get; set; }
    }
}