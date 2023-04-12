using AutoMapper;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Login.Aplicacion
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuarios, UsuarioDto>();
        }
    }
}
