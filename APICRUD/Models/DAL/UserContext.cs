using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace APICRUD.Models.DAL
{
   
    public class UserDBInitializer : CreateDatabaseIfNotExists<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            IList<User> defaultUsers = new List<User>();
            defaultUsers.Add(new User() { Id = 1, FirstName = "Rahul", LastName = "Dixit" });
            defaultUsers.Add(new User() { Id = 2, FirstName = "Jonathon", LastName = "Holden" });
            defaultUsers.Add(new User() { Id = 3, FirstName = "David", LastName = "Miller" });
            defaultUsers.Add(new User() { Id = 4, FirstName = "Reggie", LastName = "Johnson" });

            context.Employees.AddRange(defaultUsers);

            base.Seed(context);
        }
    }

    public class UserContext : DbContext
    {
        public UserContext() : base(ConfigurationManager.AppSettings["ConnectionString"])
        {           
        }
        public virtual DbSet<User> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .HasColumnName("Id");

            modelBuilder.Entity<User>()
            .Property(u => u.FirstName)
            .HasColumnName("FirstName");

            modelBuilder.Entity<User>()
            .Property(u => u.LastName)
            .HasColumnName("LastName");

        }
    }
}