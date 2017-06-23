using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Collections
{
    public sealed class SheetMusicCollection
    {
        #region Singleton

        private static SheetMusicCollection sheetMusicList = null;
        public static SheetMusicCollection Instance
        {
            get
            {
                if (sheetMusicList == null)
                {
                    sheetMusicList = new SheetMusicCollection();
                }
                return sheetMusicList;
            }
        }

        #endregion

        public List<SheetMusic> AllSheetMusicList { get; set; }

        private SheetMusicCollection()
        {
            // tu docelowo pobrana lista z bazy danych
            // dodatkowo musi znaleźć się pole z informacją, ile mamy nut na stanie
            this.AllSheetMusicList = new List<SheetMusic>();

            // tymczasowo!
            AllSheetMusicList.Add(new SheetMusic("Earth Song", "Frank Tichieli", 5));
            AllSheetMusicList.Add(new SheetMusic("Lux Aurumque", "Eric Whitacre", 2));
            AllSheetMusicList.Add(new SheetMusic("Sacrum Convivium", "Dawid Kusz OP", 2));
            AllSheetMusicList.Add(new SheetMusic("All That Hath Life and Breath Praise Ye the Lord", "Rene Clausen", 2));
            AllSheetMusicList.Add(new SheetMusic("All of me", "John Legend", 1));
        }

    }
}