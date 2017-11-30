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
    public class FacturasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Facturas
        public ActionResult Index()
        {
            return View(db.FacturasModels.ToList());
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            if (TempData.Count > 0 && TempData["recargar"].ToString() == "no")
            {
                TempData["recargar"] = "si";
                return View(TempData["facturaViewModel"]);
            }
            FacturaViewModel facturaViewModel = new FacturaViewModel();
            facturaViewModel.factura = new FacturasModels();
            facturaViewModel.factura.fecha = DateTime.Now;
            facturaViewModel.detalle = new FacturaDetallesModels();
            facturaViewModel.articulos = db.ArticulosModels.ToList().Select(i => new SelectListItem() { Value = i.id.ToString(), Text = i.descripcion }).ToList();
            return View(facturaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacturaViewModel facturaViewModel)
        {
            if (ModelState.IsValid)
            {
                db.FacturasModels.Add(facturaViewModel.factura);
                db.SaveChanges();
            }
            facturaViewModel.articulos = db.ArticulosModels.ToList().Select(i => new SelectListItem() { Value = i.id.ToString(), Text = i.descripcion }).ToList();
            return View(facturaViewModel);
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
