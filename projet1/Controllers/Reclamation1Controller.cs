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

    public class Reclamation1Controller : Controller
    {
        // GET: Reclamation1
        ModelBD dc = new ModelBD();
        // GET: Reclamation
        [HttpGet]
        public ActionResult Index(String surchtext, int? i)
        {
            int number;

            if (int.TryParse(surchtext, out number) == true)
            {
                int result = int.Parse(surchtext);
                return View(dc.reclamations.Where(x => x.CommandeId.Equals(result) || x.id.Equals(result) || surchtext == null).ToList().ToPagedList(i ?? 1, 2));
            }
            else
            {
                return View(dc.reclamations.Where(x => x.client.nom.Contains(surchtext) || x.client.prenom.Contains(surchtext) || x.header.Contains(surchtext) || x.text.Contains(surchtext) || surchtext == null).ToList().ToPagedList(i ?? 1, 2));
            }

        }





        public ActionResult Delete(int? id)
        {
           
                ModelBD db = new ModelBD();

                var obj = db.reclamations.Where(x => x.id== id).First();
                db.reclamations.Remove(obj);
                db.SaveChanges();



                var res = db.reclamations.ToList().ToPagedList(1, 2);
                return View("Index", res);


            
        }


    }
}
