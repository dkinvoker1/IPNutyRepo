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
        /// <summary>
        /// admin usuwa nuty z bazy (i wszystkie ich wystąpienia u singerów)
        /// </summary>
        /// <param name="sheetMusic"> nuty do usunięcia</param>
        public void RemoveSheetMusic(SheetMusic sheetMusic)
        {
            using (var db = new ApplicationDbContext())
            {
                //usuwanie orderów na te shhetmusicy
                var orderDel = db.Orders.Where(e => e.SheetMusicId.SheetMusicId == sheetMusic.SheetMusicId);
                db.Orders.RemoveRange(orderDel);
                //usuwanie wszystkich wystąpień w sheetmusic(z tymi które mają singerzy)
                var del = db.SheetsOfMusic.Where(e => e.Author == sheetMusic.Author && e.Title == sheetMusic.Title && e.Type == sheetMusic.Type);
                db.SheetsOfMusic.RemoveRange(del);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Usuwanie wszystkich nut danego singera
        /// </summary>
        /// <param name="singer"> Singer którego nuty trzeba usunąć</param>
        public void RemoveSheetMusic(Singer singer)
        {
            using (var db = new ApplicationDbContext())
            {
                //var del=db.SheetsOfMusic.Where(e => e.SheetMusicId == sheetMusic.SheetMusicId).FirstOrDefault();
                var dell = db.SheetsOfMusic.ToList();
                var del = dell.Where(e => e.SingerID!=null && e.SingerID.SingerId.Equals(singer.SingerId));
                if (del.Count() > 0)
                    db.SheetsOfMusic.RemoveRange(del);
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