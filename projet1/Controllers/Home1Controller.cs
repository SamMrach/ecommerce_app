using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using static projet1.ModelBD;
namespace projet1.Controllers
{
    
    public class Home1Controller : Controller
    {
        ModelBD model = new ModelBD();
        public ActionResult Index()
        {
            var produitsList = model.produits.ToList().Take(7);
            DateTime today = DateTime.Today;
            //client client = model.clients.Find(36);
           // proprietaire proprietaire = model.proprietaires.Find(34);
            /* var conversation = new conversation() { client = client, proprietaire = proprietaire };
             var message = new message() { text="hi do you offer a refund",conversation = conversation, date = today.ToString(), fromClient=true};
             conversation.LastMessage = message.text;
             conversation.LastMessageDate = message.date;
             model.conversations.Add(conversation);
             model.messages.Add(message);
             model.SaveChanges();*/

           // var particulier1 = model.proprietaires.Find(34);
            /*commande commande1 = new commande() { date = today.ToString(), montant = 22, quantite = 2, client = client };
            commande commande2 = new commande() { date = today.ToString(), montant = 32, quantite = 1, client = client };
            model.commandes.Add(commande2);
            model.commandes.Add(commande1);
            model.SaveChanges();
            
            var categorie1 = new categorie() { name = "parfums", description = "parfums agreables" };
            var categorie2 = new categorie() { name = "tech accessories", description = "technologies stuff" };
            
            var categorie1 = model.categories.Find(2);
            var produit1 = new produit() { proprietaire=particulier1,title = "apple airpods 3 generation", prix = 19, description = "Les AirPods vont changer à jamais votre façon d’utiliser des écouteurs. Une fois sortis de leur boîtier de charge, ils s’utilisent avec tous vos ",categorie=categorie1};
            var produit2 = new produit() { proprietaire = particulier1, title = "sauvage parfum homme", prix = 18, description = "Masculine and confident, the new Eros Eau de Parfum is a fragrance for a bold, passionate man. The sensual scent fuses woody, oriental and fresh", categorie = categorie1 };
            var produit3 = new produit() { proprietaire = particulier1, title = "Dior EAU SAUVAGE PARFUM ", prix = 18, description = "Dior EAU SAUVAGE PARFUM " , categorie = categorie1 };
            var produit4 = new produit() { proprietaire = particulier1, title = "BLEU DE CHANEL MINIATURE", prix = 18, description = "CHANEL BLEU DE CHANEL MINIATURE EAU DE PARFUM POUR HOMME 10 ML VIP GIFT", categorie = categorie1 };
            var produit5 = new produit() { proprietaire = particulier1, title = "Christian Dior Sauvage ", prix = 18, description = "Christian Dior Sauvage Edp Eau de Parfum", categorie = categorie1 };
            var produit6 = new produit() { proprietaire = particulier1, title = "BLEU DE CHANEL MINIATURE", prix = 18, description = "CHANEL BLEU DE CHANEL MINIATURE EAU DE PARFUM POUR HOMME 10 ML VIP GIFT", categorie = categorie1 };
            var produit7 = new produit() { proprietaire = particulier1, title = "dior sauvage eau de parfum", prix = 18, description = "dior sauvage eau de parfum", categorie = categorie1 };
           

            model.produits.Add(produit1);
            model.produits.Add(produit2);
            model.produits.Add(produit3);
            model.produits.Add(produit4);
            model.produits.Add(produit5);
            model.produits.Add(produit6);
            model.produits.Add(produit7);
            model.SaveChanges();
            */
            //var utilisateur2 = new utilisateur() { nom="ahmed",prenom="sam",email="ahmed@gmail.com",tel="06666666"};
            //model.utlisateurs.Add(utilisateur2);
            //model.SaveChanges();

            return View(produitsList);
           // return View();
        }
        [HttpPost]
        public ActionResult SignUp(utilisateur user1, string cin, string SIRN, string type_activite)
        {
            if (ModelState.IsValid)
            {
                using (var model = new ModelBD())
                {
                    if (Request["type"] == "client")
                    {
                        client client = new client(user1);
                        client.status = client.Status.normal;
                        client.dateAjout = DateTime.Now;
                        client.Role = "client";
                        model.clients.Add(client);
                        panier panier = new panier() { client = client };
                        model.paniers.Add(panier);
                        model.SaveChanges();
                    }
                    else if (Request["type"] == "particulier")
                    {
                        proprietaire proprietaire = new proprietaire(user1);
                        particulier particulier = new particulier(proprietaire, cin);
                        particulier.dateAjout = DateTime.Now;
                        particulier.Role = "proprietaire";
                        model.particuliers.Add(particulier);
                        model.SaveChanges();
                    }
                    else
                    {
                        proprietaire proprietaire = new proprietaire(user1);
                        societe societe = new societe(proprietaire, SIRN, type_activite);
                        societe.dateAjout = DateTime.Now;
                        societe.Role = "proprietaire";
                        model.societes.Add(societe);
                        model.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Login");
        }
        public ActionResult SignUp()
        {
            if (Request.IsAuthenticated)
                return Redirect(Url.Action("Index", "Client"));
            else
                return View();
        }
        public ActionResult Login()
        {
            if(Roles.IsUserInRole(User.Identity.Name,"client"))
                return Redirect(Url.Action("Index", "Client1"));
            else if(Roles.IsUserInRole(User.Identity.Name, "proprietaire"))
                return Redirect(Url.Action("Index", "HomeProp"));
            else if(Roles.IsUserInRole(User.Identity.Name, "admin"))
                return Redirect(Url.Action("Index", "Client"));
            //ViewBag.error = TempData["LoginError"];
            else
                return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password, string returnUrl)
        {
            if (model.clients.Any(u => u.email == email && u.password == password))
            {
                FormsAuthentication.SetAuthCookie(email, false);
                return RedirectToAction("Index");

            }
            else if(model.proprietaires.Any(u => u.email == email && u.password == password))
            {
                FormsAuthentication.SetAuthCookie(email, false);
                return RedirectToAction("Index","HomeProp");
            }else if(email=="admin@shop.com" && password == "admin")
            {
                FormsAuthentication.SetAuthCookie(email, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["LoginError"] = "email ou mot de pass incorrect";
                return Redirect(returnUrl);
            }
        }
        public ActionResult Product(int id)
        {

            var product = model.produits.Find(id);
            return View(product);
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
        
        public ActionResult Search(string title)
        {
            var produits = model.produits.Where(p => p.title.Contains(title)).ToList();
            return View(produits);
        }
       
        public ActionResult SearchByCategory(string category)
        {
            var produits = model.produits.Where(p => p.categorie.name == category);
            return View(produits);
        }
    }
}