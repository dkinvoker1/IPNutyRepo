using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IPNuty.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using IPNuty;

namespace IPNuty.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //public string AddUser()
        //{
        //    ApplicationUser user;
        //    ApplicationUserStore Store = new ApplicationUserStore(new ApplicationDbContext());
        //    ApplicationUserManager userManager = new ApplicationUserManager(Store);
        //    user = new ApplicationUser
        //    {
        //        UserName = "TestUser",
        //        Email = "TestUser@test.com"
        //    };
            
        //    var result = userManager.Create(user);
        //    if (!result.Succeeded)
        //    {
        //        return result.Errors.First();
        //    }
        //    return "User Added";
        //}
    }
}