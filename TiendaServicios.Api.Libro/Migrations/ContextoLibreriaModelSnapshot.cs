﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TiendaServicios.Api.Libro.Persistencia;

namespace TiendaServicios.Api.Libro.Migrations
{
    [DbContext(typeof(ContextoLibreria))]
    partial class ContextoLibreriaModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("TiendaServicios.Api.Libro.Modelo.LibreriaMaterial", b =>
                {
                    b.Property<Guid>("LibreriaMaterialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AutorLibro")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("FechaPublicacion")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Precio")
                        .HasColumnType("double");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext");

                    b.HasKey("LibreriaMaterialId");

                    b.ToTable("LibreriasMaterial");
                });
#pragma warning restore 612, 618
        }
    }
}