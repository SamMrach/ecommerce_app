using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static projet1.ModelBD;

namespace projet1.Controllers
{
    [Authorize(Roles="client")]
    public class Client1Controller : Controller
    {
        ModelBD model = new ModelBD();
        // GET: Client

        public ActionResult Index()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = model.utlisateurs.Where(p => p.email == email).FirstOrDefault();
            //var currentUser = model.utlisateurs.Find(6);
            return View(currentUser);
        }
        public ActionResult Commande()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = model.utlisateurs.Where(p => p.email == email).FirstOrDefault();

            List<commande> commandes = model.commandes.Where(p => p.client.id == currentUser.id).ToList();
            List<commande_produit> commandes_produits = model.commandes_produits.ToList();
            dynamic mymodel = new ExpandoObject();
            mymodel.Commandes = commandes;
            mymodel.commande_produits = commandes_produits;
            return View(mymodel);
        }
        public ActionResult Message()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = model.utlisateurs.Where(p => p.email == email).FirstOrDefault();

            var conversations = model.conversations.Where(p => p.clientId == currentUser.id).ToList();
            var messages = model.messages.Where(p => p.conversation.clientId == currentUser.id).ToList();
            dynamic mymodel = new ExpandoObject();
            mymodel.conversations = conversations;
            mymodel.messages = messages;
            return View(mymodel);
        }
        public ActionResult Disputes()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = model.utlisateurs.Where(p => p.email == email).FirstOrDefault();

            var reclamations = model.reclamations.Where(p=>p.clientId==currentUser.id).ToList();

            return View(reclamations);
        }
        [HttpPost]
        public ActionResult AjouterPanier(string size, int id)
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var clientCurrent = model.clients.Where(p => p.email == email).FirstOrDefault();

            panier panier = model.paniers.Where(p => p.client_Id == clientCurrent.id).FirstOrDefault();

            produit produit = model.produits.Find(id);
            panier_produit pp = new panier_produit() { panier = panier, produit = produit, size = size };
            model.paniers_produit.Add(pp);
            model.SaveChanges();
            //clientCurrent.panier = new Dictionary<produit, int>() { };


            return RedirectToAction("Panier");
        }
        public ActionResult Panier()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var clientCurrent = model.clients.Where(p => p.email == email).FirstOrDefault();

            panier panier = model.paniers.Where(p => p.client_Id == clientCurrent.id).FirstOrDefault();

            List<panier_produit> articles = model.paniers_produit.Where(p => p.panier_id == panier.id).ToList();
            //pour finaliser commande
            ViewBag.panierId = panier.id;
            //clientCurrent.panier = new Dictionary<produit, int>() { };
            //<produit> produits = clientCurrent.panier.Keys.ToList();
            return View(articles);
        }
        public ActionResult Delete(int id)
        {
            panier_produit pp = model.paniers_produit.Find(id);
            model.paniers_produit.Remove(pp);
            model.SaveChanges();
            return RedirectToAction("Panier");
        }
        [HttpPost]
        public ActionResult Panier(produit p, int quantity)
        {

            return View();
        }
        public ActionResult updateArticle(int id, int quantity)
        {
            panier_produit p = model.paniers_produit.Find(id);
            p.quantity = quantity;
            model.SaveChanges();
            return RedirectToAction("Panier");
        }
        public ActionResult confirmation(int commandeId)
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentClient = model.clients.Where(p => p.email == email).FirstOrDefault();
            TempData["name"] = currentClient.nom;
            commande commande = model.commandes.Find(commandeId);
            return View(commande);
        }
        public ActionResult checkout(int panierId)
        {
            var panier_produits = model.paniers_produit.Where(p => p.panier_id == panierId).ToList();
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentClient = model.clients.Where(p => p.email == email).FirstOrDefault();

            var commande = new commande() { date = DateTime.Now.ToString(), client = currentClient };
            ViewBag.commandeId = commande.reference_cmd;
            model.commandes.Add(commande);
            model.SaveChanges();
            var quantity = 0;
            double montant = 0;
            foreach (var panier_produit in panier_produits)
            {
                quantity += panier_produit.quantity;
                montant += panier_produit.quantity * panier_produit.produit.prix;
                var commande_produit = new commande_produit() { commande = commande, quantity = panier_produit.quantity, produit = panier_produit.produit };
                model.commandes_produits.Add(commande_produit);
                model.SaveChanges();
                commande.label = panier_produit.produit.title;
                model.paniers_produit.Remove(panier_produit);
                model.SaveChanges();
            }
            commande.montant = montant;
            commande.quantite = quantity;
            if (panier_produits.Count != 0)
            {

            }

            model.SaveChanges();
            //model.paniers_produit.Remove(panier_produits);
            return RedirectToAction("confirmation",new { commandeId=commande.reference_cmd});
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return Redirect(Url.Action("Index", "Home1"));
        }

        public ActionResult reclamer(int id)
        {
            var commande = model.commandes.Find(id);

            return View(commande);
        }
        [HttpPost]
        public ActionResult reclamer(int client_id, int reference_cmd, string title, string description)
        {
            var client = model.clients.Find(client_id);
            var commande = model.commandes.Find(reference_cmd);
            var commande_produit = model.commandes_produits.Where(p => p.cmd_id == reference_cmd).FirstOrDefault();
            var proprietaire = commande_produit.produit.utilisateur;
            
            var reclamation = new reclamation() { header = title, text = description, commande = commande, client = client, utilisateur = proprietaire ,status="en attente"};
            model.reclamations.Add(reclamation);
            model.SaveChanges();
            return RedirectToAction("Disputes");
        }
        [HttpPost]
        public ActionResult Edit(utilisateur user)
        {
            utilisateur current = model.utlisateurs.Find(user.id);
            current.nom = user.nom;
            current.prenom = user.prenom;
            current.email = user.email;
            current.tel = user.tel;
            current.password = user.password;
            current.address = user.address;
            try
            {
                model.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

            return RedirectToAction("Index");
        }
        public ActionResult createMessage(int conversationId, string text)
        {
            var conversation = model.conversations.Find(conversationId);
            DateTime date = DateTime.Now;
            var message = new message() { text = text, fromClient = true, conversation = conversation, date = date.ToString() };
            conversation.LastMessage = text;
            conversation.LastMessageDate = date.ToString();
            model.messages.Add(message);
            model.SaveChanges();
            return RedirectToAction("Message");
        }
        
        public ActionResult contact(int proprietaireId, string message)
        {
            var conversation=new conversation() { };
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var clientCurrent = model.clients.Where(p => p.email == email).FirstOrDefault();
            var proprietaire = model.proprietaires.Find(proprietaireId);
            if (!model.conversations.Any(p=>p.proprietaireId==proprietaireId && p.clientId == clientCurrent.id))
            {
             conversation = new conversation() { client = clientCurrent, proprietaire = proprietaire, LastMessage = message, LastMessageDate = DateTime.Now.ToString() };
             model.conversations.Add(conversation);
            }
            else
            {
                 conversation = model.conversations.Where(p => p.proprietaireId == proprietaireId && p.clientId == clientCurrent.id).FirstOrDefault();

            }
            var newMessage = new message() { conversation = conversation, text = message, date = DateTime.Now.ToString(), fromClient = true };
            
            model.messages.Add(newMessage);
            model.SaveChanges();

            return RedirectToAction("Index");
        }/*
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Search(string category, string byCategory)
        {
            var produits = model.produits.Where(p => p.categorie.name == category).ToList();
            return View(produits);
        }*/
    }

}
