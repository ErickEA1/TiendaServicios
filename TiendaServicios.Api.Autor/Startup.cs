using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor
{
    public class Startup
    {
        private readonly string _MyCors = "CorsApi";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //FluentValidation: Validaciones para los modelos en el programa
            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevos>());

            services.AddDbContext<ContextoAutor>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            services.AddCors();
            //Agregar MediaTr
            services.AddMediatR(typeof(Nuevos.Manejador).Assembly);
            services.AddAutoMapper(typeof(Consulta.Manejador));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x =>
x.AllowAnyHeader()
.AllowAnyMethod()
//.AllowCredentials()
.WithOrigins("*")
//.AllowAnyOrigin()
//.WithOrigins(new string[] {
//   "http://localhost:8080/",
//  "http://192.168.0.3:8080/",
//  "http://localhost:8080/login" })
);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
