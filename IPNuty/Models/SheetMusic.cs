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

        public enum MusicType
        {
            rozrywkowe,
            sakralne,
            musicalowe,
            ludowe;
        }

        public MusicType Type { get; set; }

        public SheetMusic(string title, string author, MusicType Type)
        {
            this.Title = title;
            this.Author = author;
            this.Type = Type;
        }
    }
}