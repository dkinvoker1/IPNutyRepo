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

        public void AddSheetMusic(SheetMusic sheetMusic)
        {
            using (var db = new ApplicationDbContext())
            {
                db.SheetsOfMusic.Add(sheetMusic);
                db.SaveChanges();
            }

        }

        public void RemoveSheetMusic(SheetMusic sheetMusic)
        {
            using (var db = new ApplicationDbContext())
            {
                db.SheetsOfMusic.Remove(sheetMusic);
                db.SaveChanges();
            }
        }

        public void SheetMusicTitleUpdate(SheetMusic sheetMusic, string title)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.SheetsOfMusic.SingleOrDefault(s => s.Title == sheetMusic.Title & s.Author == sheetMusic.Author);
                result.Title = title;
                db.SaveChanges();
            }
        }

        public void SheetMusicAuthorUpdate(SheetMusic sheetMusic, string author)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.SheetsOfMusic.SingleOrDefault(s => s.Title == sheetMusic.Title & s.Author == sheetMusic.Author);
                result.Author = author;
                db.SaveChanges();
            }
        }
    }
}