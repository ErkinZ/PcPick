using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PcPick.MyDB
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("MyDbContext")
        {
        }

        public virtual DbSet<Models.Product> Products { get; set; }
        public virtual DbSet<Models.Category> Categories { get; set; }
    }
}