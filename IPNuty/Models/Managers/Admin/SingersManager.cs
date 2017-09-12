using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IPNuty.Models.Collections;

namespace IPNuty.Models.Managers.Admin
{
    public class SingersManager
    {

        public void CreateNewSinger(Singer singer)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Singers.Add(singer);
                db.SaveChanges();
            }
        }

        public Singer GetSingerById(int id)
        {
            Singer singer;
            using (var db = new ApplicationDbContext())
            {
                var result = db.Singers.SingleOrDefault(s => s.SingerId == s.SingerId);
                singer = result;
            }
            return singer;
        }

        public void RemoveSinger(Singer singer)
        {
            using (var db = new ApplicationDbContext())
            {
                singer = db.Singers.Where(e => e.SingerId == singer.SingerId).FirstOrDefault();
                if (singer != null)
                {
                    db.Singers.Remove(singer);
                    db.SaveChanges();
                }
            }
        }

        public void SingerNameUpdate(Singer singer, string name)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Singers.SingleOrDefault(s => s.Name == singer.Name & s.LastName == singer.LastName);
                result.Name = name;
                db.SaveChanges();
            }
        }

        public void SingerLastNameUpdate(Singer singer, string lastName)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Singers.SingleOrDefault(s => s.Name == singer.Name & s.LastName == singer.LastName);
                result.LastName = lastName;
                db.SaveChanges();
            }
        }

        public void SingerActivicityUpdate(Singer singer)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Singers.SingleOrDefault(s => s.Name == singer.Name & s.LastName == singer.LastName);

                bool activicity = result.Activicity;
                if (activicity == true)
                {
                    result.Activicity = false;
                }
                else
                {
                    result.Activicity = true;
                }
                db.SaveChanges();
            }
        }

        public void SingerJoiningDateUpdate(Singer singer, DateTime joiningDate)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Singers.SingleOrDefault(s => s.Name == singer.Name & s.LastName == singer.LastName);
                result.JoiningDate = joiningDate;
                db.SaveChanges();
            }
        }

        public void SingerSheetMusicListAdd(Singer singer, SheetMusic sheetMusic)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Singers.SingleOrDefault(s => s.Name == singer.Name & s.LastName == singer.LastName);
                result.SingerSheetMusicList.Add(sheetMusic);
                db.SaveChanges();
            }
        }

        public void SingerSheetMusicListRemove(Singer singer, SheetMusic sheetMusic)
        {
            using (var db = new ApplicationDbContext())
            {
                // tu jest pewna wątpliwość - jak zaznaczane było w DB które nuty posiada który chórzysta?
                var resultSinger = db.Singers.SingleOrDefault(s => s.Name == singer.Name & s.LastName == singer.LastName);
                var resultSheetMusic = db.SheetsOfMusic.SingleOrDefault(s => s.SheetMusicId == sheetMusic.SheetMusicId);
                resultSinger.SingerSheetMusicList.Remove(resultSheetMusic);
                db.SaveChanges();
            }
        }

        public void SingerSheetMusicListClear(Singer singer)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Singers.SingleOrDefault(s => s.Name == singer.Name & s.LastName == singer.LastName);
                result.SingerSheetMusicList.Clear();
                db.SaveChanges();
            }
        }


    }
}