﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IPNuty.ViewModels.Admin;
using IPNuty.Models;
using IPNuty.Models.Managers.Admin;
using System.Threading.Tasks;
using System.Net;
using IPNuty.Models.Collections;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace IPNuty.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }

        // GET: /Admin/
        public ActionResult Order()
        {
            var orderViewModel = new OrderViewModel();
            return View(orderViewModel.AllOrdersList);
        }

        // GET: /Admin/
        public ActionResult SheetMusicListAcctualization()
        {
            var sheetMusicListAcctualization = new SheetMusicListAcctualizationViewModel();
            return View(sheetMusicListAcctualization.allSheetMusicList);
        }

        // GET: /Admin/
        public ActionResult SingerListAcctualization()
        {
            var singersListAcctualization = new SingersListAcctualizationViewModel();
            return View(singersListAcctualization.allSingersList);
        }

        public ActionResult ChangeSingerActivicity(int singerId)
        {
            SingersManager singersManager = new SingersManager();
            //Singer singer = singersManager.GetSingerById(singerId);
            var allSingers = SingersCollection.GetAllSingers();
            var singer = allSingers.Where(x => x.SingerId == singerId).FirstOrDefault();

            singersManager.SingerActivicityUpdate(singer);

            var singersListAcctualization = new SingersListAcctualizationViewModel();
            return View("SingerListAcctualization", singersListAcctualization.allSingersList);
        }
        
        // GET: /Admin/
        public ActionResult SingersSheetMusicListAcctualization()
        {
            var singersSheetMusicListAcctualization = new SingersSheetMusicListAcctualizationViewModel();
            return View(singersSheetMusicListAcctualization);
        }

        [HttpGet]
        [AllowAnonymous]
        // usuwanie zamówień
        public ActionResult RemoveSingerOrder(Order order)
        {

                OrdersManager orderManager = new OrdersManager();
                orderManager.RemoveOrder(order);

                ViewBag.Message = "Usunięto zamówienie!";
                return View("Order", OrdersCollection.GetAllOrders());

        }

        [HttpGet]
        [AllowAnonymous]
        // usuwanie zamówień
        public ActionResult SumUp()
        {
            var allOrders = OrdersCollection.GetAllOrders();
            var uniqueSheetMusic = allOrders.Where(x => x.Completed == false).GroupBy(e => e.SheetMusicId.SheetMusicId).Select(x => x.ToList()).ToList();

            var uniqueForView = new List<Order>();
            var uniqueForViewCount = new List<int>();

            foreach (var item in uniqueSheetMusic)
            {
                var uniqueOrder = item.ToList();
                int uniqueOrderCount = uniqueOrder.Count;

                uniqueForView.Add(uniqueOrder[0]);
                uniqueForViewCount.Add(uniqueOrderCount);
            }

            ViewBag.Count = uniqueForViewCount;
            ViewBag.UniqueOrders = uniqueForView;

            return View("Order", allOrders);

        }

        public ActionResult RemoveSumedUpOrder(Order order)
        {
            var thisOrder = order;

            var allOrders = OrdersCollection.GetAllOrders();
            var uniqueSheetMusic = allOrders.GroupBy(e => e.SheetMusicId.SheetMusicId).Select(x => x.ToList()).ToList();

            var uniqueForView = new List<Order>();
            var uniqueForViewCount = new List<int>();

            foreach (var item in uniqueSheetMusic)
            {
                var uniqueOrder = item.ToList();
                int uniqueOrderCount = uniqueOrder.Count;

                uniqueForView.Add(uniqueOrder[0]);
                uniqueForViewCount.Add(uniqueOrderCount);
            }

            var orderToRemove = uniqueForView.Where(x => x.OrderId == thisOrder.OrderId).FirstOrDefault();
            var index = uniqueForView.IndexOf(orderToRemove);

            uniqueForView.Remove(orderToRemove);
            uniqueForViewCount.RemoveAt(index);

            ViewBag.Count = uniqueForViewCount;
            ViewBag.UniqueOrders = uniqueForView;

            return View("Order", allOrders);
        }

        public ActionResult RemoveAllSumedUpOrder()
        {

            var allOrders = OrdersCollection.GetAllOrders();

            ViewBag.Count = null;
            ViewBag.UniqueOrders = null;

            return View("Order", allOrders);
        }

        public ActionResult ChangeOrderStatus(Order order)
        {

            var allOrders = OrdersCollection.GetAllOrders();
            var thisOrder = allOrders.Where(x => x.OrderId == order.OrderId).FirstOrDefault();

            var ordersToChange = allOrders.Where(x => x.SheetMusicId.SheetMusicId == thisOrder.SheetMusicId.SheetMusicId).ToList();

            OrdersManager ordersManager = new OrdersManager();

            foreach (Order o in ordersToChange)
            {
                ordersManager.OrderStatusUpdate(o, !o.Completed);
            }

            SumUp();

            if (Session["type"] != null && Session["resulttype"] != null)
            {
                return View("Order", allOrders);
            }
            else
            {
                return RedirectToAction("Order");
            }
            
        }
        #region Singers - to jest w AccountControllerze

        //GET
        //[AllowAnonymous]
        //public ActionResult AddNewSinger()
        //{
        //    var addNewSinger = new AddNewSingerViewModel();
        //    return View(addNewSinger);
        //}
        // POST: /Admin/AddNewSinger
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ActionResult> AddNewSinger(AddNewSingerViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Singer singer = new Singer.Builder(model.name, model.lastName)
        //        .SetActivicity(model.activicity)
        //        .SetJoiningDate(model.joiningDate)
        //        .build();

        //        SingersManager sheetManager = new SingersManager();
        //        sheetManager.CreateNewSinger(singer);
        //        ViewBag.Message = "Dodano chórzystę do bazy!";
        //        ModelState.Clear();
        //        return View();
        //        //return RedirectToAction("", "");
        //    }
        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
        #endregion

        #region SheetMusic
        //GET
        [AllowAnonymous]
        public ActionResult AddNewSheetMusic()
        {
            var addNewSheetMusic = new AddNewSheetMusicModel();
            return View(addNewSheetMusic);
        }

        // POST: /Admin/AddSheetMusic
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddNewSheetMusic(AddNewSheetMusicModel model)
        {
            if (ModelState.IsValid)
            {
                SheetMusic sheet = new SheetMusic(model.title, model.author, model.type-1);
                var all=SheetMusicCollection.GetAllSheetMusic();
                if(all.Where(e=>e.Author==sheet.Author && e.Title== sheet.Title && e.Type==sheet.Type).FirstOrDefault()!=null)
                {
                    TempData["msg"] = "<script>alert('Nie można dodać nut: w bazie istnieje już ich kopia.');</script>";
                    ModelState.Clear();
                    return View();
                }
                SheetMusicManager sheetManager = new SheetMusicManager();
                sheetManager.AddSheetMusic(sheet);
                TempData["msg"] = "<script>alert('Pomyślnie dodano nuty do bazy.');</script>";
                ModelState.Clear();
                return View();
                //return RedirectToAction("AddNewSheetMusic", "Admin");
            }
            return View(model);
        }

        //GET
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RemoveSheetMusic(SheetMusic sheetToDelete)
        {
            SheetMusic sheet;
            var allSheets = SheetMusicCollection.GetAllSheetMusic();
            sheet = allSheets.Where(e => e.SheetMusicId == sheetToDelete.SheetMusicId).FirstOrDefault();
            if (allSheets.Contains(sheet))
            {
                ViewBag.Message = sheetToDelete.Author + ", " + sheetToDelete.Title +".";
                SheetMusicManager sheetManager = new SheetMusicManager();
                sheetManager.RemoveAllSheetMusic(sheet);
                ModelState.Clear();
                allSheets.Remove(sheet);
            }
            return View("SheetMusicListAcctualization", allSheets.ToList());
        }

        //GET
        [HttpGet]
        [AllowAnonymous]
        public ActionResult RemoveSingerSheetMusic(SheetMusic sheetToDelete)
        {
            SheetMusic sheet;
            var allSheets = SheetMusicCollection.GetAllSheetMusic();
            sheet = allSheets.Where(e => e.SheetMusicId == sheetToDelete.SheetMusicId).FirstOrDefault();
            if (allSheets.Contains(sheet))
            {
                ViewBag.Message = sheetToDelete.Author + ", " + sheetToDelete.Title + ".";
                SheetMusicManager sheetManager = new SheetMusicManager();
                sheetManager.RemoveSheetMusic(sheet);
                ModelState.Clear();
                allSheets.Remove(sheet);
            }
            var singer = sheet.SingerID;
            var sheets = SheetMusicCollection.GetAllSheetMusic();
            var singerSheets = sheets.Where(e => e.SingerID != null && e.SingerID.SingerId == singer.SingerId);
            ViewBag.Message = singer.Name+" "+singer.LastName;
            return View("SingersSheetMusicListAcctualization", singerSheets.ToList());
        }

        public ActionResult Sortuj(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "author_desc" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "type_desc" : "Type";

            ApplicationDbContext dbcontext = new ApplicationDbContext();
            var sheets = from s in dbcontext.SheetsOfMusic
                           select s;
            switch (sortOrder)
            {
                case "author_desc":
                    sheets = sheets.OrderByDescending(s => s.Author);
                    break;
                case "Title":
                    sheets = sheets.OrderBy(s => s.Title);
                    break;
                case "title_desc":
                    sheets = sheets.OrderByDescending(s => s.Title);
                    break;
                case "Type":
                    sheets = sheets.OrderBy(s => s.Type);
                    break;
                case "type_desc":
                    sheets = sheets.OrderByDescending(s => s.Type);
                    break;
                default:
                    sheets = sheets.OrderBy(s => s.Author);
                    break;
            }
            return View("SheetMusicListAcctualization", sheets.ToList());
        }
        //========================================================================
        //to jest do usuwania singerów ->przenieść do góry
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                _userManager = value;
            }
        }
        /// <summary>
        /// Usuwanie singera przez admina
        /// </summary>
        /// <param name="singerToBeDeleted"> Singer który ma być usunięty </param>
        /// <returns></returns>

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteUser(Singer singerToBeDeleted)
        {
            //pobranie identity singera (hasła, role, logint)
            //var identitySinger = UserManager.Users.Where(e => e.SingerId == singerToBeDeleted).FirstOrDefault();
            //var UserContext = new ApplicationDbContext();
            //var users = UserContext.Users.ToList();
            var identitySingers = UserManager.Users.ToArray();
            var identitySinger = identitySingers.Where(e => e.UserName == singerToBeDeleted.Name+singerToBeDeleted.LastName).FirstOrDefault();

            //tutaj to samo co wyżej ale dla orderów jak Ola zrobi
            //====================================================
            OrdersManager orderMnager = new OrdersManager();
            orderMnager.RemoveOrder(singerToBeDeleted);
            //====================================================
            //usunięcie wszystkich nut singera
            SheetMusicManager musicManager = new SheetMusicManager();
            musicManager.RemoveSheetMusic(singerToBeDeleted);

            if (identitySinger != null)
            {
                UserManager.RemoveFromRole(identitySinger.Id, "Singer");
                UserManager.Delete(identitySinger);
            }
            SingersManager singerManager = new SingersManager();
            singerManager.RemoveSinger(singerToBeDeleted);

            var allSingers = SingersCollection.GetAllSingers();
            return View("SingerListAcctualization", allSingers.ToList());
        }
        #endregion

        public ViewResult SortujSinger(string sortOrder, string searchString)
        {
            ViewBag.SurnameSortParm = String.IsNullOrEmpty(sortOrder) ? "surname_desc" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";
            ApplicationDbContext dbcontext = new ApplicationDbContext();
            var singers = from s in dbcontext.Singers
                         select s;

            switch (sortOrder)
            {
                case "surname_desc":
                    singers = singers.OrderByDescending(s => s.LastName);
                    break;
                case "Name":
                    singers = singers.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    singers = singers.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    singers = singers.OrderBy(s => s.JoiningDate);
                    break;
                case "date_desc":
                    singers = singers.OrderByDescending(s => s.JoiningDate);
                    break;
                case "Status":
                    singers = singers.OrderBy(s => s.Activicity);
                    break;
                case "status_desc":
                    singers = singers.OrderByDescending(s => s.Activicity);
                    break;
                default:
                    singers = singers.OrderBy(s => s.LastName);
                    break;
            }

            return View("SingerListAcctualization", singers.ToList());
        }

        public ViewResult SortujOrder(string sortOrder, string searchString)
        {
            ViewBag.StatusSortParm = String.IsNullOrEmpty(sortOrder) ? "status_desc" : "";
            ViewBag.IDSortParm = sortOrder == "ID" ? "ID_desc" : "ID";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.SurnameSortParm = sortOrder == "Surname" ? "surname_desc" : "Surname";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "author_desc" : "Author";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";


            ApplicationDbContext dbcontext = new ApplicationDbContext();
            var orders = from s in dbcontext.Orders
                          select s;

            switch (sortOrder)
            {
                case "status_desc":
                    orders = orders.OrderBy(s => s.Completed);
                    break;

                case "ID":
                    orders = orders.OrderBy(s => s.OrderId);
                    break;
                case "ID_desc":
                    orders = orders.OrderByDescending(s => s.OrderId);
                    break;
                case "Date":
                    orders = orders.OrderBy(s => s.OrderTime);
                    break;
                case "date_desc":
                    orders = orders.OrderByDescending(s => s.OrderTime);
                    break;
                case "Surname":
                    orders = orders.OrderBy(s => s.SingerId.LastName);
                    break;
                case "surname_desc":
                    orders = orders.OrderByDescending(s => s.SingerId.LastName);
                    break;
                case "Author":
                    orders = orders.OrderBy(s => s.SheetMusicId.Author);
                    break;
                case "author_desc":
                    orders = orders.OrderByDescending(s => s.SheetMusicId.Author);
                    break;
                case "Title":
                    orders = orders.OrderBy(s => s.SheetMusicId.Title);
                    break;
                case "title_desc":
                    orders = orders.OrderByDescending(s => s.SheetMusicId.Title);
                    break;
                default:
                    orders = orders.OrderByDescending(s => s.Completed);
                    break;
            }

            return View("Order", orders.ToList());
        }

        // GET: /Admin/
        public ActionResult SingerID(Singer singer)
        {
            
            var sheets = SheetMusicCollection.GetAllSheetMusic();
            var singerSheets = sheets.Where(e =>e.SingerID!=null && e.SingerID.SingerId == singer.SingerId);
            @ViewBag.Message = singer.Name + " " + singer.LastName;
            return View("SingersSheetMusicListAcctualization", singerSheets.ToList());
        }
    }
}