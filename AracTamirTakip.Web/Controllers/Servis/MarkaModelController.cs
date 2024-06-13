using AracTamirTakip.BusinessLayer.Abstract;
using AracTamirTakip.Entities.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AracTamirTakip.Web.Controllers.Servis
{
    public class MarkaModelController : Controller
    {
        private readonly Repository<Model> rpModeller = new Repository<Model>();
        private readonly Repository<Marka> rpMarkalar = new Repository<Marka>();

        // GET: MarkaModel
        public ActionResult Index()
        {

            return View(rpMarkalar.Get(x=> x.Silindi==false).OrderBy(x=>x.MarkaAd).ToList());
        }
        public JsonResult ModelDoldur(int markaid)
        {
            var modeller = rpModeller.Get(x => x.MarkaId == markaid).Select(x => new { x.ModelId, x.ModelAd }).ToList();
            return Json(modeller, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MarkaKaydet(Marka marka)
        {
            if (rpMarkalar.Get(x=>x.MarkaAd == marka.MarkaAd).Any())
            {
                TempData["No"] = "Bu marka zaten kayıtlı";
            }
            else
            {
                rpMarkalar.Insert(marka);
            }
            
            return RedirectToAction("Index");
        }
        public ActionResult ModelListesi(int markaid)
        {
            var marka = rpMarkalar.GetById(markaid);
            ViewBag.Title = marka.MarkaAd + " Modelleri";
            ViewBag.MarkaId = markaid;
            return View(rpModeller.Get(x=>x.MarkaId==markaid && x.Silindi==false).ToList());
        }
        public ActionResult ModelKaydet(Model model)
        {
            rpModeller.Insert(model);
            TempData["Ok"] = model.ModelAd + " kaydedildi.";
            return RedirectToAction("ModelListesi", new { markaid = model.MarkaId });
        }
        [HttpPost]
        public ActionResult MarkaSil(int id)
        {
            var marka = rpMarkalar.GetById(id);
            marka.Silindi = true;
            rpMarkalar.Update(marka);
            TempData["Ok"] = marka.MarkaAd + " markası silindi.";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ModelSil(int id)
        {
            var model = rpModeller.GetById(id);
            model.Silindi = true;
            rpModeller.Update(model);
            TempData["Ok"] = model.ModelAd + " modeli silindi.";
            return RedirectToAction("ModelListesi",new {markaid=model.MarkaId});
        }
    }
}