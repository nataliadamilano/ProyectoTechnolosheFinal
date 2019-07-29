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
        public ActionResult Index(string filtro)
        {
            var discos = db.Discos.Include(d => d.Artista);
            if (filtro != null)
            {
                discos = discos.Where(f => f.nvarchNombre.Contains(filtro) || f.Artista.nvarchNombre.Contains(filtro) || f.Artista.Genero.nvarchNombre.Contains(filtro));
            }
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

                //Agrego el nuevo disco a la base de datos y guardo.
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
        public ActionResult Edit(Disco disco)
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

        //Este método obtiene la imagen a partir de un ID de Disco que se le pasa
        public FileContentResult GetImage(int discoID)
        {
            //Obtener la foto correspondiente al ID del disco
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
            //Quiero mostrar solo los últimos discos que subí a partir de un int number que me pasan.
            //Sino muestro todos...
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

        public ActionResult _TraerPistasporDisco(int? id)
        {
            Disco disco = (from d in db.Discos where d.ID == id select d).FirstOrDefault();

            if (disco == null)
            {
                return HttpNotFound();
            }
            return PartialView("_TraerPistasporDisco", disco);
        }

        public JsonResult LlamarJson()
        {
            var output = ObtenerListaDiscos();
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        private List<DescargaDisco> ObtenerListaDiscos()
        {
            List<DescargaDisco> lDescargas = new List<DescargaDisco>(){
            new DescargaDisco(){ Disco = "Screaming for Vengeance", Url = "magnet:?xt=urn:btih:2adff61c51650145d80b2ec7e6ea2a4fabc0b9a8&dn=Judas+Priest+-+1982+-+Screaming+For+Vengeance+%5Bmp3%2C+CBR%2C+320kbps&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.openbittorrent.com%3A80&tr=udp%3A%2F%2Fopen.demonii.com%3A1337&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Fexodus.desync.com%3A6969"},
            new DescargaDisco(){ Disco = "Animal Boy", Url = "magnet:?xt=urn:btih:7af372d6d5508609dee3324f16f3174d278ba3b4&dn=10+Ramones-Animal_Boy-1986-rH_INT&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.openbittorrent.com%3A80&tr=udp%3A%2F%2Fopen.demonii.com%3A1337&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Fexodus.desync.com%3A6969"},
            new DescargaDisco(){ Disco = "Road To Ruin", Url = "magnet:?xt=urn:btih:6d1bf8ec501ba9daa0489c92c2ca4d13a13673d6&dn=Ramones+-+Road+To+Ruin+%281978%29+%5BEAC-FLAC%5D+The+Sire+Years+%282013%29+&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.openbittorrent.com%3A80&tr=udp%3A%2F%2Fopen.demonii.com%3A1337&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Fexodus.desync.com%3A6969"},
            new DescargaDisco(){ Disco = "Abbey Road", Url = "magnet:?xt=urn:btih:eea2f846ea8f23d774a28136265da1e1508cf8c7&dn=The+Beatles+-+Abbey+Road+%5B320k+MP3%5D&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.openbittorrent.com%3A80&tr=udp%3A%2F%2Fopen.demonii.com%3A1337&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Fexodus.desync.com%3A6969"}, 
            new DescargaDisco(){ Disco = "Defenders Of The Faith", Url = "magnet:?xt=urn:btih:0962efe7587b9282984030b3040ec056b5316172&dn=Judas+Priest+-+1984+-+Defenders+of+the+Faith+%5B320kbs%5D+%7E%7ERenovati&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.openbittorrent.com%3A80&tr=udp%3A%2F%2Fopen.demonii.com%3A1337&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Fexodus.desync.com%3A6969" },
            new DescargaDisco(){ Disco = "Machine Head", Url = "magnet:?xt=urn:btih:c3a6b8c218329f757622a7726be4d12c2941ffa1&dn=Deep+Purple+-+Machine+Head+%281972%29+%40+320kbps&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.openbittorrent.com%3A80&tr=udp%3A%2F%2Fopen.demonii.com%3A1337&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Fexodus.desync.com%3A6969" }
        }; 
            return lDescargas;
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
