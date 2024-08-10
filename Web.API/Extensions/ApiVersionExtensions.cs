using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Web.API.Extensions;

public static class ApiVersionExtensions
{
    public static void ApplyApiVersion(this WebApplicationBuilder builder)
    {
        builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VVV");

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        });
    }
}
