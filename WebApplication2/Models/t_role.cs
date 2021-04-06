using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    [Table("t_role")]
    public class t_role
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string role_name { get; set; }
    }
}