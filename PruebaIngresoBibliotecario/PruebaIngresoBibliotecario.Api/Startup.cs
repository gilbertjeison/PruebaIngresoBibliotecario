using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PruebaIngresoBibliotecario.Application.Interfaces;
using PruebaIngresoBibliotecario.Application.Services;
using PruebaIngresoBibliotecario.Infraestructure.Context;
using PruebaIngresoBibliotecario.Infraestructure.Interfaces;
using PruebaIngresoBibliotecario.Infraestructure.Repository;
using System;
using System.Diagnostics;

namespace PruebaIngresoBibliotecario.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            Trace.Indent();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerDocument(options =>
            {
                options.Title = "Ingreso bibliotecario API";
                options.Version = "v1";
            });

            services.AddDbContext<PersistenceContext>(opt =>
            {
                opt.UseInMemoryDatabase("PruebaIngreso");
            });

            //Services
            services.AddTransient<IPrestamoService, PrestamoService>();

            //Repository
            services.AddTransient<IPrestamoRepository, PrestamoRepository>();

            services.AddControllers(mvcOpts =>
            {
            });

            services.AddMvc()
             .AddJsonOptions(options => {
                 options.JsonSerializerOptions
                        .IgnoreNullValues = true;
             });
        }


        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

        }
    }
}
