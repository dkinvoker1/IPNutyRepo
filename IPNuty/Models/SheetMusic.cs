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

        public virtual Singer SingerID { get; set; }

        public MusicType Type { get; set; }

        public enum MusicType
        {
            rozrywkowe,
            sakralne,
            musicalowe,
            ludowe,
            inne
        }



        public SheetMusic()
        {

        }

        public SheetMusic(string title, string author, int type)
        {
            this.Title = title;
            this.Author = author;
            this.Type = (MusicType)type;
        }
    }
}