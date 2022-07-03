using System.Diagnostics.CodeAnalysis;
using Boxed.AspNetCore;
using InvoiceGenerator.WebApi.Extensions;

namespace InvoiceGenerator.WebApi;

[ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("invoiceConnectionString");
            services
                .AddCustomCors()
                .AddControllers().Services
                .AddTransientServices(connectionString)
                .AddMappers()
                .AddRazorPages();

            if (!Environment.IsEnvironment("IntegrationTesting"))
            {
                services.AddCustomSwagger();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseForwardedHeaders()
                // .UseResponseCaching()
                // .UseResponseCompression()
                // .UseIf(
                //     env.IsDevelopment(),
                //     app => app.UseDeveloperExceptionPage())
                .UseDeveloperExceptionPage()
                .UseRouting()
                // .UseAuthentication()
                // .UseAuthorization()
                .UseCors("AllowAny")
                .UseEndpoints(
                    builder => {
                        builder
                            .MapControllers()
                            .RequireCors("AllowAny");
                        builder.MapRazorPages();
                    })
                .UseIf(
                    !env.IsEnvironment("IntegrationTesting"),
                    app => app.UseSwagger()
                )
                .UseIf(
                    !env.IsEnvironment("IntegrationTesting"),
                    app => app.UseSwaggerUI(c =>
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "InvoiceGenerator.WebApi v1"))
                );
        }
    }