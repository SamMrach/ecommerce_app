using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
namespace projet1.Controllers
{
    [Authorize(Roles = "admin")]

    public class CommandeController : Controller
    {
        ModelBD bd = new ModelBD();
        // GET: Produit
        [HttpGet]
        // GET: Commande
        public ActionResult Index(String surchtext, int? i)
        {
            return View(bd.commandes.Where(x => x.client.nom.Contains(surchtext) || x.client.prenom.Contains(surchtext) || surchtext == null).ToList().ToPagedList(i ?? 1, 2));
        }
        public ActionResult Delete(int? id)
        {
            ModelBD db = new ModelBD();

            var obj = db.commandes.Where(x => x.reference_cmd == id).First();
            db.commandes.Remove(obj);
            db.SaveChanges();



            var res = db.commandes.ToList().ToPagedList(1, 2);
            return View("Index", res);
        }
    }
}