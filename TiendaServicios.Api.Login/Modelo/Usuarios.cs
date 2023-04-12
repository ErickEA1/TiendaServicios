using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing.Printing;

namespace TiendaServicios.Api.Autor.Modelo
{
    public class Usuarios
    {
        [Key]
        public Guid IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
