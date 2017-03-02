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
    public class RepararEstacionController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /RepararEstacion/
         [Authorize(Roles = "Administrador,Oficina,Personal")]
        public ViewResult Index()
        {
            var repararestacion = db.RepararEstacion.Include("Empleado").Include("Estacion");
            return View(repararestacion.ToList());
        }

        //
        // GET: /RepararEstacion/Details/5
         [Authorize(Roles = "Administrador,Oficina,Personal")]
        public ViewResult Details(int id)
        {
            RepararEstacion repararestacion = db.RepararEstacion.Single(r => r.idRepararEstacion == id);
            return View(repararestacion);
        }

        //
        // GET: /RepararEstacion/Create
         [Authorize(Roles = "Administrador,Oficina,Personal")]
        public ActionResult Create()
        {
            ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNIEmpleado");
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "nombreEstacion");
            return View();
        } 

        //
        // POST: /RepararEstacion/Create

        [HttpPost]
        public ActionResult Create(RepararEstacion repararestacion)
        {
            if (ModelState.IsValid)
            {
                db.RepararEstacion.AddObject(repararestacion);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNIEmpleado", repararestacion.idEmpleado);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", repararestacion.idEstacion);
            return View(repararestacion);
        }
        
        //
        // GET: /RepararEstacion/Edit/5
         [Authorize(Roles = "Administrador,Oficina,Personal")]
        public ActionResult Edit(int id)
        {
            RepararEstacion repararestacion = db.RepararEstacion.Single(r => r.idRepararEstacion == id);
            ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNIEmpleado", repararestacion.idEmpleado);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", repararestacion.idEstacion);
            return View(repararestacion);
        }

        //
        // POST: /RepararEstacion/Edit/5

        [HttpPost]
        public ActionResult Edit(RepararEstacion repararestacion)
        {
            if (ModelState.IsValid)
            {
                db.RepararEstacion.Attach(repararestacion);
                db.ObjectStateManager.ChangeObjectState(repararestacion, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNIEmpleado", repararestacion.idEmpleado);
            ViewBag.idEstacion = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", repararestacion.idEstacion);
            return View(repararestacion);
        }

        //
        // GET: /RepararEstacion/Delete/5
         [Authorize(Roles = "Administrador,Oficina,Personal")]
        public ActionResult Delete(int id)
        {
            RepararEstacion repararestacion = db.RepararEstacion.Single(r => r.idRepararEstacion == id);
            return View(repararestacion);
        }

        //
        // POST: /RepararEstacion/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            RepararEstacion repararestacion = db.RepararEstacion.Single(r => r.idRepararEstacion == id);
            db.RepararEstacion.DeleteObject(repararestacion);
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