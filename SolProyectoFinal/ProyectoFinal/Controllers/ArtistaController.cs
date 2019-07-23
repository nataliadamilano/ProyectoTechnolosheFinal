using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    public class ArtistaController : Controller
    {
        private ProyectoEntities db = new ProyectoEntities();

        // GET: Artista
        public ActionResult Index()
        {
            return View(db.Artistas.ToList());
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

        // GET: /Photo/Create
        public ActionResult Create()
        {
            //Create the new photo
            Artista nuevoartista = new Artista();
            return View(nuevoartista);
        }

        //
        // POST: /Photo/Create
        [HttpPost]
        public ActionResult Create(Artista artista, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                //Is there a photo? If so save it
                if (image != null)
                {
                    artista.ImageMimeType = image.ContentType;
                    artista.varbImagen = new byte[image.ContentLength];
                    image.InputStream.Read(artista.varbImagen, 0, image.ContentLength);
                }

                //Add the photo to the database and save it
                db.Artistas.Add(artista);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artista);
        }


        // GET: Artista/Edit/5
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
            return View(artista);
        }

        // POST: Artista/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,varbImagen,varchNombre,intGenero,intAño,varchBiografia")] Artista artista)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artista).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artista);
        }

        // GET: Artista/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artista artista = db.Artistas.Find(id);
            db.Artistas.Remove(artista);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //This action gets the photo file for a given Photo ID
        public FileContentResult GetImage(int artistaID)
        {
            //Get the right photo
            Artista requestedPhoto = db.Artistas.FirstOrDefault(p => p.ID == artistaID);
            if (requestedPhoto != null)
            {
                return File(requestedPhoto.varbImagen, requestedPhoto.ImageMimeType);
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
