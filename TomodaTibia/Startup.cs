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
using TomodaTibiaAPI.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using AutoMapper;
using TomodaTibiaAPI.Maps;
using TomodaTibiaAPI.Utils;
using TomodaTibiaAPI.BLL;
using AspNetCoreRateLimit;

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
            #region  RateLimitingConfiguration
            // needed to load configuration from appsettings.json
            services.AddOptions();

            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();

            //load general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            //load ip rules from appsettings.json
            //services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            // inject counter and rules stores
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            // Add framework services.
            //services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            #endregion  

            services.AddDbContext<TomodaTibiaContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("TomodaTibia"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                });
            });

            #region Services
            services.AddHttpClient();
            services.AddHttpClient<TibiaApiService>((httpClient) =>
            {
                httpClient.BaseAddress = new Uri(Configuration["TibiaAPI"]);
            });
            services.AddScoped<BaseBLL>();
            services.AddScoped<HuntBLL>();
            services.AddScoped<AuthorBLL>();

            services.AddScoped<HuntDataService>();
            services.AddScoped<AuthorDataService>();
            services.AddScoped<AuthenticationDataService>();
            services.AddScoped<PredictSearchDataService>();
            services.AddScoped<CurrentUserService>();
            #endregion

            services.AddMvc().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
     
            services.AddAutoMapper(typeof(MapsProfiles));
            services.AddSwaggerGen();

            services.AddAuthentication("TomodaTibiaAPI")
                .AddCookie("TomodaTibiaAPI", options =>
                {
                    options.Cookie.Name = "CookieTomodaTibia";
                });
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

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseIpRateLimiting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(options =>
            {
                options.MapDefaultControllerRoute();
            });
        }
    }
}
