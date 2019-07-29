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

        [Range(1, 25, ErrorMessage = "La duración de la canción no debe ser mayor a 25 minutos.")]
        [DisplayName("Duración (aprox)")]
        public Nullable<int> intDuracion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La canción debe pertenecer a un álbum.")]
        [DisplayName("Álbum")]
        public int IDDisco { get; set; }
    
        public virtual Disco Disco { get; set; }
    }
}
