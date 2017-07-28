using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IPNuty.Models;
using System.ComponentModel.DataAnnotations;

namespace IPNuty.ViewModels.Admin
{
    public class AddNewSingerViewModel
    {

        [Required]
        [Display(Name = "Imię")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Altywność")]
        public bool activicity { get; set; }

        [Required]
        [Display(Name = "Data dołączenia")]
        public DateTime joiningDate { get; set; }

        //[Required]
        //[Display(Name = "Lista nut")]
        //public List<SheetMusic> singerSheetMusicList { get; set; }
    }
}