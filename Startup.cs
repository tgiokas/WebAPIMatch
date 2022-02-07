using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;
using Swashbuckle.AspNetCore.Filters;

using WebAPIMatch.Models;
using WebAPIMatch.Service;
using WebAPIMatch.Helpers.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace WebAPIMatch
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
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd";               
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Match & Odds Management API",
                    Version = "v2",
                    Description = "Demo WebAPI with .NET Core 3.1 for Match management",
                });

                options.ExampleFilters();               

                options.MapType(typeof(TimeSpan), () => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("hh:mm")
                });

                options.MapType(typeof(DateTime), () => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("yyyy-MM-dd")
                });
                options.MapType(typeof(decimal), () => new OpenApiSchema
                {
                    Type= "string",
                    Example = new OpenApiString("00.00"),                    
                });

            });            
            
            services.AddSwaggerExamplesFromAssemblyOf<SwaggerExampleMatch>();           

            services.AddDbContext<DBEntities>(o =>o.UseSqlServer(Configuration.GetConnectionString("WebAPIDB")));
            services.AddScoped<IServiceMatch, ServiceMatch>();
            services.AddScoped<IServiceMatchOdd, MatchOddService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            //app.UseAuthorization();
            //app.UseStaticFiles();

            app.UseRouting(); 

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}/{id}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "PlaceInfo Services");
                options.RoutePrefix = string.Empty;   
            });
        }
    }
}
