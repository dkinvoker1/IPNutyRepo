using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IPNuty.Models.Collections;

namespace IPNuty.Models.Managers.Singers
{
    public class OrdersManager
    {
        Order order;

        public void CreateNewOrder( Singer singer)
        {
            order = new Order.Builder(singer).SetOrderTime(DateTime.Now).SetOrderStatus(false).Build();
        }
         public List<Order> GetAllSingerOrders(Singer singer)
        {
            List<Order> singerOrdersList = OrdersCollection.GetAllOrders().Where(o => o.singer == singer && o.completed == false).ToList();
            return singerOrdersList;
        }
    }
}