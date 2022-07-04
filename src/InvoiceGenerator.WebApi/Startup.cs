using System.Diagnostics.CodeAnalysis;
using InvoiceGenerator.WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

namespace InvoiceGenerator.WebApi;

[ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("invoiceConnectionString");
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddCors(opt =>
            {
                opt.AddPolicy(name: "AllowAny", builder =>
                {
                    builder.WithOrigins().AllowAnyHeader().AllowAnyOrigin();
                });
            });

            services.AddRazorPages();

            services.AddTransientServices(connectionString).AddMappers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsEnvironment("IntegrationTesting"))
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvoiceGenerator.WebApi v1"));
            }

            app.UseCors("AllowAny");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }