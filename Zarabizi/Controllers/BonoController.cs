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
    public class BonoController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /Bono/
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ViewResult Index()
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            
            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();            

            if (HttpContext.User.IsInRole("Administrador"))
            {
                var bono = db.Bono.Include("Socio").Include("Tarifa");
                return View(bono.ToList());
            }

            if (HttpContext.User.IsInRole("Cliente"))
            {
                return View(db.Bono.Where(b => b.idSocio == socio.idSocio).ToList());
            }

            //Devolver únicame los bonos de la tarifa del rol oficina actual.
            Tarifa tarifa = db.Tarifa.Where(t => t.idOficina == socio.idOficina).FirstOrDefault();
            return View(db.Bono.Where(b => b.idTarifa == tarifa.idTarifa).ToList());
        }

        //
        // GET: /Bono/Details/5
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ViewResult Details(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            //Ver detalles únicame de los bonos de la tarifa del rol oficina actual.
            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            int hayBono = 0;
            if (HttpContext.User.IsInRole("Cliente"))
            {
                //Recuperamos el idSocio del bono actual
                Bono bonos = db.Bono.Where(b => b.idBono == id).FirstOrDefault();
                //Si coinciden los ids es su bono
                if (bonos.idSocio == socio.idSocio)
                {
                    hayBono = 1;
                }
                else
                {
                    hayBono = 0;
                }
                           
                if (hayBono == 0)
                {
                    return View("Error");
                }
            }

            if (HttpContext.User.IsInRole("Oficina"))
            {               
                Tarifa tarifa = db.Tarifa.Where(t => t.idOficina == socio.idOficina).FirstOrDefault();
                hayBono = db.Bono.Where(b => b.idTarifa == tarifa.idTarifa && b.idBono == id).Count();

                if (hayBono == 0)
                {
                    return View("Error");  
                }
            }

                Bono bono = db.Bono.Single(b => b.idBono == id);
                return View(bono);
        }

        //
        // GET: /Bono/Create
        [Authorize(Roles = "Administrador,Oficina")]
        public ActionResult Create()
        {
            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio");
            ViewBag.idTarifa = new SelectList(db.Tarifa, "idTarifa", "nombreTarifa");
            return View();
        } 

        //
        // POST: /Bono/Create

        [HttpPost]
        public ActionResult Create(Bono bono)
        {
            if (ModelState.IsValid)
            {
                db.Bono.AddObject(bono);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio", bono.idSocio);
            ViewBag.idTarifa = new SelectList(db.Tarifa, "idTarifa", "nombreTarifa", bono.idTarifa);
            return View(bono);
        }
        
        //
        // GET: /Bono/Edit/5
         [Authorize(Roles = "Administrador,Oficina")]
        public ActionResult Edit(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            //Editar únicame los bonos de la tarifa del rol oficina actual.
            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            Tarifa tarifa = db.Tarifa.Where(t => t.idOficina == socio.idOficina).FirstOrDefault();
            int hayBono = db.Bono.Where(b => b.idTarifa == tarifa.idTarifa && b.idBono == id).Count();

            if (hayBono == 0)
            {
                return View("Error");
            }
            else
            {
                Bono bono = db.Bono.Single(b => b.idBono == id);
                ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio", bono.idSocio);
                ViewBag.idTarifa = new SelectList(db.Tarifa, "idTarifa", "nombreTarifa", bono.idTarifa);
                return View(bono);
            }
        }

        //
        // POST: /Bono/Edit/5

        [HttpPost]
        public ActionResult Edit(Bono bono)
        {
            if (ModelState.IsValid)
            {
                db.Bono.Attach(bono);
                db.ObjectStateManager.ChangeObjectState(bono, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio", bono.idSocio);
            ViewBag.idTarifa = new SelectList(db.Tarifa, "idTarifa", "nombreTarifa", bono.idTarifa);
            return View(bono);
        }

        //
        // GET: /Bono/Delete/5
         [Authorize(Roles = "Administrador,Oficina")]
        public ActionResult Delete(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            //Ver detalles únicame de los bonos de la tarifa del rol oficina actual.
            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            Tarifa tarifa = db.Tarifa.Where(t => t.idOficina == socio.idOficina).FirstOrDefault();
            int hayBono = db.Bono.Where(b => b.idTarifa == tarifa.idTarifa && b.idBono == id).Count();

            if (hayBono == 0)
            {
                return View("Error");
            }
            else
            {
                Bono bono = db.Bono.Single(b => b.idBono == id);
                return View(bono);
            }
        }

        //
        // POST: /Bono/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Bono bono = db.Bono.Single(b => b.idBono == id);
            db.Bono.DeleteObject(bono);
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