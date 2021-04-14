﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCPartForum.Models
{
    public class Electronic
    {
        public int ProductId { get; set; }

        public string Manufacturer { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }
    }
}
