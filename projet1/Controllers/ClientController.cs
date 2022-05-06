
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace projet1.Controllers
{
    [Authorize(Roles = "admin")]

    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetClient()
        {
            using (ModelBD dc = new ModelBD())
            {
                var client = dc.clients.OrderBy(a => a.id).ToList();
              
                return Json(new { data= client}, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            ModelBD dc = new ModelBD();

            var v = dc.clients.Where(a => a.id == id).FirstOrDefault();
            return View(v);

        }

        [HttpPost]
        public ActionResult Save(ModelBD.client client)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (ModelBD dc = new ModelBD())
                {
                    if (client.id > 0)
                    {
                        //Edit 
                        var v = dc.clients.Where(a => a.id == client.id).FirstOrDefault();
                        if (v != null)
                        {
                            v.nom = client.nom;
                            v.prenom = client.prenom;
                            v.email = client.email;
                            v.tel = client.tel;
                            v.status = client.status;
                            v.password = client.password;
                            v.passwordComfirm = client.passwordComfirm;
                            //return Json(new { success = true, message = "updated Successfully" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        //Save
                        client.dateAjout = DateTime.Now;
                        dc.clients.Add(client);
                        // return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
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
                var v = dc.clients.Where(a => a.id == id).FirstOrDefault();
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
                var v = dc.clients.Where(a => a.id == id).FirstOrDefault();
                if (v != null)
                {
                    dc.clients.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
        
    }
}