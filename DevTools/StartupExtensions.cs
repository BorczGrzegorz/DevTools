using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace DevTools
{
    public static class StartupExtensions
    {
        public static void AddCustomIdMap<T>(this SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.MapType<T>(() => new OpenApiSchema { Type = "string" });
        }

        public static void AddConverter<T>(this MvcNewtonsoftJsonOptions options, T converter) where T : JsonConverter
        {
            options.SerializerSettings.Converters.Insert(0, converter);
        }
    }
}
