using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projet1.Controllers
{
    [Authorize(Roles = "proprietaire")]
    public class ProprietaireController : Controller
    {
        // GET: Proprietaire
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetProprietaire()
        {
            using (ModelBD dc = new ModelBD())
            {
                var proprietaire = dc.proprietaires.OrderBy(a => a.id).ToList();
                return Json(new { data = proprietaire }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            ModelBD dc = new ModelBD();

            var v = dc.proprietaires.Where(a => a.id == id).FirstOrDefault();
            return View(v);

        }

        [HttpPost]
        public ActionResult Save(ModelBD.proprietaire proprietaire)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ModelBD dc = new ModelBD())
                {
                    if (proprietaire.id > 0)
                    {
                        //Edit 
                        var v = dc.proprietaires.Where(a => a.id == proprietaire.id).FirstOrDefault();
                        if (v != null)
                        {
                            v.nom = proprietaire.nom;
                            v.prenom = proprietaire.prenom;
                            v.email = proprietaire.email;
                            v.tel = proprietaire.tel;
                            v.password = proprietaire.password;
                            v.passwordComfirm = proprietaire.passwordComfirm;

                            //return Json(new { success = true, message = "updated Successfully" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //Save
                        proprietaire.dateAjout = DateTime.Now;
                        dc.proprietaires.Add(proprietaire);
                        //return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                    }
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (ModelBD dc = new ModelBD())
            {
                var v = dc.proprietaires.Where(a => a.id == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (ModelBD dc = new ModelBD())
            {
                var v = dc.proprietaires.Where(a => a.id == id).FirstOrDefault();
                if (v != null)
                {
                    dc.proprietaires.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}