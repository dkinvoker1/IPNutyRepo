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

        public void AddSheetMusic(SheetMusic sheetMusic) //admin dodaje nowe nuty do bazy
        {
            using (var db = new ApplicationDbContext())
            {
                db.SheetsOfMusic.Add(sheetMusic);
                db.SaveChanges();
            }

        }

        public void RemoveSheetMusic(SheetMusic sheetMusic) //admin usuwa nuty z bazy
        {
            using (var db = new ApplicationDbContext())
            {
                var del=db.SheetsOfMusic.Where(e => e.SheetMusicId == sheetMusic.SheetMusicId).FirstOrDefault();
                db.SheetsOfMusic.Remove(del);
                db.SaveChanges();
            }
        }

        public void SheetMusicTitleUpdate(SheetMusic sheetMusic, string title) //brakuje wywołania
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.SheetsOfMusic.SingleOrDefault(s => s.Title == sheetMusic.Title & s.Author == sheetMusic.Author);
                result.Title = title;
                db.SaveChanges();
            }
        }

        public void SheetMusicAuthorUpdate(SheetMusic sheetMusic, string author) //brakuje wywołania
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