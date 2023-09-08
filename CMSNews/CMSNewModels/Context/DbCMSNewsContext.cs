using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMSNewModels.Models;

namespace CMSNewModels.Context
{
    public class DbCMSNewsContext:DbContext
    {
        public DbSet<News> News   { get; set; }
        public DbSet<NewGroup> NewGroups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }


    }
}
