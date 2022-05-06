using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static projet1.ModelBD;

namespace projet1.Controllers
{
    [Authorize(Roles="proprietaire")]
    public class HomePropController : Controller
    {
        ModelBD db = new ModelBD();
        // GET: HomeProp
        public ActionResult Index()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.utlisateurs.Where(p => p.email == email).FirstOrDefault();
            var product = db.produits.OrderBy(a => a.ref_Id).Where(a => a.utilisateurId == currentUser.id).ToList();

           // var product = db.produits.OrderBy(a => a.ref_Id).ToList();
            return View(product);
        }


        public ActionResult Historique()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.utlisateurs.Where(p => p.email == email).FirstOrDefault();
            var hist = db.historiques.OrderBy(a => a.Id_hist).Where(a => a.utilisateurId == currentUser.id).ToList();

            //var hist = db.historiques.OrderBy(a => a.dateOperation).ToList();
            
            return View(hist);
        }

        [HttpGet]
        public ActionResult Deletehistorique(int id)
        {
            db.historiques.Remove(db.historiques.Find(id));
            db.SaveChanges();
            return RedirectToAction("Historique");
        }


        public ActionResult profile()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.utlisateurs.Where(p => p.email == email).FirstOrDefault();
            var prfil = db.utlisateurs.Find(currentUser.id);

            return View(prfil);

           
        }

        public ActionResult Editprofile(int? id)
        {

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
            u.prenom= user.prenom;
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



        // GET: Reclamation
        
        public ActionResult Disputes(String search, int? i)
        {
            //var email = "ddddd@gmail.com";
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.proprietaires.Where(p => p.email == email).FirstOrDefault();
            var MesDisp = db.reclamations.Where(x => x.utilisateurId == currentUser.id).ToList();
            //var MesDisp = db.reclamations.Find(2);
            return View(MesDisp);


        }      

       
        public ActionResult SendMessage(int? clientId)
        {
           
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var PropCurrent = db.proprietaires.Where(p => p.email == email).FirstOrDefault();

            var msg = db.messages.Where(x => x.conversation.clientId == clientId && x.conversation.proprietaireId == PropCurrent.id);

            return View(msg);
        }

       
            [HttpPost]
        public ActionResult Send(int clientId, int conversationId,string text)
        {
            var conversation = db.conversations.Find(conversationId);
            DateTime date = DateTime.Now;
            var message = new message() { text = text, fromClient = false, conversation = conversation, date = date.ToString() };
            conversation.LastMessage = text;
            conversation.LastMessageDate = date.ToString();
            db.messages.Add(message);
            db.SaveChanges();

            
            return RedirectToAction("Index");
            
        }

        public ActionResult Reception()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var PropCurrent = db.proprietaires.Where(p => p.email == email).FirstOrDefault();

            var conversations = db.conversations.Where(p => p.proprietaireId == PropCurrent.id).ToList();
            var messages = db.messages.Where(p => p.conversation.proprietaireId == PropCurrent.id).ToList();

            dynamic mymodel = new ExpandoObject();
            mymodel.conversations = conversations;
            mymodel.messages = messages;
            return View(mymodel);

            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); 
            return Redirect(Url.Action("Login", "Home1"));
        }
        public ActionResult AcceptReclamation(int id)
        {

            var MesDisp = db.reclamations.Find(id);

            if (MesDisp.status == "Refusée")
            {
                TempData["Decision"] = "Vous avez dejâ refuser cette reclamation";
                return RedirectToAction("Disputes");
            }
            else
            {
                MesDisp.status = "Acceptée";
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Disputes");
            }

        }

        public ActionResult RefuseReclamation(int id)
        {

            var MesDisp = db.reclamations.Find(id);

            if (MesDisp.status == "Acceptée")
            {
                TempData["Decision"] = "Vous avez dejâ l'cceptée";
                return RedirectToAction("Disputes");

            }
            else
            {
                MesDisp.status = "Refusée";
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Disputes");
            }

        }
    }
}