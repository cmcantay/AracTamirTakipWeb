﻿using AracTamirTakip.BusinessLayer.Abstract;
using AracTamirTakip.Entities.Servis;
using AracTamirTakip.Entities.Web;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AracTamirTakip.Web.Views.Shared
{
    public partial class RaporPDF : System.Web.Mvc.ViewPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int isemriid = Convert.ToInt32(ViewBag.IsemriId);
                    Repository<Islem> rpIslem = new Repository<Islem>();
                    Repository<HaritaIletisim> rpIletisim = new Repository<HaritaIletisim>();
                    Repository<Isemri> rpIsemri = new Repository<Isemri>();
                    var islemler = rpIslem.Get(x => x.IsemriId == isemriid).ToList();
                    var detay = rpIsemri.Get(x => x.IsemriId == isemriid,includeProperties:"Musteri,Model").FirstOrDefault();

                    ReportParameter[] prm = new ReportParameter[13];
                    prm[0] = new ReportParameter("Unvan", rpIletisim.Get().FirstOrDefault().Unvan);
                    prm[1] = new ReportParameter("Iletisim", Regex.Replace(rpIletisim.Get().FirstOrDefault().Iletisim, "<.*?>", string.Empty));
                    prm[2] = new ReportParameter("AdSoyad",detay.Musteri.AdSoyad);
                    prm[3] = new ReportParameter("Marka",detay.Model.Marka.MarkaAd);
                    prm[4] = new ReportParameter("Model",detay.Model.ModelAd);
                    prm[5] = new ReportParameter("Plaka",detay.Plaka);
                    prm[6] = new ReportParameter("AracKm",detay.AracKm.ToString());
                    prm[7] = new ReportParameter("SaseNo",detay.SaseNo);
                    prm[8] = new ReportParameter("ModelYil",detay.ModelYil.ToString());
                    prm[9] = new ReportParameter("Telefon",detay.Musteri.Telefon);
                    prm[10] = new ReportParameter("Adres",detay.Musteri.Adres);
                    prm[11] = new ReportParameter("OdemeSekli",detay.OdemeSekli);
                    prm[12] = new ReportParameter("AlinanUcret",detay.AlinanUcret.ToString());


                    

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/RaporDizayn/RaporSonuc.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rds = new ReportDataSource("dsIslemler", islemler);
                    ReportViewer1.LocalReport.SetParameters(prm);
                    ReportViewer1.LocalReport.DataSources.Add(rds);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}