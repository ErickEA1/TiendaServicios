﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace TiendaServicios.Api.Autor.Modelo
{
    public class AutorLibro
    {
        public int AutorLibroId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public ICollection<GradoAcademico> GradosAcademicos { get; set; }
        public Guid AutorLibroGuid { get; set; }
    }
}
