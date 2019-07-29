namespace Proyecto.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Genero
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genero()
        {
            this.Artistas = new HashSet<Artista>();
        }
    
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El nombre no puede estar vacío.")]
        [DisplayName("Género")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "El número de carácteres debe ser mínimo de 4.")]
        public string nvarchNombre { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Artista> Artistas { get; set; }
    }
}
