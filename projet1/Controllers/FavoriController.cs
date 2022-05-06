using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projet1.Controllers
{
    [Authorize(Roles = "admin")]

    public class FavoriController : Controller
    {
        // GET: Favori
        public ActionResult Index()
        {
            ModelBD db = new ModelBD();
            var list = db.clients.Where(a=>a.status!=0).ToList();
            return View(list);
        }
        public ActionResult AddFavori(string[] ids)
        {
            int[] id = null;
            if (ids != null)
            {
                id = new int[ids.Length];
                int j = 0;
                foreach (string i in ids)
                {
                    int.TryParse(i, out id[j++]);
                }
            }

            if (id != null && id.Length > 0)
            {
              
                using (ModelBD db = new ModelBD())
                {
                   var allSelected = db.clients.Where(a => id.Contains(a.id)).ToList();
                    foreach (var i in allSelected)
                    {
                        i.status = ModelBD.client.Status.favorie;
                    }
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
    }
}