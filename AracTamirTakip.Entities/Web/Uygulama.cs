﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracTamirTakip.Entities.Web
{
    public class Uygulama
    {
        public int UygulamaId { get; set; }
        [MaxLength(100)]
        public string Baslik { get; set; }
        [MaxLength(100)]
        public string Resim{ get; set; }
    }
}
