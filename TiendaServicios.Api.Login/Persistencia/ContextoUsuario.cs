using Microsoft.EntityFrameworkCore;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Login.Persistencia
{
    public class ContextoUsuario:DbContext
    {
        public ContextoUsuario(DbContextOptions<ContextoUsuario> options): base(options)
        {

        }
        public DbSet<Usuarios> UsuariosLogin { get; set; }
    }
}
