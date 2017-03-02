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
    public class EstacionController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /Estacion/
        [Authorize(Roles = "Administrador")]
        public ViewResult Index()
        {
         
                return View(db.Estacion.ToList());
            
        }

        //
        // GET: /Estacion/Details/5
        [Authorize(Roles = "Administrador")]
        public ViewResult Details(int id)
        {
            Estacion estacion = db.Estacion.Single(e => e.idEstacion == id);
            return View(estacion);
        }

        //
        // GET: /Estacion/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Estacion/Create

        [HttpPost]
        public ActionResult Create(Estacion estacion)
        {
            if (ModelState.IsValid)
            {
                db.Estacion.AddObject(estacion);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(estacion);
        }
        
        //
        // GET: /Estacion/Edit/5
         [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            Estacion estacion = db.Estacion.Single(e => e.idEstacion == id);
            return View(estacion);
        }

        //
        // POST: /Estacion/Edit/5

        [HttpPost]
        public ActionResult Edit(Estacion estacion)
        {
            if (ModelState.IsValid)
            {
                db.Estacion.Attach(estacion);
                db.ObjectStateManager.ChangeObjectState(estacion, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estacion);
        }

        //
        // GET: /Estacion/Delete/5

        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            Estacion estacion = db.Estacion.Single(e => e.idEstacion == id);
            return View(estacion);
        }

        //
        // POST: /Estacion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Estacion estacion = db.Estacion.Single(e => e.idEstacion == id);
            db.Estacion.DeleteObject(estacion);
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