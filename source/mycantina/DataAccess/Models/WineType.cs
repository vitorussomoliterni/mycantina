﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycantina.DataAccess.Models
{
    public class WineType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Bottle> Bottles { get; set; }
    }
}
