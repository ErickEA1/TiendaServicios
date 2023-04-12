using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Persistencia;
using MediatR;

namespace TiendaServicios.Api.Libro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddDbContext<ContextoLibreria>(options =>
           options.UseMySql(
               Configuration.GetConnectionString("DefaultConnection"),ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));
            services.AddCors();
            services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
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
