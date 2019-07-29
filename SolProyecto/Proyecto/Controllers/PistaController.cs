using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
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
        public ActionResult Index(string filtro)
        {
            var pistasord = db.Pistas.Include(p => p.Disco).OrderBy(a => a.Disco.Artista.nvarchNombre);
            var pistas = db.Pistas.Include(p => p.Disco);
            if (filtro != null)
            {
                pistas = pistas.Where(f => f.nvarchNombre.Contains(filtro) || f.Disco.Artista.nvarchNombre.Contains(filtro) || f.Disco.Artista.Genero.nvarchNombre.Contains(filtro));
            }
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
            Pista nuevapista = new Pista();
            ViewBag.IDDisco = new SelectList(db.Discos, "ID", "nvarchNombre");
            return View(nuevapista);
        } 

        // POST: Pista/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pista pista)
        {
            if (ModelState.IsValid)
            {
                db.Pistas.Add(pista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDisco = new SelectList(db.Discos, "ID", "nvarchNombre", pista.IDDisco);
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
            ViewBag.IDDisco = new SelectList(db.Discos, "ID", "nvarchNombre", pista.IDDisco);
            return View(pista);
        }

        // POST: Pista/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pista pista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDDisco = new SelectList(db.Discos, "ID", "nvarchNombre", pista.IDDisco);
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
