using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IPNuty.ViewModels.Admin;
using IPNuty.Models;
using IPNuty.Models.Managers.Admin;

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

        public ActionResult AddNewSheetMusic()
        {
            var addNewSheetMusic = new AddNewSheetMusicModel();
            return View(addNewSheetMusic);
        }
    }
}