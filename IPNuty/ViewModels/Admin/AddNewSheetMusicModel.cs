using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IPNuty.Models;
using System.ComponentModel.DataAnnotations;


namespace IPNuty.ViewModels.Admin
{
    public class AddNewSheetMusicModel
    {
        public SheetMusic sheetMusic;

        [Required]
        [Display(Name = "Tytuł")]
        public string title { get; set; }

        [Required]
        [Display(Name = "Autor")]
        public string author { get; set; }

        [Required]
        [Display(Name = "Typ")]
        public int type { get; set; }

    }
}