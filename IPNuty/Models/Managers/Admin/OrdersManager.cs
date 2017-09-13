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
            using (var db = new ApplicationDbContext())
            {
                db.Orders.Add(order);
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