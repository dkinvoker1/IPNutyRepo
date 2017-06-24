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

        public void CreateNewOrder(int id, Singer singer)
        {
            order = new Order.Builder(id, singer).Build();
        }

        public void AddSheetMusic(SheetMusic sheetMusic)
        {
            if (order != null)
            {
                order.orderedSheetMusicList.Add(sheetMusic);
            }
        }

        public void RemoveSheetMusic(SheetMusic sheetMusic)
        {
            if (order != null)
            {
                order.orderedSheetMusicList.Remove(sheetMusic);
            }
        }


        public void ExecuteOrder(Order order)
        {
            if (order != null)
            {
                bool isEmpty = !order.orderedSheetMusicList.Any();
                if (!isEmpty)
                {
                    OrdersCollection.GetAllOrdersList().Add(order);
                }
            }
        }
    }
}