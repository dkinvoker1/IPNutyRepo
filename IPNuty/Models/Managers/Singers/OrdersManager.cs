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
        Order order;

        public void AddOrder(Order order)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            } 
               
        }

    }
}