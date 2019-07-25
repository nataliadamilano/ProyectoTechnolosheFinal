using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;


namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        private ProyectoEntities db = new ProyectoEntities();

        public ActionResult Index()
        {
            return View(db.Generos.ToList());
        }

        public ActionResult _TraerArtistasPorGenero(int? id)
        {
            Genero genero = (from g in db.Generos where g.ID == id select g).FirstOrDefault();

            if (genero == null)
            {
                return HttpNotFound();
            }
            return View(genero);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}