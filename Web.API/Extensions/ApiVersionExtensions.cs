using Microsoft.AspNetCore.Mvc.Versioning;

namespace Web.API.Extensions;

public static class ApiVersionExtensions
{
    public static void ApplyApiVersion(this WebApplicationBuilder builder)
    {
        builder.Services.AddApiVersioning(
        options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        builder.Services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

    }
}
