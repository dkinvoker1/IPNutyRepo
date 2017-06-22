using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Managers.Singers
{
    public class SheetMusicManager
    {
        public void AddSheetMusic(Singer singer, SheetMusic sheetMusic)
        {
            singer.SingerSheetMusicList.Add(sheetMusic);
        }

        public void RemoveSheetMusic(Singer singer, SheetMusic sheetMusic)
        {
            singer.SingerSheetMusicList.Remove(sheetMusic);
        }
    }
}