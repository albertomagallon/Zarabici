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
    public class RepararBicicletaController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /RepararBicicleta/
        [Authorize(Roles = "Administrador,Oficina,Personal")]
        public ViewResult Index()
        {
            var repararbicicleta = db.RepararBicicleta.Include("Bicicleta").Include("Empleado");
            return View(repararbicicleta.ToList());
        }

        //
        // GET: /RepararBicicleta/Details/5
        [Authorize(Roles = "Administrador,Oficina,Personal")]
        public ViewResult Details(int id)
        {
            RepararBicicleta repararbicicleta = db.RepararBicicleta.Single(r => r.idRepararBicicleta == id);
            return View(repararbicicleta);
        }

        //
        // GET: /RepararBicicleta/Create
        [Authorize(Roles = "Administrador,Oficina,Personal")]
        public ActionResult Create()
        {
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta");
            ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNIEmpleado");
            return View();
        } 

        //
        // POST: /RepararBicicleta/Create

        [HttpPost]
        public ActionResult Create(RepararBicicleta repararbicicleta)
        {
            if (ModelState.IsValid)
            {
                db.RepararBicicleta.AddObject(repararbicicleta);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta", repararbicicleta.idBicicleta);
            ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNIEmpleado", repararbicicleta.idEmpleado);
            return View(repararbicicleta);
        }
        
        //
        // GET: /RepararBicicleta/Edit/5
         [Authorize(Roles = "Administrador,Oficina,Personal")]
        public ActionResult Edit(int id)
        {
            RepararBicicleta repararbicicleta = db.RepararBicicleta.Single(r => r.idRepararBicicleta == id);
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta", repararbicicleta.idBicicleta);
            ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNIEmpleado", repararbicicleta.idEmpleado);
            return View(repararbicicleta);
        }

        //
        // POST: /RepararBicicleta/Edit/5

        [HttpPost]
        public ActionResult Edit(RepararBicicleta repararbicicleta)
        {
            if (ModelState.IsValid)
            {
                db.RepararBicicleta.Attach(repararbicicleta);
                db.ObjectStateManager.ChangeObjectState(repararbicicleta, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta", repararbicicleta.idBicicleta);
            ViewBag.idEmpleado = new SelectList(db.Empleado, "idEmpleado", "DNIEmpleado", repararbicicleta.idEmpleado);
            return View(repararbicicleta);
        }

        //
        // GET: /RepararBicicleta/Delete/5
         [Authorize(Roles = "Administrador,Oficina,Personal")]
        public ActionResult Delete(int id)
        {
            RepararBicicleta repararbicicleta = db.RepararBicicleta.Single(r => r.idRepararBicicleta == id);
            return View(repararbicicleta);
        }

        //
        // POST: /RepararBicicleta/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            RepararBicicleta repararbicicleta = db.RepararBicicleta.Single(r => r.idRepararBicicleta == id);
            db.RepararBicicleta.DeleteObject(repararbicicleta);
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