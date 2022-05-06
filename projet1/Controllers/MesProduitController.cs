using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static projet1.ModelBD;

namespace projet1.Controllers
{
    [Authorize(Roles = "proprietaire")]

    public class MesProduitController : Controller
    {
        ModelBD db = new ModelBD();

        [HttpGet]
        public ActionResult SearchProduits(String search)
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.utlisateurs.Where(p => p.email == email).FirstOrDefault();

            var P = db.produits.Where(a => a.utilisateurId == currentUser.id).ToList();
            var MesProduits = P.Where(x => x.description.Contains(search) || x.title.Contains(search) || x.prix.ToString() == search || search == null).ToList();

            return View(MesProduits);
        }
        // GET: MesProduit
        [HttpGet]
        public ActionResult Mesproduits()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.utlisateurs.Where(p => p.email == email).FirstOrDefault();

            var P = db.produits.Where(a => a.utilisateurId == currentUser.id).ToList();
           // var MesProduits = P.Where(x => x.description.Contains(search) || x.title.Contains(search) || x.prix.ToString() == search || search == null).ToList();
        
            //var MesProduits = db.produits.Where(x => x.description.Contains(search) || x.title.Contains(search) || x.prix.ToString() == search || search == null).ToList();
            return View(P);
        }

        [HttpGet]
        public ActionResult NouveauProduit()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NouveauProduit(produit p, int CategorieId)
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.utlisateurs.Where(x => x.email == email).FirstOrDefault();

            string filename = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
            string extension = Path.GetExtension(p.ImageFile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            p.ImagePath = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            p.ImageFile.SaveAs(filename);

            produit p1 = new produit();
            p1.title = p.title;
            p1.description = p.description;
            p1.prix = p.prix;
            p1.utilisateurId = currentUser.id;
            //p1.utilisateurId = 2;
            p1.dateAjout = DateTime.Now;
            p1.CategorieId = CategorieId;
            p1.statusP = produit.StatusProduit.NonVendu;
            p1.ImagePath = p.ImagePath;
            p1.ImageFile = p.ImageFile;
            using (var modbdel = new ModelBD())
            {
                if (ModelState.IsValid)
                {
                    if (Request["sub"] == "ajouter")
                    {
                        modbdel.produits.Add(p1);

                        historique h1 = new historique();
                        h1.operation = historique.Operation.Ajout;
                        h1.utilisateurId = currentUser.id;
                        h1.dateOperation = DateTime.Now;
                        h1.label = p1.title;
                        modbdel.historiques.Add(h1);

                        modbdel.SaveChanges();
                        TempData["AlertMessage"] = "Le Produit à été bien enregistré";                       
                        return RedirectToAction("MesProduits");
                    }
                    else if (Request["sub"] == "annuler")
                    {
                        return RedirectToAction("NouveuProduit");
                    }
                    else
                    {
                        return View("NouveauProduit");
                    }

                }
                ModelState.Clear();
                return View(p);
            }

        }


        [HttpGet]
        public ActionResult DeleteProduit(int id)
        {
            db.produits.Remove(db.produits.Find(id));
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.utlisateurs.Where(x => x.email == email).FirstOrDefault();

            historique h = new historique();
            h.operation = historique.Operation.Suppression;
            h.utilisateurId = currentUser.id;
            h.dateOperation = DateTime.Now;
            h.label = db.produits.Find(id).title;

            db.historiques.Add(h);

            db.SaveChanges();
            return RedirectToAction("Mesproduits");
        }

        //Modification d'un produit
        public ActionResult EditProduit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produit pdt = db.produits.Find(id);
            if (pdt == null)
            {
                return HttpNotFound();
            }
            return View(pdt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProduit(produit p, int CategorieId)
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.utlisateurs.Where(x => x.email == email).FirstOrDefault();

            var p1 = db.produits.Where(a => a.ref_Id == p.ref_Id).FirstOrDefault();
            if (p.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(p.ImageFile.FileName);
                string extension = Path.GetExtension(p.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                p.ImagePath = "~/Image/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                p.ImageFile.SaveAs(fileName);
                p1.ImageFile = p.ImageFile;
                p1.ImagePath = p.ImagePath;


                //ViewBag.dernierModification = DateTime.Now.ToString();
            }
           
            p1.title = p.title;
            p1.CategorieId = p.CategorieId;
            //v.QtéStock = produit1.QtéStock;
            p1.prix = p.prix;
            p1.description = p.description;
            //p1.utilisateurId = currentUser.id;

            historique h = new historique();
            h.operation = historique.Operation.Modification;
            //h1.utilisateurId = currentUser.id;
            h.utilisateurId = currentUser.id;
            h.dateOperation = DateTime.Now;
            h.label = p1.title;

            db.historiques.Add(h);

            db.SaveChanges();
            TempData["AlertMessage"] = "Le Produit à été modifié avec succès";
            ModelState.Clear();

            return RedirectToAction("MesProduits");
        }

        public ActionResult DetailsProduits(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produit p = db.produits.Find(id);
            if (p == null)
            {
                return HttpNotFound();
            }
            return View(p);
        }

        public ActionResult ProduitCategorie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //string email = System.Web.HttpContext.Current.User.Identity.Name;
            //var currentUser = db.utlisateurs.Where(x => x.email == email).FirstOrDefault();
            //var pProp = db.produits.Where(a => a.utilisateurId == currentUser.id).ToList();
            //var p1 = pProp.Where(x => x.CategorieId == id).ToList();

            var p1 = db.produits.Where(x => x.CategorieId == id).ToList() ;
            if (p1 == null)
            {
                return HttpNotFound();
            }
            return View(p1);
        }

        public ActionResult ProduitsVendus()
        {
            string email = System.Web.HttpContext.Current.User.Identity.Name;
            var currentUser = db.utlisateurs.Where(p => p.email == email).FirstOrDefault();
            var commande_produits = db.commandes_produits.Where(p => p.produit.utilisateurId == currentUser.id);
            //string email = System.Web.HttpContext.Current.User.Identity.Name;
            //var currentUser = db.utlisateurs.Where(x => x.email == email).FirstOrDefault();
            //var pProp = db.produits.Where(a => a.utilisateurId == currentUser.id).ToList();
            //var ProduitsVendu = pProp.Where(x => x.statusP == ModelBD.produit.StatusProduit.Vendu).ToList();

            //var ProduitsVendu = db.produits.Where(x => x.statusP == ModelBD.produit.StatusProduit.NonVendu).ToList();
            return View(commande_produits);
        }

        public ActionResult VersProVendus()
        {
            return RedirectToAction("ProduitsVendus");
        }
    }
}