using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IPNuty.ViewModels.Admin;
using IPNuty.Models;
using IPNuty.Models.Managers.Admin;
using System.Threading.Tasks;

namespace IPNuty.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }

        //
        // GET: /Admin/
        public ActionResult Order()
        {
            var orderViewModel = new OrderViewModel();
            return View(orderViewModel.AllOrdersList);
        }

        //
        // GET: /Admin/
        public ActionResult SheetMusicListAcctualization()
        {
            var sheetMusicListAcctualization = new SheetMusicListAcctualizationViewModel();
            return View(sheetMusicListAcctualization.allSheetMusicList);
        }


        //
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
        //
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
                SheetMusic sheet = new SheetMusic(model.title, model.author, model.type);
                SheetMusicManager sheetManager = new SheetMusicManager();
                sheetManager.AddSheetMusic(sheet);
                ViewBag.Message = "Dodano nuty do bazy!";
                ModelState.Clear();
                return View();
                //return RedirectToAction("AddNewSheetMusic", "Admin");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        #endregion
    }
}