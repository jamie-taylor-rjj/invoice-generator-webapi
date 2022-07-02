using System.Diagnostics.CodeAnalysis;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using InvoiceGenerator.WebApi.Helpers;
using InvoiceGenerator.BusinessLogic;
using InvoiceGenerator.Domain;
using InvoiceGenerator.ViewModels;

namespace InvoiceGenerator.WebApi.Extensions;

[ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add cross-origin resource sharing (CORS) services and configures named CORS policies. See
        /// https://docs.asp.net/en/latest/security/cors.html
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> object.</param>
        /// <returns>The <see cref="IServiceCollection"/> object with CORS services added.</returns>
        public static IServiceCollection AddCustomCors(this IServiceCollection services) =>
            // TODO Create named CORS policies here which can be consume using application.UseCors("PolicyName") or a [EnableCors("PolicyName")] attribute on a controller or action.
            services.AddCors(
                options =>
                    options.AddPolicy(
                        "AllowAny",
                        x => x
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(origin => true)
                            .AllowAnyHeader()));

    /// <summary>
    /// Adds all services which require a Transient scope
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> object.</param>
    /// <param name="connectionString">A string representing the database connection string</param>
    /// <returns>The <see cref="IServiceCollection"/> object with the required Transient services</returns>
    public static IServiceCollection AddTransientServices(this IServiceCollection services, string connectionString) =>
            services
                .AddTransient<IClientService, ClientService>()
                .AddTransient(typeof(IRepository<>), typeof(Repository<>))
                .AddDbContext<InvoiceDbContext>(options => options.UseSqlServer(connectionString));

        /// <summary>
        /// Adds all require mappers (using <see cref="IMapper{TSource,TDestination}"/>) to the service collection 
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> object.</param>
        /// <returns>The <see cref="IServiceCollection"/> object with the required mappers added</returns>
        public static IServiceCollection AddMappers(this IServiceCollection services) =>
            services
                .AddTransient<IMapper<ClientViewModel, Domain.Models.Client>, ClientViewModelMapper>()
                .AddTransient<IMapper<ClientNameViewModel, Domain.Models.Client>, ClientNameViewModelMapper>()
                .AddTransient<IMapper<ClientCreationModel, Domain.Models.Client>, ClientCreationViewModelMapper>();

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            var appName = CommonHelpers.GetAssemblyName();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = appName,
                    Description = "A .NET WebApi project which exposes a WebApi for interacting with the InvoiceGenerator database",
                    TermsOfService = new Uri("https://rjj-software.co.uk/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Jamie Taylor",
                        Email = string.Empty,
                        Url = new Uri("https://rjj-software.co.uk")
                    }
                });
            
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{appName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            return services;
        }
    }