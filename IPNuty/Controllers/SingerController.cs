using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IPNuty.ViewModels.Singers;
using System.Threading.Tasks;
using IPNuty.Models;
using IPNuty.Models.Collections;

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

        // POST: /Songer/AddSheetMusic
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddNewSheetMusicToSinger()
        {
            //tutaj trzeba będzie określić jakoś tego singera -> do zrobienia potem
            var singers = SingersCollection.GetAllSingers();
            Singer thisSinger = singers[1];

            SheetMusic sheet = new SheetMusic("Adelajda!", "Panteon", 1);

            thisSinger.SingerSheetMusicList.Add(sheet);
            SingersCollection.UpdateSinger(thisSinger);

            ViewBag.Message = "Dodano nuty do bazy!";
            return View();
        }
    }
}