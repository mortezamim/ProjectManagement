﻿
using Microsoft.AspNetCore.Mvc.Versioning;

namespace Web.API.Extensions;

public static class ApiVersionExtensions
{
    public static void ApplyApiVersion(this WebApplicationBuilder builder)
    {
        //builder.Services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'V");

        //builder.Services.AddApiVersioning();

        //builder.Services.AddApiVersioning(options =>
        //{
        //    options.DefaultApiVersion = new ApiVersion(1, 1);
        //    options.ReportApiVersions = true;
        //    options.AssumeDefaultVersionWhenUnspecified = true;
        //    options.ApiVersionReader = ApiVersionReader.Combine(
        //        new UrlSegmentApiVersionReader(),
        //        new HeaderApiVersionReader("X-Api-Version"));
        //});

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
                //options.SubstitutionFormat = "VVV";
                options.SubstituteApiVersionInUrl = true;
                //options.ApiVersionParameterSource = new UrlSegmentApiVersionReader();
            });
    }
}
