using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AuthApps.Api.Models.DBModels
{
    [Table("t_user_login")]
    public class t_user_login
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string pasword { get; set; }
        public bool? is_login { get; set; }
        public DateTime? session_refresh_date { get; set; }
        public string token { get; set; }
        public string mac_address { get; set; }
    }
}