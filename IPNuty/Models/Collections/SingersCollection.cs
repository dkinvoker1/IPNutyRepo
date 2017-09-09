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
                    foreach (var item in updatedSinger.SingerSheetMusicList)
                    {
                        if (item.SingerID==null)
                        {
                            del.SingerSheetMusicList.Add(item);
                        }
                    }
                    dbcontext.Singers.AddOrUpdate(del);
                }
                dbcontext.SaveChanges();
            }

        }
    }
}