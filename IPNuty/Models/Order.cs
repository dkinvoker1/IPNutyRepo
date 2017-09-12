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
        public DateTime orderTime;
        public bool completed;
        public SheetMusic SheetMusic;

        public Order()
        {
        }

        public Order(Builder builder)
        {
            this.singer = builder.singer;
        }

        public class Builder
        {
            public Singer singer = null;
            public DateTime orderTime = DateTime.Now;
            public bool completed = false;
            public SheetMusic sheetMusic = null;

            public Builder(Singer singer)
            {
                this.singer = singer;
            }
            public Builder SetOrderedSheetMusic(SheetMusic sheetMusic)
            {
                this.sheetMusic = sheetMusic;
                return this;
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