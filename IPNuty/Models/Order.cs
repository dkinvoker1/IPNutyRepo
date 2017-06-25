using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public Singer singer;
        public List<SheetMusic> orderedSheetMusicList;
        public DateTime orderTime;
        public bool completed;

        public Order(Builder builder)
        {
            this.singer = builder.singer;
        }

        public class Builder
        {
            public Singer singer = null;
            public List<SheetMusic> orderedSheetMusicList = null;
            public DateTime orderTime = DateTime.Now;
            public bool completed = false;

            public Builder(Singer singer)
            {
                this.singer = singer;
            }

            public Builder SetOrderTime(DateTime orderTime)
            {
                this.orderTime = orderTime;
                return this;
            }

            public Builder SetOrderStatus(bool completed)
            {
                this.completed = completed;
                return this;
            }

            public Order Build()
            {
                return new Order(this);
            }

        }
    }
}