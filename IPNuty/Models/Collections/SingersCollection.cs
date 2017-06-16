using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPNuty.Models.Collections
{
    public sealed class SingersCollection
    {
        #region Singleton

        private static SingersCollection singersList = null;
        public static SingersCollection Instance
        {
            get
            {
                if (singersList == null)
                {
                    singersList = new SingersCollection();
                }
                return singersList;
            }
        }

        #endregion

        public List<Singer> AllSingersList { get; set; }

        private SingersCollection()
        {
            // tu docelowo pobrana lista z bazy danych
            this.AllSingersList = new List<Singer>();
            // tymczasowo!
            AllSingersList.Add(new Singer.Builder("Magda", "Magdzińska").build());
            AllSingersList.Add(new Singer.Builder("Adam", "Adamowicz").build());
            AllSingersList.Add(new Singer.Builder("Krzysztof", "Krzysztofiński").build());
            AllSingersList.Add(new Singer.Builder("Anna", "Anińska").build());
            AllSingersList.Add(new Singer.Builder("Katarzyna", "Kasińska").build());
        }

    }
}