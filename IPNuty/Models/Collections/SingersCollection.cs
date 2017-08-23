using System;
using System.Collections.Generic;
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
                List<Singer> AllSingers = dbcontext.Singers.ToList();
                return AllSingers;
            }

        }

        public static void UpdateSinger(Singer updatedSinger)
        {
            using (ApplicationDbContext dbcontext = new ApplicationDbContext())
            {
                dbcontext.Singers.Remove(updatedSinger);
                dbcontext.Singers.Add(updatedSinger);
                dbcontext.SaveChangesAsync();
            }

        }
    }
}