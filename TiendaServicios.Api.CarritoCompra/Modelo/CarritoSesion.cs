using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using TiendaServicios.Api.CarritoCompra.Aplicacion;

namespace TiendaServicios.Api.CarritoCompra.Modelo
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public Guid IdUser { get; set; }
        public ICollection<CarritoSesionDetalle> ListaDetalle { get; set; }
    }
}
