using projet1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Net;
using static projet1.ModelBD;

namespace projet1.Controllers
{
    [Authorize(Roles = "admin")]

    public class HomeController : Controller
    {
        ModelBD db = new ModelBD();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetProprietaire()
        {
            ModelBD db = new ModelBD();
            //DateTime mydate = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy"+ " 00:00:00", System.Globalization.CultureInfo.InvariantCulture);
            //Console.WriteLine(mydate);
            var prpActue = db.proprietaires.OrderBy(x => x.id).Where(x=>x.dateAjout.Day.Equals(DateTime.Now.Day) && x.dateAjout.Month.Equals(DateTime.Now.Month) && x.dateAjout.Year.Equals(DateTime.Now.Year)).ToList();
            return Json(new { data = prpActue }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetClient()
        {
            ModelBD db = new ModelBD();
        
        var prpActue = db.clients.OrderBy(x => x.id).Where(x => x.dateAjout.Day.Equals(DateTime.Now.Day) && x.dateAjout.Month.Equals(DateTime.Now.Month) && x.dateAjout.Year.Equals(DateTime.Now.Year)).ToList();
            return Json(new { data = prpActue}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
           
           
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return Redirect(Url.Action("Index", "Home1"));
        }
        public ActionResult profile()
        {
            ModelBD db = new ModelBD();
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.utlisateurs.Where(p => p.email == email).FirstOrDefault();
            var prfil = db.utlisateurs.Find(currentUser.id);
            return View(prfil);
        }
        public ActionResult Editprofile(int? id)
        {
            ModelBD db = new ModelBD();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            utilisateur pdt = db.utlisateurs.Find(id);
            if (pdt == null)
            {
                return HttpNotFound();
            }
            return View(pdt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editprofile(utilisateur user)
        {
            var u = db.utlisateurs.Where(a => a.id == user.id).FirstOrDefault();
            u.nom = user.nom;
            u.prenom = user.prenom;
            u.email = user.email;
            u.password = user.password;
            u.passwordComfirm = user.passwordComfirm;
            u.tel = user.tel;

            historique h = new historique();
            h.operation = historique.Operation.Modification;
            h.utilisateurId = user.id;
            h.dateOperation = DateTime.Now;
            h.label = "Vos informations personnelles";

            db.historiques.Add(h);

            db.SaveChanges();
            TempData["AlertMessage"] = "Vous avez modifier vos informations avec succès";
            ModelState.Clear();

            return RedirectToAction("profile");

        }
    }
}