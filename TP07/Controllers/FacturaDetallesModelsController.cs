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
    public class FacturaDetallesModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FacturaDetallesModels
        public ActionResult Index()
        {
            var facturaDetallesModels = db.FacturaDetallesModels.Include(f => f.articulo).Include(f => f.factura);
            return View(facturaDetallesModels.ToList());
        }

       

        // GET: FacturaDetallesModels/Create
        public ActionResult Create()
        {
            //Mostrar los detalles de las facturas
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacturaViewModel facturaViewModel)
            {
            if (ModelState.IsValid)
            {
                facturaViewModel.detalle.facturaid = facturaViewModel.factura.id;
                db.FacturaDetallesModels.Add(facturaViewModel.detalle);
                db.SaveChanges();
            }
            facturaViewModel.articulos = db.ArticulosModels.ToList().Select(i => new SelectListItem() { Value = i.id.ToString(), Text = i.descripcion }).ToList();

            TempData.Add("recargar", "no");
            TempData.Add("facturaViewModel", facturaViewModel);

            return RedirectToAction("Create", "Facturas");
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