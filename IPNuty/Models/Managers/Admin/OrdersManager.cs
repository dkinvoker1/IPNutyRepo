using IPNuty.Models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Managers.Admin
{
    public class OrdersManager
    {
        public List<Order> allOrdersList = OrdersCollection.GetAllOrders();

        public void CompleteOrder(Order order)
        {
            order.completed = true;
        }
    }
}