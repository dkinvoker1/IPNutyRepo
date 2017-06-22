using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Collections
{
    public sealed class OrdersCollection
    {   //tymczasowo singleton!

        #region singleton
        private static OrdersCollection ordersList = null;
        public static OrdersCollection Instance
        {
            get
            {
                if (ordersList == null)
                {
                    ordersList = new OrdersCollection();
                }
                return ordersList;
            }
        }
        #endregion

        public List<Order> AllOrdersList { get; set; }

        private OrdersCollection()
        {
            this.AllOrdersList = new List<Order>();

            AllOrdersList.Add(new Order.Builder(1, SingersCollection.Instance.AllSingersList[1]).Build());
            AllOrdersList.Add(new Order.Builder(2, SingersCollection.Instance.AllSingersList[2]).Build());
            AllOrdersList.Add(new Order.Builder(3, SingersCollection.Instance.AllSingersList[3]).Build());
        }

        // docelowo pobrana lista z DB



    }
}