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
            using (var db = new ApplicationDbContext())
            {
                var nuta1 = new SheetMusic("Earth Song", "Frank Tichieli", 5);
                var nuta2 = new SheetMusic("Lux Aurumque", "Eric Whitacre", 2);

                var muzyk = new Singer.Builder("Magda", "Magdzińska").build();

                //db.Singers.Add(muzyk);
                db.SheetsOfMusic.Add(nuta1);
                db.SaveChanges();
                //allSheetMusicList=db.SheetsOfMusic.Select
            }
            
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