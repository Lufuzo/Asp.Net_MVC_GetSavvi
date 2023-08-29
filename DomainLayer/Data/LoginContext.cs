using Entities.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DomainLayer.Data
{
    public class LoginContext : DbContext
    {
        //LoginContext(DbContextOptions<LoginContext> options) : base(options)
        //{ }

       
        public LoginContext() : base("name=LoginConnection")
        {
            Database.SetInitializer<LoginContext>(null);
        }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    

        public DbSet<LoginCredentials> LoginEntities { get; set; }

    }
}
