using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Web.API.Extensions;
using Web.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.ApplyLoggerConfig();

builder.ApplyApiVersion();

builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    c.EnableAnnotations();

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Code Maze API",
        Version = "v1",
        Description = "CompanyEmployees API by CodeMaze",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "John Doe",
            Email = "John.Doe@gmail.com",
            Url = new Uri("https://twitter.com/johndoe"),
        },
        License = new OpenApiLicense
        {
            Name = "CompanyEmployees API LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Code Maze API", Version = "v2" });

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
    //  c.SwaggerDoc("v1", new OpenApiInfo
    //  {
    //      Title = "Orchid Project Management",
    //      Version = "v1"
    //  });
    //  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    //  {
    //      In = ParameterLocation.Header,
    //      Description = "Please insert JWT with Bearer into field",
    //      Name = "Authorization",
    //      Type = SecuritySchemeType.ApiKey
    //  });
    //  c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    // {
    //   new OpenApiSecurityScheme
    //   {
    //     Reference = new OpenApiReference
    //     {
    //       Type = ReferenceType.SecurityScheme,
    //       Id = "Bearer"
    //     }
    //    },
    //    new string[] { }
    //  }
    //});
});

builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors",
    builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["HmacSecretKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Code Maze API v1");
        s.SwaggerEndpoint("/swagger/v2/swagger.json", "Code Maze API v2");
    });

    app.UseReDoc(c =>
    {
        c.DocumentTitle = "REDOC API Documentation";
        c.SpecUrl = "/swagger/v1/swagger.json";
        //c.SpecUrl = "/swagger/v2/swagger.json";
    });
    app.ApplyMigrations();
}

//app.UseHttpsRedirection();
app.UseCors("cors");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }