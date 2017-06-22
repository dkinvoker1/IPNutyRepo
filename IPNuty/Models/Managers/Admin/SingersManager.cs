using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//u Oli tego nie było, do sprawdzenia potem
using IPNuty.Models.Collections;

namespace IPNuty.Models.Managers.Admin
{
    public class SingersManager
    {
        public List<Singer> allSingersList = SingersCollection.Instance.AllSingersList;

        public Singer CreateNewSinger(string name, string lastName)
        {
            Singer singer = new Singer.Builder(name, lastName).build();
            return singer;
        }

        public void SingerNameUpdate(Singer singer, string name)
        {
            singer.Name = name;
        }

        public void SingerLastNameUpdate(Singer singer, string lastName)
        {
            singer.LastName = lastName;
        }

        public void SingerActivicityUpdate(Singer singer, bool activicity)
        {
            singer.Activicity = activicity;
        }

        public void SingerJoiningDateUpdate(Singer singer, DateTime joiningDate)
        {
            singer.JoiningDate = joiningDate;
        }

        public void SingerSheetMusicListAdd(Singer singer, SheetMusic sheetMusic)
        {
            singer.SingerSheetMusicList.Add(sheetMusic);
        }

        public void SingerSheetMusicListRemove(Singer singer, SheetMusic sheetMusic)
        {
            singer.SingerSheetMusicList.Remove(sheetMusic);
        }

        public void SingerSheetMusicListClear(Singer singer)
        {
            singer.SingerSheetMusicList.Clear();
        }

        public void AddSinger(Singer singer)
        {
            allSingersList.Add(singer);
        }

        public void RemoveSinger(Singer singer)
        {
            allSingersList.Remove(singer);
        }
    }
}