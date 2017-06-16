using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models
{
    public class SheetMusic
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public SheetMusic(string title, string author)
        {
            this.Title = title;
            this.Author = author;
        }
    }
}