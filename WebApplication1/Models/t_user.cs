using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    [Table("t_user")]
    public class t_user
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string user_name { get; set; }
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public int role_id { get; set; }
    }
}