using AracTamirTakip.BusinessLayer.Abstract;
using AracTamirTakip.Entities.Servis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Repository<Musteri> rpMusteri = new Repository<Musteri>();
        [HttpGet]
        public IActionResult Musteri()
        {
            return Ok(rpMusteri.Get().OrderByDescending(x => x.MusteriId).Take(20).ToList());

        }
    }
}
