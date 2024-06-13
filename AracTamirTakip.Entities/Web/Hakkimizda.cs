﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AracTamirTakip.Entities.Web
{
  public class Hakkimizda
    {
        public int HakkimizdaId{ get; set; }
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        public string Icerik{ get; set; }
        public string Resim{ get; set; }
    }
}
