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
    public class BicicletaController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /Bicicleta/
        [Authorize(Roles = "Administrador")]
        public ViewResult Index()
        {
            return View(db.Bicicleta.ToList());
        }

        //
        // GET: /Bicicleta/Details/5
        [Authorize(Roles = "Administrador")]
        public ViewResult Details(int id)
        {
            Bicicleta bicicleta = db.Bicicleta.Single(b => b.idBicicleta == id);
            return View(bicicleta);
        }

        //
        // GET: /Bicicleta/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Bicicleta/Create

        [HttpPost]
        public ActionResult Create(Bicicleta bicicleta)
        {
            if (ModelState.IsValid)
            {
                db.Bicicleta.AddObject(bicicleta);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(bicicleta);
        }
        
        //
        // GET: /Bicicleta/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            Bicicleta bicicleta = db.Bicicleta.Single(b => b.idBicicleta == id);
            return View(bicicleta);
        }

        //
        // POST: /Bicicleta/Edit/5

        [HttpPost]
        public ActionResult Edit(Bicicleta bicicleta)
        {
            if (ModelState.IsValid)
            {
                db.Bicicleta.Attach(bicicleta);
                db.ObjectStateManager.ChangeObjectState(bicicleta, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bicicleta);
        }

        //
        // GET: /Bicicleta/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            Bicicleta bicicleta = db.Bicicleta.Single(b => b.idBicicleta == id);
            return View(bicicleta);
        }

        //
        // POST: /Bicicleta/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Bicicleta bicicleta = db.Bicicleta.Single(b => b.idBicicleta == id);
            db.Bicicleta.DeleteObject(bicicleta);
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