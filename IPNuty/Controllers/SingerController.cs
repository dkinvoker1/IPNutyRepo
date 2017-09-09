using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IPNuty.ViewModels.Singers;
using System.Threading.Tasks;
using IPNuty.Models;
using IPNuty.Models.Collections;
using Microsoft.AspNet.Identity;

namespace IPNuty.Controllers
{
    public class SingerController : Controller
    {
        // GET: Singer
        public ActionResult Home()
        {
            return View();
        }

        //
        // GET: /Singer/
        public ActionResult Order()
        {
            var orderViewModel = new OrderViewModel();
            return View(orderViewModel.order);
        }

        //
        // GET: /Singer/
        public ActionResult SheetMusicList()
        {
            var sheetMusicListAcctualizationViewModel = new SheetMusicListAcctualizationViewModel();
            return View(sheetMusicListAcctualizationViewModel.allSheetMusicList);
        }

        //GET
        [HttpGet]
        [AllowAnonymous]
        public ActionResult AddNewSheetMusicToSinger(SheetMusic sheetToAdd)
        {
            string userID = User.Identity.GetUserName();
            var allSingers = SingersCollection.GetAllSingers();
            Singer thisSinger = allSingers.Where(e => e.Name+e.LastName == userID).FirstOrDefault();
            //to jest do zmiany!
            if(thisSinger==null)
            {
                ViewBag.Message = "Musisz być zalogowany aby dodać nuty!";
                return View("Home");
            }

            if (thisSinger.SingerSheetMusicList == null) 
            {
                thisSinger.SingerSheetMusicList = new List<SheetMusic>();
            }
            var typ = sheetToAdd.Type.GetHashCode();
            sheetToAdd = new SheetMusic(sheetToAdd.Title, sheetToAdd.Author, typ);
            thisSinger.SingerSheetMusicList.Add(sheetToAdd);
            SingersCollection.UpdateSinger(thisSinger);





            ViewBag.Message = "Dodano nuty jako posiadane!";
            return View("Home");
        }
    }
}