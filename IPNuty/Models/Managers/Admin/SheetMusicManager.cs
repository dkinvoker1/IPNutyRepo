using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//u Oli tego nie było, sprawdzić potem
using IPNuty.Models.Collections;

namespace IPNuty.Models.Managers.Admin
{
    public class SheetMusicManager
    {
        public List<SheetMusic> allSheetMusicList = SheetMusicCollection.Instance.AllSheetMusicList;

        public void AddSheetMusic(SheetMusic sheetMusic)
        {
            allSheetMusicList.Add(sheetMusic);
        }

        public void RemoveSheetMusic(SheetMusic sheetMusic)
        {
            allSheetMusicList.Remove(sheetMusic);
        }

        public void SheetMusicTitleUpdate(SheetMusic sheetMusic, string title)
        {
            sheetMusic.Title = title;
        }

        public void SheetMusicAuthorUpdate(SheetMusic sheetMusic, string author)
        {
            sheetMusic.Author = author;
        }
    }
}