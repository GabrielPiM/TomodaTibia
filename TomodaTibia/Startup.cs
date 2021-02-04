using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TomodaTibiaAPI;
using TomodaTibiaAPI.Services;
using Swashbuckle.AspNetCore.Swagger;
using EFDataAcessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TomodaTibia
{
    public class Startup
    {
        
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
     
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
           

            services.AddHttpClient<TibiaApiService>((httpClient) =>
            {
                httpClient.BaseAddress = new Uri(Configuration["TibiaAPI"]);
            });

            string con = Configuration.GetConnectionString("DBContext");

            services.AddDbContext<TomodaTibiaContext>(options => options.UseSqlServer(con));

            services.AddScoped<HuntDataService>();
            services.AddScoped<AuthorDataService>();
            services.AddScoped<JsonReturn>();

            services.AddHttpClient();
            services.AddControllers();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMvc().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen();

      


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

            });
       
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=hunt}/{action=Index}");
            });

         


        }
    }
}
