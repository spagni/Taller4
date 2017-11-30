using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TP07.Models;

namespace TP07.Controllers
{
    public class ArticulosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articulos
        public ActionResult Index()
        {
            return View(db.ArticulosModels.ToList());
        }

        // GET: Articulos/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,codigo,descripcion,precio")] ArticulosModels articulosModels)
        {
            if (ModelState.IsValid)
            {
                db.ArticulosModels.Add(articulosModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articulosModels);
        }

        // GET: Articulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticulosModels articulosModels = db.ArticulosModels.Find(id);
            if (articulosModels == null)
            {
                return HttpNotFound();
            }
            return View(articulosModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,codigo,descripcion,precio")] ArticulosModels articulosModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulosModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articulosModels);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
