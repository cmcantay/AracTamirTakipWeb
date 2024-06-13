﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracTamirTakip.Entities.Servis
{
    public class Marka
    {
        [Key]
        public int MarkaId { get; set; }
        public string MarkaAd { get; set; }
        public bool Silindi { get; set; }
        public List<Model>Models { get; set; }
    }
}
