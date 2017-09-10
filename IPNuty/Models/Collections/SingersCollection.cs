using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Collections
{
    public static class SingersCollection
    {
        public static List<Singer> GetAllSingers()
        {
            using (ApplicationDbContext dbcontext = new ApplicationDbContext())
            {
                dbcontext.Configuration.LazyLoadingEnabled = false;
                List<Singer> AllSingers = dbcontext.Singers.ToList();
                foreach (var singer in AllSingers)
                {
                    singer.SingerSheetMusicList = new List<SheetMusic>();
                    var singerSheets = dbcontext.SheetsOfMusic.Where(e=>e.SingerID.SingerId==singer.SingerId).ToList();
                    if(singerSheets.Count>0)
                    {
                        singer.SingerSheetMusicList = singerSheets;
                    }
                }
                    return AllSingers;
            }

        }

        public static void UpdateSinger(Singer updatedSinger)
        {
            using (ApplicationDbContext dbcontext = new ApplicationDbContext())
            {
                var del = dbcontext.Singers.Where(e => e.Name == updatedSinger.Name && e.LastName == updatedSinger.LastName).FirstOrDefault();
                if (del != null)
                {
                    if (del.SingerSheetMusicList.Count< updatedSinger.SingerSheetMusicList.Count)
                    {
                        foreach (var item in updatedSinger.SingerSheetMusicList)
                        {
                            if (item.SingerID == null)
                            {
                                del.SingerSheetMusicList.Add(item);
                            }
                        }
                    }
                    else if(del.SingerSheetMusicList.Count == updatedSinger.SingerSheetMusicList.Count)
                    {
                        return;
                    }
                    else if (del.SingerSheetMusicList.Count > updatedSinger.SingerSheetMusicList.Count)
                    {
                        var toBeLeft = updatedSinger.SingerSheetMusicList.Select(e => e.SheetMusicId);
                        var toBeRemoved = new List<SheetMusic>(del.SingerSheetMusicList.Where(e => !toBeLeft.Contains(e.SheetMusicId)));
                        foreach (var item in toBeRemoved)
                        {
                            del.SingerSheetMusicList.Remove(item);
                            dbcontext.SheetsOfMusic.Remove(item);
                        }
                    }
                    dbcontext.Singers.AddOrUpdate(del);
                }
                dbcontext.SaveChanges();
            }

        }
    }
}