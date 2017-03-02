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
    public class EmpleadoController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /Empleado/
        [Authorize(Roles = "Administrador")]
        public ViewResult Index()
        {
            return View(db.Empleado.ToList());
        }

        //
        // GET: /Empleado/Details/5
        [Authorize(Roles = "Administrador")]
        public ViewResult Details(int id)
        {
            Empleado empleado = db.Empleado.Single(e => e.idEmpleado == id);
            return View(empleado);
        }

        //
        // GET: /Empleado/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Empleado/Create

        [HttpPost]
        public ActionResult Create(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleado.AddObject(empleado);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(empleado);
        }
        
        //
        // GET: /Empleado/Edit/5
         [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            Empleado empleado = db.Empleado.Single(e => e.idEmpleado == id);
            return View(empleado);
        }

        //
        // POST: /Empleado/Edit/5

        [HttpPost]
        public ActionResult Edit(Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                db.Empleado.Attach(empleado);
                db.ObjectStateManager.ChangeObjectState(empleado, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleado);
        }

        //
        // GET: /Empleado/Delete/5
         [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            Empleado empleado = db.Empleado.Single(e => e.idEmpleado == id);
            return View(empleado);
        }

        //
        // POST: /Empleado/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Empleado empleado = db.Empleado.Single(e => e.idEmpleado == id);
            db.Empleado.DeleteObject(empleado);
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