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
    public class IncidenciaController : Controller
    {
        private ZarabiziEntities db = new ZarabiziEntities();

        //
        // GET: /Incidencia/
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ViewResult Index()
        {

            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            
            if (HttpContext.User.IsInRole("Cliente"))
            {
                //Devolver únicame las Incidencias del socio actual.
                Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();                
                return View(db.Incidencia.Where(i => i.idSocio == socio.idSocio).ToList());
            }

            if (HttpContext.User.IsInRole("Oficina"))
            {
                //Devolver únicame las Incidencias de la oficina actual.
                Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
                //Recuperar el idoficina del socio actual
                Oficina oficina = db.Oficina.Where(o => o.idOficina == socio.idOficina).FirstOrDefault();
                //Recuperar los idsocios de la oficina actual
                Socio socios = db.Socio.Where(o => o.idOficina == oficina.idOficina).FirstOrDefault();                
                //Devolver todas las incidencias de los idsocios de la oficina
                return View(db.Incidencia.Where(i => i.idSocio == socios.idSocio).ToList());
            }


            var incidencia = db.Incidencia.Include("Socio");
            return View(incidencia.ToList());
        }
      
        //
        // GET: /Incidencia/Details/5
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ViewResult Details(int id)
        {
            int hayIncidencia = 0;
            
            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            if (HttpContext.User.IsInRole("Cliente"))
            {
                //Ver detalles únicame de la incidencia del cliente.
                Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
                //Ver el idSocio puede ver la incidencia actual
                Incidencia incidenciass = db.Incidencia.Where(i => i.idIncidencia == id).FirstOrDefault();
                if (incidenciass.idSocio != socio.idSocio)
                {
                     hayIncidencia = 0;
                }else{

                     hayIncidencia=1;
                }
                //int hayIncidencia = db.Socio.Where(t => t.idSocio == incidenciass.idSocio).Count();               

                if (hayIncidencia == 0)
                {
                    return View("Error");
                }
            }

            if (HttpContext.User.IsInRole("Oficina"))
            {
                //Ver detalles únicame de la incidencia del cliente.
                Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
                //Recuperar el idoficina del socio actual
                Oficina oficina = db.Oficina.Where(o => o.idOficina == socio.idOficina).FirstOrDefault();
                //Recuperar los idsocios de la oficina actual
                Socio socios = db.Socio.Where(o => o.idOficina == oficina.idOficina).FirstOrDefault();
                //Ver si los idSocios pueden ver la incidencia actual
                Incidencia incidencias = db.Incidencia.Where(i => i.idIncidencia == id).FirstOrDefault();
                
                hayIncidencia = db.Socio.Where(t => t.idSocio == incidencias.idSocio).Count();               

                if (hayIncidencia == 0)
                {
                    return View("Error");
                }
            }

                Incidencia incidencia = db.Incidencia.Single(i => i.idIncidencia == id);
                return View(incidencia);
        }

        //
        // GET: /Incidencia/Create
        [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ActionResult Create()
        {
            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio");
            return View();
        } 

        //
        // POST: /Incidencia/Create

        [HttpPost]
        public ActionResult Create(Incidencia incidencia)
        {
            if (ModelState.IsValid)
            {
                db.Incidencia.AddObject(incidencia);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio", incidencia.idSocio);
            return View(incidencia);
        }
        
        //
        // GET: /Incidencia/Edit/5
         [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ActionResult Edit(int id)
        {

            int hayIncidencia = 0;

            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            if (HttpContext.User.IsInRole("Cliente"))
            {
                //Ver detalles únicame de la incidencia del cliente.
                Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
                //Ver el idSocio puede ver la incidencia actual
                Incidencia incidenciass = db.Incidencia.Where(i => i.idIncidencia == id).FirstOrDefault();
                if (incidenciass.idSocio != socio.idSocio)
                {
                    hayIncidencia = 0;
                }
                else
                {

                    hayIncidencia = 1;
                }
                //int hayIncidencia = db.Socio.Where(t => t.idSocio == incidenciass.idSocio).Count();               

                if (hayIncidencia == 0)
                {
                    return View("Error");
                }
            }

            if (HttpContext.User.IsInRole("Oficina"))
            {
                //Ver detalles únicame de la incidencia del cliente.
                Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
                //Recuperar el idoficina del socio actual
                Oficina oficina = db.Oficina.Where(o => o.idOficina == socio.idOficina).FirstOrDefault();
                //Recuperar los idsocios de la oficina actual
                Socio socios = db.Socio.Where(o => o.idOficina == oficina.idOficina).FirstOrDefault();
                //Ver si los idSocios pueden ver la incidencia actual
                Incidencia incidencias = db.Incidencia.Where(i => i.idIncidencia == id).FirstOrDefault();

                hayIncidencia = db.Socio.Where(t => t.idSocio == incidencias.idSocio).Count();

                if (hayIncidencia == 0)
                {
                    return View("Error");
                }
            }


            Incidencia incidencia = db.Incidencia.Single(i => i.idIncidencia == id);
            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio", incidencia.idSocio);
            return View(incidencia);
        }

        //
        // POST: /Incidencia/Edit/5

        [HttpPost]
        public ActionResult Edit(Incidencia incidencia)
        {
            if (ModelState.IsValid)
            {
                db.Incidencia.Attach(incidencia);
                db.ObjectStateManager.ChangeObjectState(incidencia, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idSocio = new SelectList(db.Socio, "idSocio", "nombreSocio", incidencia.idSocio);
            return View(incidencia);
        }

        //
        // GET: /Incidencia/Delete/5
         [Authorize(Roles = "Administrador,Oficina, Cliente")]
        public ActionResult Delete(int id)
        {

            int hayIncidencia = 0;

            Guid keyUser = (Guid)Membership.GetUser().ProviderUserKey;
            if (HttpContext.User.IsInRole("Cliente"))
            {
                //Ver detalles únicame de la incidencia del cliente.
                Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
                //Ver el idSocio puede ver la incidencia actual
                Incidencia incidenciass = db.Incidencia.Where(i => i.idIncidencia == id).FirstOrDefault();
                if (incidenciass.idSocio != socio.idSocio)
                {
                    hayIncidencia = 0;
                }
                else
                {

                    hayIncidencia = 1;
                }
                //int hayIncidencia = db.Socio.Where(t => t.idSocio == incidenciass.idSocio).Count();               

                if (hayIncidencia == 0)
                {
                    return View("Error");
                }
            }

            if (HttpContext.User.IsInRole("Oficina"))
            {
                //Ver detalles únicame de la incidencia del cliente.
                Socio socio = db.Socio.Where(o => o.idUsuario == keyUser).FirstOrDefault();
                //Recuperar el idoficina del socio actual
                Oficina oficina = db.Oficina.Where(o => o.idOficina == socio.idOficina).FirstOrDefault();
                //Recuperar los idsocios de la oficina actual
                Socio socios = db.Socio.Where(o => o.idOficina == oficina.idOficina).FirstOrDefault();
                //Ver si los idSocios pueden ver la incidencia actual
                Incidencia incidencias = db.Incidencia.Where(i => i.idIncidencia == id).FirstOrDefault();

                hayIncidencia = db.Socio.Where(t => t.idSocio == incidencias.idSocio).Count();

                if (hayIncidencia == 0)
                {
                    return View("Error");
                }
            }

            Incidencia incidencia = db.Incidencia.Single(i => i.idIncidencia == id);
            return View(incidencia);
        }

        //
        // POST: /Incidencia/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Incidencia incidencia = db.Incidencia.Single(i => i.idIncidencia == id);
            db.Incidencia.DeleteObject(incidencia);
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