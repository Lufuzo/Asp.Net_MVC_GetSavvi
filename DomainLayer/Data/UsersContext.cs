using Entities.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext() : base("name=LoginConnection")
        {
            Database.SetInitializer<UsersContext>(null);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Users>  Users { get; set; }
    }
}
