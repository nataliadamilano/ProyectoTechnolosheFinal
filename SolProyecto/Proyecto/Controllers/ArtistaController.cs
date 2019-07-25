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
    public class ArtistaController : Controller
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: Artista
        public ActionResult Index()
        {
            var artistas = db.Artistas.Include(a => a.Genero);
            return View(artistas.ToList());
        }

        // GET: Artista/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artista artista = db.Artistas.Find(id);
            if (artista == null)
            {
                return HttpNotFound();
            }
            return View(artista);
        }


        // GET: /Artista/Create
        [Authorize]
        public ActionResult Create()
        {
            //Crea un nuevo artista
            //Artista nuevoartista = new Artista();
            ViewBag.IDGenero = new SelectList(db.Generos, "ID", "nvarchNombre");
            return View();
        }

        //
        // POST: /Artista/Create
        [Authorize]

        [HttpPost]
        public ActionResult Create(Artista artista, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //Hay una imagen? Si es así, guardarla
                if (image != null)
                {
                    artista.nvarchImageMimeType = image.ContentType;
                    artista.varbImagen = new byte[image.ContentLength];
                    image.InputStream.Read(artista.varbImagen, 0, image.ContentLength);
                }

                //Agrego el nuevo artista a la base de datos y guardo.
                db.Artistas.Add(artista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDGenero = new SelectList(db.Generos, "ID", "nvarchNombre", artista.IDGenero);
            return View(artista);
        }


        // GET: Artista/Edit/5
        [Authorize]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artista artista = db.Artistas.Find(id);
            if (artista == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDGenero = new SelectList(db.Generos, "ID", "nvarchNombre", artista.IDGenero);
            return View(artista);
        }

        // POST: Artista/Edit/5
        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Artista artista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDGenero = new SelectList(db.Generos, "ID", "nvarchNombre", artista.IDGenero);
            return View(artista);
        }

        // GET: Artista/Delete/5
        [Authorize]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artista artista = db.Artistas.Find(id);
            if (artista == null)
            {
                return HttpNotFound();
            }
            return View(artista);
        }

        // POST: Artista/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artista artista = db.Artistas.Find(id);
            db.Artistas.Remove(artista);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult _UltimasAdiciones(int number = 0)
        {
            //We want to display only the latest photos when a positive integer is supplied to the view.
            //Otherwise we'll display them all
            List<Artista> artistas;

            if (number == 0)
            {
                artistas = db.Artistas.ToList();
            }
            else
            {
                artistas = (from p in db.Artistas
                          orderby p.ID descending
                          select p).Take(number).ToList();
            }

            return PartialView("_UltimasAdiciones", artistas);
        }

        public ActionResult TraerDiscosporArtista (int? id)
        {
            Artista artista = (from a in db.Artistas where a.ID == id select a).FirstOrDefault();

            if (artista == null)
            {
                return HttpNotFound();
            }
            return View(artista);
        }
        //Este método obtiene la imagen a partir de un ID de artista que se le pasa
        public FileContentResult GetImage(int artistaID)
        {
            //Obtener la foto correspondiente al ID del artista
            Artista requestedPhoto = db.Artistas.FirstOrDefault(p => p.ID == artistaID);
            if (requestedPhoto != null)
            {
                return File(requestedPhoto.varbImagen, requestedPhoto.nvarchImageMimeType);
            }
            else
            {
                return null;
            }
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
