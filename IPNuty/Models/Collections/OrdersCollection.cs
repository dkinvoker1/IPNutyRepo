﻿using System;
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
                List<Order> AllOrdersList = dbcontext.Orders.ToList();
                return AllOrdersList;
            }

        }

        public static List<Order> GetAllSingerOrders(Singer singer)
        {
            List<Order> singerOrdersList = OrdersCollection.GetAllOrders().Where(o => o.SingerId == singer && o.Completed == false).ToList();
            return singerOrdersList;
        }

    }
}