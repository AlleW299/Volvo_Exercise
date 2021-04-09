using System;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Volvo_CTC.CommonFunctions;

namespace Volvo_CTC
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
            // Add Json formatter
            services.AddControllers(options =>
            {
                options.InputFormatters.Insert(0, new RawJsonBodyFormatter());
            });

            // Register swagger generator, defining document and set comments path
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "A simple...",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact{ Name = "Alexander Wåhlin", Email="AlexanderWahlin299@gmail.com" }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            // Enable middleware to serve generated swagger endpoint.
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Volvo Congestion Tax Calculator");
                s.InjectStylesheet("/swagger-ui/custom.css");
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
