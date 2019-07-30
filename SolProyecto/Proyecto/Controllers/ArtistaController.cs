using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Imaging;
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
        public ActionResult Index(string filtro)
        {
            var artistas = db.Artistas.Include(a => a.Genero);
            if (filtro != null)
            {
                artistas = artistas.Where(f => f.nvarchNombre.Contains(filtro) || f.nvarchPais.Contains(filtro) || f.Genero.nvarchNombre.Contains(filtro));
            }
            return View(artistas.ToList());
        }

        // GET: Artista/Details
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


        // GET: Artista/Edit
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

        // POST: Artista/Edit
        [Authorize]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Artista artista, HttpPostedFileBase image)
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
                db.Entry(artista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDGenero = new SelectList(db.Generos, "ID", "nvarchNombre", artista.IDGenero);
            return View(artista);
        }


        // GET: Artista/Delete
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

        // POST: Artista/Delete
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artista artista = db.Artistas.Find(id);
            //var discos = artista.Discos;
            //if (discos != null)
            //{
                //DiscoController ctl = new DiscoController();
                //ActionResult action = ctl.DeleteConfirmed(id);
                //db.Discos.RemoveRange(discos);
                //return action;
            //}
            db.Artistas.Remove(artista);
            db.SaveChanges();
            return RedirectToAction("Index");
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

        //A partir de obtener el ID de un artista, relaciono éste con Discos para traer una colección
        // de los mismos en una vista parcial llamada "_TraerDiscosporArtista".
        public ActionResult _TraerDiscosporArtista(int? id)
        {
            Artista artista = (from a in db.Artistas where a.ID == id select a).FirstOrDefault();

            if (artista == null)
            {
                return HttpNotFound();
            }
            return PartialView("_TraerDiscosporArtista", artista);
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
