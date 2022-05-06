using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static projet1.ModelBD;
using PagedList.Mvc;
using PagedList;
namespace projet1.Controllers
{
    [Authorize(Roles = "admin")]

    public class ProduitController : Controller
    {
        static bool status = false;
        ModelBD bd = new ModelBD();
        // GET: Produit
        [HttpGet]
        public ActionResult Index(String surchtext,int? i)
        {
           
           // var listP = bd.produits.OrderBy(a => a.ref_Id).ToList();
            return View(bd.produits.Where(x => x.title.Contains(surchtext) || surchtext == null).ToList().ToPagedList(i ?? 1,2));
       
        }
        public ActionResult Add(produit produitA)
        {
            if (produitA != null)
            {
              return View(produitA);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Add(produit produit1, int CategorieId)
        {
            status = false;
            produit p = new produit();
            string fileName = Path.GetFileNameWithoutExtension(produit1.ImageFile.FileName);
            string extension = Path.GetExtension(produit1.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            produit1.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            produit1.ImageFile.SaveAs(fileName);
            p.ref_Id = produit1.ref_Id;
            p.CategorieId = CategorieId;
            p.ImagePath = produit1.ImagePath;
            p.ImageFile = produit1.ImageFile;
            p.prix = produit1.prix;
            p.title = produit1.title;
            p.QtéStock = produit1.QtéStock;
            p.description = produit1.description;
            p.utilisateurId = produit1.utilisateurId;
            p.dateAjout = DateTime.Now;
            ModelBD db = new ModelBD();
            
                if (produit1.ref_Id==0)
                {
                db.produits.Add(p);
                db.SaveChanges();
                }
                else
                {
                var v = db.produits.Where(a => a.ref_Id == produit1.ref_Id).FirstOrDefault();
                v.title = produit1.title;
                v.CategorieId = CategorieId;
                v.QtéStock = produit1.QtéStock;
                v.ImageFile = produit1.ImageFile;
                v.ImagePath = produit1.ImagePath;
                v.prix = produit1.prix;
                v.description = produit1.description;
                v.utilisateurId = produit1.utilisateurId;
                    db.SaveChanges();
                }
                
                status = true;
            
            ModelState.Clear();
           
            var res = db.produits.ToList().ToPagedList(1, 2);
            return View("Index", res);


        }
        public ActionResult Edit(int? id)
        {
            ModelBD db = new ModelBD();
            var data = db.produits.Where(x => x.ref_Id == id).SingleOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit1(produit produit1)
        {
            ModelBD db = new ModelBD();
            status = false;
            var v = db.produits.Where(a => a.ref_Id == produit1.ref_Id).FirstOrDefault();
            if (produit1.ImageFile!=null)
            {
            string fileName = Path.GetFileNameWithoutExtension(produit1.ImageFile.FileName);
            string extension = Path.GetExtension(produit1.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            produit1.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            produit1.ImageFile.SaveAs(fileName);
            v.ImageFile = produit1.ImageFile;
            v.ImagePath = produit1.ImagePath;

            }
           
            
            v.title = produit1.title;
            v.CategorieId = produit1.CategorieId;
            v.QtéStock = produit1.QtéStock;
           
            v.prix = produit1.prix;
            v.description = produit1.description;
            v.utilisateurId = produit1.utilisateurId;
            db.SaveChanges();
            status = true;

            ModelState.Clear();

            var res = db.produits.ToList().ToPagedList(1, 2);
            return View("Index", res);
        }
        public ActionResult Delete(int? id)
        {
            ModelBD db = new ModelBD();
           
                var obj = db.produits.Where(x => x.ref_Id == id).First();
                db.produits.Remove(obj);
                db.SaveChanges();


          
            var res = db.produits.ToList().ToPagedList(1, 2);
            return View("Index", res);
        }

    }
}