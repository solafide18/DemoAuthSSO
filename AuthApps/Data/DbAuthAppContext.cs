using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AuthApps.Models;

namespace AuthApps.Data
{
    public class DbAuthAppContext : DbContext
    {
        public DbAuthAppContext()
            : base("Name = DbAuthAppContext")
        {
        }
        public virtual DbSet<t_user_login> user_login { get; set; }
    }
}