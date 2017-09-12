using System;
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
            Singer singer = singersManager.GetSingerById(singerId);

            singersManager.SingerActivicityUpdate(singer);

            var singersListAcctualization = new SingersListAcctualizationViewModel();
            return View(singersListAcctualization.allSingersList);
        }
        
        // GET: /Admin/
        public ActionResult SingersSheetMusicListAcctualization()
        {
            var singersSheetMusicListAcctualization = new SingersSheetMusicListAcctualizationViewModel();
            return View(singersSheetMusicListAcctualization);
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
                sheetManager.RemoveSheetMusic(sheet);
                ModelState.Clear();
                allSheets.Remove(sheet);
            }
            return View("SheetMusicListAcctualization", allSheets.ToList());
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

        ///// <summary>
        ///// Usuwanie singera przez admina
        ///// </summary>
        ///// <param name="singerToBeDeleted"> Singer który ma być usunięty </param>
        ///// <returns></returns>
        //// GET:
        //[HttpGet]
        //[Authorize(Roles = "Admin")]
        //public ActionResult DeleteUser(Singer singerToBeDeleted)
        //{
        //    AccountController cont = new AccountController();
        //    return cont.DeleteUser(singerToBeDeleted);
        //}
        #endregion


    }
}