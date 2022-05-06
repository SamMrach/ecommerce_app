using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projet1.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategorieController : Controller
    {
        // GET: Categorie
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetCategorie()
        {
            using (ModelBD dc = new ModelBD())
            {
                var categorie = dc.categories.OrderBy(a => a.cat_Id).ToList();
                return Json(new { data = categorie }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            ModelBD dc = new ModelBD();

            var v = dc.categories.Where(a => a.cat_Id == id).FirstOrDefault();
            return View(v);

        }

        [HttpPost]
        public ActionResult Save(ModelBD.categorie categorie1)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ModelBD dc = new ModelBD())
                {
                    if (categorie1.cat_Id > 0)
                    {
                        //Edit 
                        var v = dc.categories.Where(a => a.cat_Id == categorie1.cat_Id).FirstOrDefault();
                        if (v != null)
                        {
                            v.name = categorie1.name;
                            v.description = categorie1.description;
                           
                            //return Json(new { success = true, message = "updated Successfully" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //Save
                        dc.categories.Add(categorie1);
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
                var v = dc.categories.Where(a => a.cat_Id == id).FirstOrDefault();
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
                var v = dc.categories.Where(a => a.cat_Id == id).FirstOrDefault();
                if (v != null)
                {
                    dc.categories.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

    }
}