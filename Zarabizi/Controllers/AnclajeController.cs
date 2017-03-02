using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zarabizi.Models;
using System.Web.Security;

namespace Zarabizi.Controllers
{
    public class AnclajeController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /Anclaje/
        [Authorize(Roles = "Administrador")]
        public ViewResult Index()
        {
           

            var anclaje = db.Anclaje.Include("Bicicleta").Include("Estacion");
            return View(anclaje.ToList());
        }

        //
        // GET: /Anclaje/Details/5
        [Authorize(Roles = "Administrador")]
        public ViewResult Details(int id)
        {
            Anclaje anclaje = db.Anclaje.Single(a => a.idAnclaje == id);
            return View(anclaje);
        }

        //
        // GET: /Anclaje/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "nombreEstacion");
            return View();
        } 

        //
        // POST: /Anclaje/Create

        [HttpPost]
        public ActionResult Create(Anclaje anclaje)
        {
            if (ModelState.IsValid)
            {
                db.Anclaje.AddObject(anclaje);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta", anclaje.idBicicleta);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", anclaje.idEstacion);
            return View(anclaje);
        }
        
        //
        // GET: /Anclaje/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            Anclaje anclaje = db.Anclaje.Single(a => a.idAnclaje == id);
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta", anclaje.idBicicleta);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", anclaje.idEstacion);
            return View(anclaje);
        }

        //
        // POST: /Anclaje/Edit/5

        [HttpPost]
        public ActionResult Edit(Anclaje anclaje)
        {
            if (ModelState.IsValid)
            {
                db.Anclaje.Attach(anclaje);
                db.ObjectStateManager.ChangeObjectState(anclaje, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta", anclaje.idBicicleta);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", anclaje.idEstacion);
            return View(anclaje);
        }

        //
        // GET: /Anclaje/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            Anclaje anclaje = db.Anclaje.Single(a => a.idAnclaje == id);
            return View(anclaje);
        }

        //
        // POST: /Anclaje/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Anclaje anclaje = db.Anclaje.Single(a => a.idAnclaje == id);
            db.Anclaje.DeleteObject(anclaje);
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