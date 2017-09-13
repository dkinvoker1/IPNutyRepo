using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Collections
{
    public static class OrdersCollection
    {
        public static List<Order> GetAllOrders()
        {
            using (ApplicationDbContext dbcontext = new ApplicationDbContext())
            {
                ApplicationDbContext db1 = new ApplicationDbContext();
                List<Order> ordersList = db1.Orders.ToList();
                List<Singer> singersList = db1.Singers.ToList();
                List<Singer> singersWhoOrder = singersList.Take(ordersList.Count).ToList();

                List<SheetMusic> sheetMusicList = db1.SheetsOfMusic.ToList();
                List<SheetMusic> sheetMusicOrdered = sheetMusicList.Take(ordersList.Count).ToList();

                for (int i = 0; i <= ordersList.Count - 1; i ++)
                {
                    singersWhoOrder[i] = singersList.Where(e => e.SingerId == ordersList[i].SingerId.SingerId).FirstOrDefault();
                    sheetMusicOrdered[i] = sheetMusicList.Where(e => e.SheetMusicId == ordersList[i].SheetMusicId.SheetMusicId).FirstOrDefault();
                }

                dbcontext.Configuration.LazyLoadingEnabled = false;

                List<Order> AllOrders = dbcontext.Orders.ToList();

                for (int i = 0; i <= AllOrders.Count - 1; i ++)
                {
                    AllOrders[i].SingerId = singersWhoOrder[i];
                    AllOrders[i].SheetMusicId = sheetMusicOrdered[i];

                }
                return AllOrders;
            }
            
            
        }

        public static List<Order> GetAllSingerOrders(Singer singer)
        {
            using (ApplicationDbContext dbcontext = new ApplicationDbContext())
            {
                List<Order> AllOrdersList = dbcontext.Orders.ToList();
                List<Order> singerOrdersList = AllOrdersList.Where(o => o.SingerId.SingerId == singer.SingerId && o.Completed == false).ToList();
                return singerOrdersList;
            }
        }

    }
}