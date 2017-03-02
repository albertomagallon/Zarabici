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
    public class SocioController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /Socio/
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ViewResult Index()
        {            
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            Socio socios = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();                
        if (HttpContext.User.IsInRole("Cliente"))
        {
            return View(db.Socio.Where(s => s.idSocio == socios.idSocio).ToList());
        }
        if (HttpContext.User.IsInRole("Oficina"))
        {
            Oficina oficina = db.Oficina.Where(o => o.idOficina == socios.idOficina).FirstOrDefault();
            return View(db.Socio.Where(s => s.idOficina == oficina.idOficina).ToList());
        }

            var socio = db.Socio.Include("aspnet_Users").Include("Oficina");
            return View(socio.ToList());
        }

        //
        // GET: /Socio/Details/5
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ViewResult Details(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            Socio socios = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            if (HttpContext.User.IsInRole("Cliente"))
            {
                if (socios.idSocio != id)
                {
                    return View("Error");
                }               
            }
            if (HttpContext.User.IsInRole("Oficina"))
            {                
                int haySocios = db.Socio.Where(s => s.idOficina == socios.idOficina && s.idSocio==id).Count();
                if (haySocios == 0)
                {
                    return View("Error");
                }
                
            }


            Socio socio = db.Socio.Single(s => s.idSocio == id);
            return View(socio);
        }

        //
        // GET: /Socio/Create
        public ActionResult Create()
        {
            //Comprobar si hay usuario logueado
            Guid keyUsuario = (Guid)Membership.GetUser().ProviderUserKey;

            //Si no hay usuario logueado redireccionamos a la página de inicio         
            if (keyUsuario == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                //Hay usuario logueado, comprobamos si el usuario existe en la tabla Socio     
                ZarabiziEntities db = new ZarabiziEntities();

                int count = db.Socio.Where(s => s.idUsuario == keyUsuario).Count();

                //Si no existe dejamos crear el registro en la tabla socio.
                if (count == 0)
                {
                    ViewBag.idUsuario = new SelectList(db.aspnet_Users, "UserId", "UserName");
                    ViewBag.idOficina = new SelectList(db.Oficina, "idOficina", "nombreOficina");

                    //Si existe no le podemos dejar crear un nuevo registro asi que redireccionamos a la página de inicio
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        } 

        //
        // POST: /Socio/Create

        [HttpPost]
        public ActionResult Create(Socio socio)
        {
            //Recuperar GUID del usuario actual
            Guid keyUsuario = (Guid)Membership.GetUser().ProviderUserKey;

            //Buscarlo en aspnet_users
            socio.aspnet_Users = db.aspnet_Users.Single(s => s.UserId == keyUsuario);
            
            if ((ModelState.IsValid) || (!ModelState.IsValid & !ModelState.IsValidField("aspnet_Users") & (ModelState.Values.Where(x => x.Errors.Count() > 0).Count() ==1 & socio.aspnet_Users != null)))
            {            
                db.Socio.AddObject(socio);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idUsuario = new SelectList(db.aspnet_Users, "UserId", "UserName", socio.idUsuario);
            ViewBag.idOficina = new SelectList(db.Oficina, "idOficina", "nombreOficina", socio.idOficina);
            return View(socio);
        }
        
        //
        // GET: /Socio/Edit/5
         [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ActionResult Edit(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            Socio socios = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            if (HttpContext.User.IsInRole("Cliente"))
            {
                if (socios.idSocio != id)
                {
                    return View("Error");
                }
            }
            if (HttpContext.User.IsInRole("Oficina"))
            {
                int haySocios = db.Socio.Where(s => s.idOficina == socios.idOficina && s.idSocio == id).Count();
                if (haySocios == 0)
                {
                    return View("Error");
                }

            }
            Socio socio = db.Socio.Single(s => s.idSocio == id);
            ViewBag.idUsuario = new SelectList(db.aspnet_Users, "UserId", "UserName", socio.idUsuario);
            ViewBag.idOficina = new SelectList(db.Oficina, "idOficina", "nombreOficina", socio.idOficina);
            return View(socio);
        }

        //
        // POST: /Socio/Edit/5

        [HttpPost]
        public ActionResult Edit(Socio socio)
        {
            if (ModelState.IsValid)
            {
                db.Socio.Attach(socio);
                db.ObjectStateManager.ChangeObjectState(socio, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idUsuario = new SelectList(db.aspnet_Users, "UserId", "UserName", socio.idUsuario);
            ViewBag.idOficina = new SelectList(db.Oficina, "idOficina", "nombreOficina", socio.idOficina);
            return View(socio);
        }

        //
        // GET: /Socio/Delete/5
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ActionResult Delete(int id)
        {
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            Socio socios = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
            if (HttpContext.User.IsInRole("Cliente"))
            {
                if (socios.idSocio != id)
                {
                    return View("Error");
                }
            }
            if (HttpContext.User.IsInRole("Oficina"))
            {
                int haySocios = db.Socio.Where(s => s.idOficina == socios.idOficina && s.idSocio == id).Count();
                if (haySocios == 0)
                {
                    return View("Error");
                }

            }
            Socio socio = db.Socio.Single(s => s.idSocio == id);
            return View(socio);
        }

        //
        // POST: /Socio/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Socio socio = db.Socio.Single(s => s.idSocio == id);
            db.Socio.DeleteObject(socio);
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