﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnSale.Common.Entities
{
    public class RegionalBloc
    {
        public string acronym { get; set; }
        public string name { get; set; }
        public List<object> otherAcronyms { get; set; }
        public List<object> otherNames { get; set; }
    }
}
