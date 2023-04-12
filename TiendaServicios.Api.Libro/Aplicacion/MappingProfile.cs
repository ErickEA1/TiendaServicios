using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using TiendaServicios.Api.Libro.Modelo;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
