﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IPNuty.Models;
//u Oli tego nie było, sprawdzić potem
using IPNuty.Models.Collections;

namespace IPNuty.ViewModels.Admin
{
    public class SingersListAcctualizationViewModel
    {
        public Singer singer;
        public List<Singer> allSingersList = SingersCollection.Instance.AllSingersList;
    }
}