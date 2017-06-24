using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Collections
{

    public static class SheetMusicCollection
    {

        public static List<SheetMusic> GetAllSheetMusic()
        {

            using (ApplicationDbContext dbcontext = new ApplicationDbContext())
            {
                List<SheetMusic> AllSheetMusic = dbcontext.SheetsOfMusic.ToList();
                return AllSheetMusic;
            }
            
        }

    }
}