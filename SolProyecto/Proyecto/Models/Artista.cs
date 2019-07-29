//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Proyecto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class Artista
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artista()
        {
            this.Discos = new HashSet<Disco>();
        }

        public int ID { get; set; }

        [DisplayName("Foto del artista")]
        public byte[] varbImagen { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string nvarchImageMimeType { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre no puede estar vacío.")]
        [DisplayName("Nombre del artista")]
        public string nvarchNombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El país no puede estar vacío.")]
        [DisplayName("País")]
        public string nvarchPais { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El año no puede estar vacío.")]
        [Range(1900, 2019, ErrorMessage = "El año debe estar comprendido entre 1900 y 2019.")]
        [DisplayName("Año")]
        public int intAño { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El género no puede estar vacío.")]
        [DisplayName("Género")]
        public int IDGenero { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Biografía")]
        public string nvarchBiografia { get; set; }
    
        public virtual Genero Genero { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Disco> Discos { get; set; }
    }
}
