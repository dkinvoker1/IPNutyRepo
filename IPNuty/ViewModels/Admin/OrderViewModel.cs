using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IPNuty.Models.Collections;
using IPNuty.Models;

namespace IPNuty.ViewModels.Admin
{
    public class OrderViewModel
    {
        public List<Order> AllOrdersList = OrdersCollection.Instance.AllOrdersList;
    }
}