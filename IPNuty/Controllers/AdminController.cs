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

        [Route("AdminController/AddSinger")]
        public ActionResult AddSinger(SingersListAcctualizationViewModel model)
        {
            SingersManager singersManager = new SingersManager();

            Singer singer = new Singer.Builder(model.singer.Name, model.singer.LastName)
                .SetActivicity(model.singer.Activicity)
                .SetJoiningDate(model.singer.JoiningDate)
                .build();

            singersManager.CreateNewSinger(singer);

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
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //public ActionResult SingersSheetMusicListAcctualization(Singer singer)
        //{
        //    var singersSheetMusicListAcctualization = new SingersSheetMusicListAcctualizationViewModel();
        //    singersSheetMusicListAcctualization.singer = singer;
        //    return View(singersSheetMusicListAcctualization);
        //}

        public ActionResult AddNewSinger()
        {
            var addNewSinger = new AddNewSingerViewModel();
            return View(addNewSinger);
        }

    }
}