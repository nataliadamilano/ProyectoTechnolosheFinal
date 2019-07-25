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

    public partial class Pista
    {
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre de la canción no puede estar vacío.")]
        [DisplayName("Nombre")]
        public string nvarchNombre { get; set; }

        [Range(1, 20, ErrorMessage = "La duración de la canción no debe ser mayor a 20 minutos.")]
        [DisplayName("Duración")]
        public Nullable<int> intDuracion { get; set; }

        [DisplayName("Pista")]
        public string nvarchPath { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La canción debe pertenecer a un álbum.")]
        [DisplayName("Álbum")]
        public int IDDisco { get; set; }
    
        public virtual Disco Disco { get; set; }
    }
}
