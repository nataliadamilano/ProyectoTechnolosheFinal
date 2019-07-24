using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class PistaController : Controller
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: Pista
        public ActionResult Index()
        {
            var pistas = db.Pistas.Include(p => p.Disco);
            return View(pistas.ToList());
        }

        // GET: Pista/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pista pista = db.Pistas.Find(id);
            if (pista == null)
            {
                return HttpNotFound();
            }
            return View(pista);
        }

        // GET: Pista/Create
        public ActionResult Create()
        {
            ViewBag.IDDisco = new SelectList(db.Discos, "ID", "varchNombre");
            return View();
        }

        // POST: Pista/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,varchNombre,intDuracion,IDDisco")] Artista artista, Pista pista)
        {
            if (ModelState.IsValid)
            {
                db.Pistas.Add(pista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDArtista = new SelectList(db.Artistas, "ID", "varchNombre", artista.ID);
            ViewBag.IDDisco = new SelectList(db.Discos, "ID", "varchNombre", pista.IDDisco);
            return View(pista);
        }

        // GET: Pista/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pista pista = db.Pistas.Find(id);
            if (pista == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDDisco = new SelectList(db.Discos, "ID", "varchNombre", pista.IDDisco);
            return View(pista);
        }

        // POST: Pista/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,varchNombre,intDuracion,IDDisco")] Pista pista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDisco = new SelectList(db.Discos, "ID", "varchNombre", pista.IDDisco);
            return View(pista);
        }

        // GET: Pista/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pista pista = db.Pistas.Find(id);
            if (pista == null)
            {
                return HttpNotFound();
            }
            return View(pista);
        }

        // POST: Pista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pista pista = db.Pistas.Find(id);
            db.Pistas.Remove(pista);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
