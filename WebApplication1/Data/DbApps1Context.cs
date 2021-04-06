using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DbApps1Context : DbContext
    {
        public DbApps1Context()
            : base("Name = DbApps1Context")
        {
        }
        public virtual DbSet<t_user> users { get; set; }
        public virtual DbSet<t_role> roles { get; set; }
    }
}