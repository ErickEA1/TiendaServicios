using System.ComponentModel.DataAnnotations;
using System;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class LibroMaterialDto
    {
        [Key]
        public Guid LibreriaMaterialId { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public double Precio { get; set; }
        public Guid AutorLibro { get; set; }
    }
}
