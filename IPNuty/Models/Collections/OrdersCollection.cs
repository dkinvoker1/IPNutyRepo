using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Collections
{
    public static class OrdersCollection
    {
        public static List<Oder> GetAllOrders()
        {
            using (ApplicationDbContext dbcontext = new ApplicationDbContext())
            {
                List<Order> AllOdersList = dbcontext.Orders.ToList();
                return AllOrdersList;
            }

        }
    }
}