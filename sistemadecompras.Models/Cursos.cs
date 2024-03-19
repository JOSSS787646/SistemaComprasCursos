//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sistemadecompras.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cursos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cursos()
        {
            this.AlumnoCursos = new HashSet<AlumnoCursos>();
        }
    
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }
        public Nullable<decimal> Precio { get; set; }
        public Nullable<System.DateTime> FechaInicial { get; set; }
        public Nullable<System.DateTime> FechaActulizacion { get; set; }
        public string UrlImgaenCurso { get; set; }
        public Nullable<int> idCategoria { get; set; }
        public Nullable<bool> BitEliminado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlumnoCursos> AlumnoCursos { get; set; }
        public virtual Categorias Categorias { get; set; }
    }
}
