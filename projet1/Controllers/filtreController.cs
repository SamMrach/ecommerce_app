using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static projet1.ModelBD;

namespace projet1.Controllers
{
    [Authorize(Roles = "admin")]

    public class filtreController : Controller
    {
        // GET: filtre
        [HttpGet]
        public ActionResult Index(string surchtext)
        {
            ModelBD bd = new ModelBD();

            return View(bd.produits.Where(x => x.title.Contains(surchtext) || surchtext == null).ToList());
       
        }
    }
}