using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models
{
    public class SheetMusic
    {
        public int SheetMusicId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public enum MusicType
        {
            rozrywkowe,
            sakralne,
            musicalowe,
            ludowe,
            inne
        }

        public MusicType Type { get; set; }

        public SheetMusic()
        {

        }
        public SheetMusic(string title, string author, int Type)
        {
            this.Title = title;
            this.Author = author;
            this.Type = (MusicType)Type;
        }
    }
}