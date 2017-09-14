using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public bool Completed { get; set; }
        public virtual SheetMusic SheetMusicId { get; set; }
        public virtual Singer SingerId { get; set; }

        public Order()
        {
        }

        public Order(Builder builder)
        {
            this.SingerId = builder.singer;
            this.SheetMusicId = builder.sheetMusic;
            this.OrderTime = builder.orderTime.Date;
            this.Completed = builder.completed;
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