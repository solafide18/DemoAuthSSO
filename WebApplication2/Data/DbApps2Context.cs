using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class DbApps2Context : DbContext
    {
        public DbApps2Context()
            : base("Name = DbApps2Context")
        {
        }
        public virtual DbSet<t_user> users { get; set; }
        public virtual DbSet<t_role> roles { get; set; }
    }
}