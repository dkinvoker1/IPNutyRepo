using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IPNuty.Models.Collections;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace IPNuty.Models.Managers.Singers
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


    }

    }
