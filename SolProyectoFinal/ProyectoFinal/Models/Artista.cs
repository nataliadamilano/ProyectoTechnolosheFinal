//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoFinal.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class Artista
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artista()
        {
            this.Discos = new HashSet<Disco>();
        }
    
        public int ID { get; set; }
        public byte[] varbImagen { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
        public string varchNombre { get; set; }
        public int intAño { get; set; }
        public int intGenero { get; set; }
        public string varchBiografia { get; set; }
    
        public virtual Genero Genero { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Disco> Discos { get; set; }
    }
}