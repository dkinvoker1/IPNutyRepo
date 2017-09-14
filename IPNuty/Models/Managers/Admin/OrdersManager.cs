using IPNuty.Models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Managers.Admin
{
    public class OrdersManager
    {

        public void AddOrder(Order order)
        {
            var thisOrder = order;

            using (var db = new ApplicationDbContext())
            {
                var singer = thisOrder.SingerId;
                var sheetMusic = thisOrder.SheetMusicId;

                var thisSinger = db.Singers.Where(e => e.SingerId == singer.SingerId && e.Name == singer.Name).FirstOrDefault();


                var sheetMusicId = db.SheetsOfMusic.Where(e => e.SheetMusicId == sheetMusic.SheetMusicId && e.Title == sheetMusic.Title).FirstOrDefault();

                Order newOrder = new Order.Builder(thisSinger).SetOrderedSheetMusic(sheetMusicId).SetOrderStatus(false).SetOrderTime(DateTime.Today).Build();

                db.Orders.Add(newOrder);
                db.SaveChanges();
            }

        }

        public void RemoveOrder(Order order)
        {
            using (var db = new ApplicationDbContext())
            {
                order = db.Orders.Where(e => e.OrderId == order.OrderId).FirstOrDefault();
                if (order != null)
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }
            }
        }

        public void OrderStatusUpdate(Order order, bool completed)
        {
            using (var db = new ApplicationDbContext())
            {
                var thisOrder = db.Orders.SingleOrDefault(s => s.OrderId == order.OrderId);
                thisOrder.Completed = completed;
                db.SaveChanges();

                // dodanie nut Singerowi
                var allSingers = SingersCollection.GetAllSingers();
                Singer thisSinger = allSingers.Where(e => e.SingerId == thisOrder.SingerId.SingerId).FirstOrDefault();

                if (thisSinger.SingerSheetMusicList == null)
                {
                    thisSinger.SingerSheetMusicList = new List<SheetMusic>();
                }

                var allSheetMusic = SheetMusicCollection.GetAllSheetMusic();
                var thisSheetMusic = allSheetMusic.Where(e => e.SheetMusicId == thisOrder.SheetMusicId.SheetMusicId).FirstOrDefault();

                var typ = thisSheetMusic.Type.GetHashCode();
                var sheetToAdd = new SheetMusic(thisSheetMusic.Title, thisSheetMusic.Author, typ);

                thisSinger.SingerSheetMusicList.Add(sheetToAdd);

                SingersCollection.UpdateSinger(thisSinger);

            }
        }

    }
}