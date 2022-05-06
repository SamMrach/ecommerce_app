using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static projet1.ModelBD;
using System.Linq.Dynamic;

namespace projet1.Controllers
{
    [Authorize(Roles = "admin")]

    public class Favori1Controller : Controller
    {
        // GET: Favori1
        public ActionResult Index(int page = 1, string sort = "nom", string sortdir = "asc", string search = "")
        {
            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetClients(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }
        public ActionResult Index1(int page = 1, string sort = "nom", string sortdir = "asc", string search = "")
        {
            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetClients1(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }
        public List<client> GetClients(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            using (ModelBD dc = new ModelBD())
            {
                var v = (from a in dc.clients
                         where
                                 (a.nom.Contains(search) ||
                                 a.prenom.Contains(search) ||
                                 a.email.Contains(search) ||
                                 a.tel.Contains(search) ||
                                 a.password.Contains(search)) && a.status!=0
                         select a
                                );
                totalRecord = v.Count();
                v = v.OrderBy(sort + " " + sortdir);
                if (pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);
                }
                return v.ToList();
            }
        }
        public List<client> GetClients1(string search, string sort, string sortdir, int skip, int pageSize, out int totalRecord)
        {
            using (ModelBD dc = new ModelBD())
            {
                var v = (from a in dc.clients
                         where
                                 (a.nom.Contains(search) ||
                                 a.prenom.Contains(search) ||
                                 a.email.Contains(search) ||
                                 a.tel.Contains(search) ||
                                 a.password.Contains(search)) && a.status==0
                         select a
                                );
                totalRecord = v.Count();
                v = v.OrderBy(sort + " " + sortdir);
                if (pageSize > 0)
                {
                    v = v.Skip(skip).Take(pageSize);
                }
                return v.ToList();
            }
        }
        public ActionResult AddFavori(string[] ids)
        {
            int[] id = null;
            if (ids != null)
            {
                id = new int[ids.Length];
                int j = 0;
                foreach (string i in ids)
                {
                    int.TryParse(i, out id[j++]);
                }
            }

            if (id != null && id.Length > 0)
            {

                using (ModelBD db = new ModelBD())
                {
                    var allSelected = db.clients.Where(a => id.Contains(a.id)).ToList();
                    foreach (var i in allSelected)
                    {
                        i.status = ModelBD.client.Status.favorie;
                    }
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index1");
        }
        public  ActionResult listeFavoris(int page = 1, string sort = "nom", string sortdir = "asc", string search = "")
        {
            int pageSize = 10;
            int totalRecord = 0;
            if (page < 1) page = 1;
            int skip = (page * pageSize) - pageSize;
            var data = GetClients1(search, sort, sortdir, skip, pageSize, out totalRecord);
            ViewBag.TotalRows = totalRecord;
            ViewBag.search = search;
            return View(data);
        }
    }
}