using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Web.API.Extensions;

public static class ApplySwaggerExtensions
{
    public static void ApplySwagger(this WebApplicationBuilder builder)
    {

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            c.EnableAnnotations();

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Orchid Pharmed - Project Management",
                Version = "v1",
                Description = "Project Management API by MiM",
                TermsOfService = new Uri("https://mjmim.ir/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Morteza Jafary",
                    Email = "mortezamim@gmail.com",
                    Url = new Uri("https://mjmim.ir"),
                },
                License = new OpenApiLicense
                {
                    Name = "Project Management API LICX",
                    Url = new Uri("https://mjmim.ir/license"),
                }
            });
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "Orchid Pharmed - Project Management", Version = "v2" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Place to add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                    },
                    new List<string>()
                }
            });
        });

    }
}
