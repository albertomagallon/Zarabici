using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zarabizi.Models;

namespace Zarabizi.Controllers
{ 
    public class TarifaController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /Tarifa/
        [Authorize(Roles = "Administrador,Oficina")]
        public ViewResult Index()
        {
            var tarifa = db.Tarifa.Include("Oficina");
            return View(tarifa.ToList());
        }

        //
        // GET: /Tarifa/Details/5
        [Authorize(Roles = "Administrador,Oficina")]
        public ViewResult Details(int id)
        {
            Tarifa tarifa = db.Tarifa.Single(t => t.idTarifa == id);
            return View(tarifa);
        }

        //
        // GET: /Tarifa/Create
        [Authorize(Roles = "Administrador,Oficina")]
        public ActionResult Create()
        {
            ViewBag.idOficina = new SelectList(db.Oficina, "idOficina", "nombreOficina");
            return View();
        } 

        //
        // POST: /Tarifa/Create

        [HttpPost]
        public ActionResult Create(Tarifa tarifa)
        {
            if (ModelState.IsValid)
            {
                db.Tarifa.AddObject(tarifa);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idOficina = new SelectList(db.Oficina, "idOficina", "nombreOficina", tarifa.idOficina);
            return View(tarifa);
        }
        
        //
        // GET: /Tarifa/Edit/5
         [Authorize(Roles = "Administrador,Oficina")]
        public ActionResult Edit(int id)
        {
            Tarifa tarifa = db.Tarifa.Single(t => t.idTarifa == id);
            ViewBag.idOficina = new SelectList(db.Oficina, "idOficina", "nombreOficina", tarifa.idOficina);
            return View(tarifa);
        }

        //
        // POST: /Tarifa/Edit/5

        [HttpPost]
        public ActionResult Edit(Tarifa tarifa)
        {
            if (ModelState.IsValid)
            {
                db.Tarifa.Attach(tarifa);
                db.ObjectStateManager.ChangeObjectState(tarifa, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idOficina = new SelectList(db.Oficina, "idOficina", "nombreOficina", tarifa.idOficina);
            return View(tarifa);
        }

        //
        // GET: /Tarifa/Delete/5
         [Authorize(Roles = "Administrador,Oficina")]
        public ActionResult Delete(int id)
        {
            Tarifa tarifa = db.Tarifa.Single(t => t.idTarifa == id);
            return View(tarifa);
        }

        //
        // POST: /Tarifa/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Tarifa tarifa = db.Tarifa.Single(t => t.idTarifa == id);
            db.Tarifa.DeleteObject(tarifa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}