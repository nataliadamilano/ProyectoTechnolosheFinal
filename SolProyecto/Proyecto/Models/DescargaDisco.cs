using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    //Clase interna que utilizo para generar una lista con las descargas para cada álbum
    public class DescargaDisco
    {
        public string Disco { get; set; }
        public string Url { get; set; }
    }
}