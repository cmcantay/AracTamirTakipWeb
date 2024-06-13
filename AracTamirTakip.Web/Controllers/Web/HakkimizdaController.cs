using AracTamirTakip.BusinessLayer.Abstract;
using AracTamirTakip.Entities.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AracTamirTakip.Web.Controllers.Web
{
    [Authorize(Roles = "Admin")]
    public class HakkimizdaController : Controller
    {
        private readonly Repository<Hakkimizda> rpHakkimizda = new Repository<Hakkimizda>();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            if (rpHakkimizda.List().Count == 0)
            {
                Hakkimizda k = new Hakkimizda();
                k.Icerik = "-";
                k.Resim = "-";
                rpHakkimizda.Insert(k);
            }
            var hakkimizda = rpHakkimizda.List().FirstOrDefault();
            return View(hakkimizda);
        }
        [HttpPost]
        public ActionResult HakkimizdaKaydet(Hakkimizda hakkimizda, HttpPostedFileBase Resim)
        {
            if (rpHakkimizda.List().Count > 0)
            {
                string uzanti = Path.GetExtension(Resim.FileName);
                string dosyaAdi = Path.GetFileNameWithoutExtension(Resim.FileName) + '_' + Guid.NewGuid() + uzanti;
                string yol = Server.MapPath("/Img/Hakkimizda/") + dosyaAdi;
                Resim.SaveAs(yol);
                WebImage image = new WebImage(yol);
                image.Resize(300, 300, true, true);
                image.Save(yol);
                var guncellenecek = rpHakkimizda.List().FirstOrDefault();
                guncellenecek.Icerik = hakkimizda.Icerik;
                guncellenecek.Resim = "/Img/Hakkimizda/" + dosyaAdi;
                rpHakkimizda.Update(guncellenecek);
            }
            return RedirectToAction("Index");
        }
    }
}