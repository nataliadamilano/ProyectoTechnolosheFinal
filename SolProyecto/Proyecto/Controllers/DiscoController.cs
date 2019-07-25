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
    public class DiscoController : Controller
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: Disco
        public ActionResult Index()
        {
            var discos = db.Discos.Include(d => d.Artista);
            return View(discos.ToList());
        }

        // GET: Disco/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disco disco = db.Discos.Find(id);
            if (disco == null)
            {
                return HttpNotFound();
            }
            return View(disco);
        }

        // GET: Disco/Create
        [Authorize]
        public ActionResult Create()
        {
            Disco nuevodisco = new Disco();
            ViewBag.IDArtista = new SelectList(db.Artistas, "ID", "nvarchNombre");
            return View(nuevodisco);
        }

        // POST: Disco/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Disco disco, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //Hay una imagen? Si es así, guardarla
                if (image != null)
                {
                    disco.nvarchImageMimeType = image.ContentType;
                    disco.varbPortada = new byte[image.ContentLength];
                    image.InputStream.Read(disco.varbPortada, 0, image.ContentLength);
                }

                //Agrego el nuevo artista a la base de datos y guardo.
                db.Discos.Add(disco);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDArtista = new SelectList(db.Artistas, "ID", "nvarchNombre", disco.IDArtista);
            return View(disco);
        }

        // GET: Disco/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disco disco = db.Discos.Find(id);
            if (disco == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDArtista = new SelectList(db.Artistas, "ID", "nvarchNombre", disco.IDArtista);
            return View(disco);
        }

        // POST: Disco/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,varbPortada,nvarchImageMimeType,nvarchNombre,intAño,IDArtista")] Disco disco)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disco).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDArtista = new SelectList(db.Artistas, "ID", "nvarchNombre", disco.IDArtista);
            return View(disco);
        }

        // GET: Disco/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disco disco = db.Discos.Find(id);
            if (disco == null)
            {
                return HttpNotFound();
            }
            return View(disco);
        }

        // POST: Disco/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Disco disco = db.Discos.Find(id);
            db.Discos.Remove(disco);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Este método obtiene la imagen a partir de un ID de artista que se le pasa
        public FileContentResult GetImage(int discoID)
        {
            //Obtener la foto correspondiente al ID del artista
            Disco requestedPhoto = db.Discos.FirstOrDefault(p => p.ID == discoID);
            if (requestedPhoto != null)
            {
                return File(requestedPhoto.varbPortada, requestedPhoto.nvarchImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult _UltimasAdicionesDiscos(int number = 0)
        {
            //We want to display only the latest photos when a positive integer is supplied to the view.
            //Otherwise we'll display them all
            List<Disco> discos;

            if (number == 0)
            {
                discos = db.Discos.ToList();
            }
            else
            {
                discos = (from d in db.Discos
                            orderby d.ID descending
                            select d).Take(number).ToList();
            }

            return PartialView("_UltimasAdicionesDiscos", discos);
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
