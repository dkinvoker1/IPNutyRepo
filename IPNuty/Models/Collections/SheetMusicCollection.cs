using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Collections
{
    public sealed class SheetMusicCollection
    {
        public static class SheetMusicCollection
        {

            public static List<SheetMusic> GetAllSheetMusic()
            {

                using (ApplicationDbContext dbcontext = new ApplicationDbContext())
                {
                    List<SheetMusic> AllSheetMusic = dbcontext.SheetsOfMusic.toList();
                }
                return AllSheetMusic;
            }

        }
    }
}