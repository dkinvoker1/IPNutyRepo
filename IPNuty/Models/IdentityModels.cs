
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace IPNuty.Models
{
    public class ApplicationUser : IdentityUser
    {
        //You can extend this class by adding additional fields like Birthday
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Singer> Singers { get; set; }
        public DbSet<SheetMusic> SheetsOfMusic { get; set; }

        public ApplicationDbContext() : base("MyConnectionString")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}