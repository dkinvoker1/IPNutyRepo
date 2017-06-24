using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IPNuty.Models;
//u Oli tego nie było, sprawzić potem
using IPNuty.Models.Collections;

namespace IPNuty.ViewModels.Admin
{
    public class SheetMusicListAcctualizationViewModel
    {
        public SheetMusic sheetMusic;
        public List<SheetMusic> allSheetMusicList = SheetMusicCollection.GetAllSheetMusic();
    }
}