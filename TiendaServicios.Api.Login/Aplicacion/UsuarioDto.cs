using System;

namespace TiendaServicios.Api.Login.Aplicacion
{
    public class UsuarioDto
    {
        public Guid IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
