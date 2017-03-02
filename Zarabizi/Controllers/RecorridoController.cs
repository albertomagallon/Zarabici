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
    public class RecorridoController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /Recorrido/
        [Authorize(Roles = "Administrador, Oficina, Cliente")]
        public ViewResult Index()
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;

            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();

            if (HttpContext.User.IsInRole("Cliente"))
            {
               return View(db.Recorrido.Where(r => r.idSocio == socio.idSocio).ToList());
            }

            if (HttpContext.User.IsInRole("Oficina"))
            {
                //Seleccinamos todos los socios de la oficina
                Socio socios = db.Socio.Where(s => s.idOficina == socio.idOficina).FirstOrDefault();
                return View(db.Recorrido.Where(r => r.idSocio == socios.idSocio).ToList());
            }
            var recorrido = db.Recorrido.Include("Bicicleta").Include("Estacion").Include("Estacion1").Include("Socio");
            return View(recorrido.ToList());
        }

        //
        // GET: /Recorrido/Details/5
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ViewResult Details(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            if (HttpContext.User.IsInRole("Cliente"))
            {
                int miRecorrido = db.Recorrido.Where(r => r.idRecorrido == id && r.idSocio == socio.idSocio).Count();
                if (miRecorrido == 0)
                {
                    return View("Error");
                }
            }
            if (HttpContext.User.IsInRole("Oficina"))
            {
                //Seleccinamos todos los socios de la oficina
                Socio socios = db.Socio.Where(s => s.idOficina == socio.idOficina).FirstOrDefault();
                int miRecorrido = db.Recorrido.Where(r => r.idRecorrido == id && r.idSocio == socios.idSocio).Count();
                if (miRecorrido == 0)
                {
                    return View("Error");
                }
            }

            Recorrido recorrido = db.Recorrido.Single(r => r.idRecorrido == id);
            return View(recorrido);
        }

        //
        // GET: /Recorrido/Create
        [Authorize(Roles = "Administrador,Oficina")]
        public ActionResult Create()
        {
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta");
            ViewBag.idEstacionFinal = new SelectList(db.Estacion, "idEstacion", "nombreEstacion");
            ViewBag.idEstacionInicio = new SelectList(db.Estacion, "idEstacion", "nombreEstacion");
            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio");
            return View();
        } 

        //
        // POST: /Recorrido/Create

        [HttpPost]
        public ActionResult Create(Recorrido recorrido)
        {
            if (ModelState.IsValid)
            {
                db.Recorrido.AddObject(recorrido);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta", recorrido.idBicicleta);
            ViewBag.idEstacionFinal = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", recorrido.idEstacionFinal);
            ViewBag.idEstacionInicio = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", recorrido.idEstacionInicio);
            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio", recorrido.idSocio);
            return View(recorrido);
        }
        
        //
        // GET: /Recorrido/Edit/5
         [Authorize(Roles = "Administrador,Oficina")]
        public ActionResult Edit(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            if (HttpContext.User.IsInRole("Oficina"))
            {
                //Seleccinamos todos los socios de la oficina
                Socio socios = db.Socio.Where(s => s.idOficina == socio.idOficina).FirstOrDefault();
                int miRecorrido = db.Recorrido.Where(r => r.idRecorrido == id && r.idSocio == socios.idSocio).Count();
                if (miRecorrido == 0)
                {
                    return View("Error");
                }
            }
            Recorrido recorrido = db.Recorrido.Single(r => r.idRecorrido == id);
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta", recorrido.idBicicleta);
            ViewBag.idEstacionFinal = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", recorrido.idEstacionFinal);
            ViewBag.idEstacionInicio = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", recorrido.idEstacionInicio);
            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio", recorrido.idSocio);
            return View(recorrido);
        }

        //
        // POST: /Recorrido/Edit/5

        [HttpPost]
        public ActionResult Edit(Recorrido recorrido)
        {
            if (ModelState.IsValid)
            {
                db.Recorrido.Attach(recorrido);
                db.ObjectStateManager.ChangeObjectState(recorrido, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idBicicleta = new SelectList(db.Bicicleta, "idBicicleta", "idBicicleta", recorrido.idBicicleta);
            ViewBag.idEstacionFinal = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", recorrido.idEstacionFinal);
            ViewBag.idEstacionInicio = new SelectList(db.Estacion, "idEstacion", "nombreEstacion", recorrido.idEstacionInicio);
            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio", recorrido.idSocio);
            return View(recorrido);
        }

        //
        // GET: /Recorrido/Delete/5
         [Authorize(Roles = "Administrador,Oficina")]
        public ActionResult Delete(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            if (HttpContext.User.IsInRole("Oficina"))
            {
                //Seleccinamos todos los socios de la oficina
                Socio socios = db.Socio.Where(s => s.idOficina == socio.idOficina).FirstOrDefault();
                int miRecorrido = db.Recorrido.Where(r => r.idRecorrido == id && r.idSocio == socios.idSocio).Count();
                if (miRecorrido == 0)
                {
                    return View("Error");
                }
            }
            Recorrido recorrido = db.Recorrido.Single(r => r.idRecorrido == id);
            return View(recorrido);
        }

        //
        // POST: /Recorrido/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Recorrido recorrido = db.Recorrido.Single(r => r.idRecorrido == id);
            db.Recorrido.DeleteObject(recorrido);
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