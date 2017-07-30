using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models
{
    public class Singer
    {
        public int SingerId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool Activicity { get; set; }
        public DateTime JoiningDate { get; set; }
        public List<SheetMusic> SingerSheetMusicList { get; set; }

        public Singer()
        {

        }

        private Singer(Builder builder)
        {
            this.Name = builder.Name;
            this.LastName = builder.LastName;
            this.JoiningDate = builder.JoiningDate;
            this.Activicity = builder.Activicity;
        }

        public class Builder
        {
            public string Name = null;
            public string LastName = null;
            public bool Activicity = true;
            public DateTime JoiningDate = DateTime.Now;
            public List<SheetMusic> SingerSheetMusicList = null;

            public Builder(string name, string lastName)
            {
                this.Name = name;
                this.LastName = lastName;
            }

            public Builder SetActivicity(bool activicity)
            {
                this.Activicity = activicity;
                return this;
            }

            public Builder SetJoiningDate(DateTime joiningDate)
            {
                this.JoiningDate = joiningDate;
                return this;
            }

            public Singer build()
            {
                return new Singer(this);
            }
        }
    }
}