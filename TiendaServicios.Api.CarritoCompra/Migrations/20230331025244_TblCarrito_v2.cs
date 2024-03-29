﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TiendaServicios.Api.CarritoCompra.Migrations
{
    public partial class TblCarrito_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdUser",
                table: "CarritoSesiones",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "CarritoSesiones");
        }
    }
}
