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
    public class OficinaController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /Oficina/
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ViewResult Index()
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;

            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();

            if (HttpContext.User.IsInRole("Cliente") || HttpContext.User.IsInRole("Oficina"))
            {
                return View(db.Oficina.Where(b => b.idOficina == socio.idOficina).ToList());
            }
            return View(db.Oficina.ToList());
        }

        //
        // GET: /Oficina/Details/5
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ViewResult Details(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            if (HttpContext.User.IsInRole("Cliente")||HttpContext.User.IsInRole("Oficina"))
            {
                int miOficina = db.Oficina.Where(s => s.idOficina == id && s.idOficina==socio.idOficina).Count();
                if (miOficina == 0)
                {
                    return View("Error");
                }
            }
         
            Oficina oficina = db.Oficina.Single(o => o.idOficina == id);
            return View(oficina);
        }

        //
        // GET: /Oficina/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Oficina/Create

        [HttpPost]
        public ActionResult Create(Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                db.Oficina.AddObject(oficina);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(oficina);
        }
        
        //
        // GET: /Oficina/Edit/5
         [Authorize(Roles = "Administrador,Oficina")]
        public ActionResult Edit(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            if (HttpContext.User.IsInRole("Oficina"))
            {
                int miOficina = db.Oficina.Where(s => s.idOficina == id && s.idOficina == socio.idOficina).Count();
                if (miOficina == 0)
                {
                    return View("Error");
                }
            }

            Oficina oficina = db.Oficina.Single(o => o.idOficina == id);
            return View(oficina);
        }

        //
        // POST: /Oficina/Edit/5

        [HttpPost]
        public ActionResult Edit(Oficina oficina)
        {
            if (ModelState.IsValid)
            {
                db.Oficina.Attach(oficina);
                db.ObjectStateManager.ChangeObjectState(oficina, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oficina);
        }

        //
        // GET: /Oficina/Delete/5
         [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int id)
        {
            Oficina oficina = db.Oficina.Single(o => o.idOficina == id);
            return View(oficina);
        }

        //
        // POST: /Oficina/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Oficina oficina = db.Oficina.Single(o => o.idOficina == id);
            db.Oficina.DeleteObject(oficina);
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