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
using IPNuty.Models.Managers.Singers;

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

        //GET
        [HttpGet]
        [AllowAnonymous]
        public ActionResult SubtractSheetMusicFromSinger(SheetMusic sheetToSubtract)
        {
            string userID = User.Identity.GetUserName();
            var allSingers = SingersCollection.GetAllSingers();
            Singer thisSinger = allSingers.Where(e => e.Name + e.LastName == userID).FirstOrDefault();
            //to jest do zmiany!
            if (thisSinger == null)
            {
                ViewBag.Message = "Musisz być zalogowany aby odjąć nuty!";
                return View("Home");
            }

            if (thisSinger.SingerSheetMusicList == null)
            {
                thisSinger.SingerSheetMusicList = new List<SheetMusic>();
            }
            //var typ = sheetToSubtract.Type.GetHashCode();
            //sheetToSubtract = new SheetMusic(sheetToSubtract.Title, sheetToSubtract.Author, typ);
            sheetToSubtract.SingerID = thisSinger;
            var index=thisSinger.SingerSheetMusicList.FindIndex(e=>
                                                                    e.Author==sheetToSubtract.Author &&
                                                                    e.SheetMusicId==sheetToSubtract.SheetMusicId &&
                                                                    e.SingerID==sheetToSubtract.SingerID &&
                                                                    e.Title==sheetToSubtract.Title &&
                                                                    e.Type==sheetToSubtract.Type
                                                                    );
            thisSinger.SingerSheetMusicList.RemoveAt(index);
            SingersCollection.UpdateSinger(thisSinger);

            ViewBag.Message = "Dodano nuty jako posiadane!";
            return View("Home");
        }

        //GET
        [HttpGet]
        [AllowAnonymous]
        public ActionResult OrderSheetMusic(SheetMusic sheetToOrder)
        {
            string userID = User.Identity.GetUserName();
            var allSingers = SingersCollection.GetAllSingers();
            Singer thisSinger = allSingers.Where(e => e.Name + e.LastName == userID).FirstOrDefault();

            var type = sheetToOrder.Type.GetHashCode();
            sheetToOrder = new SheetMusic(sheetToOrder.Title, sheetToOrder.Author, type);

            Order thisOrder = new Order.Builder(thisSinger).SetOrderedSheetMusic(sheetToOrder).SetOrderTime(DateTime.Now).SetOrderStatus(false).Build();

            if (thisSinger == null)
            {
                ViewBag.Message = "Musisz być zalogowany aby zamówić nuty!";
                return View("Order");
            }
            else
            {
                OrdersManager orderManager = new OrdersManager();
                orderManager.AddOrder(thisOrder);

                ViewBag.Message = "Złożono zamówienie na nuty!";
                return View("Order");
            }
        }
    }
}