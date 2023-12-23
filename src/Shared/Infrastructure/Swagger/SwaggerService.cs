using Common.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Infrastructure.Core.StartupConfiguration;

public static class SwaggerService
{
    public static IServiceCollection AddMySwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerSettings = configuration.GetSwaggerSettings();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = swaggerSettings.Title,
                Version = swaggerSettings.Version
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = ParameterLocation.Header,
                Scheme = "Bearer"
            });

         
            options.CustomSchemaIds(DefaultSchemaIdSelector);
            options.DocumentFilter<LowercaseDocumentFilter>();
            options.OperationFilter<SwaggerGenerationFilter>();
        });

        return services;
    }

    private static string DefaultSchemaIdSelector(Type modelType)
    {
        if (modelType.DeclaringType != null)
        {
            var parentName = string.Empty;
            if (modelType.DeclaringType.DeclaringType != null)
                parentName = modelType.DeclaringType.DeclaringType.Name;

            parentName += modelType.DeclaringType.Name;

            return $"{parentName}{modelType.Name}";
        }


        if (!modelType.IsConstructedGenericType)
            return modelType.Name;

        var prefix = modelType.GetGenericArguments()
            .Select(DefaultSchemaIdSelector)
            .Aggregate((previous, current) => previous + current);

        return prefix + modelType.Name.Split('`').First();
    }


    public static void UseMySwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        var swaggerOptions = configuration.GetSwaggerSettings();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint(swaggerOptions.Endpoint, swaggerOptions.Title);
            c.DocExpansion(DocExpansion.None);
        });
    }
}

public class LowercaseDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths.ToDictionary(entry => LowercaseEverythingButParameters(entry.Key),
            entry => entry.Value);

        swaggerDoc.Paths.Clear();

        foreach (var (key, value) in paths)
            swaggerDoc.Paths.Add(key, value);
    }

    private static string LowercaseEverythingButParameters(string key)
    {
        var data = key.Split('/').Select(x => x.Contains("{") ? x : x.ToLower());

        return string.Join("/", data);
    }
}

public class SwaggerGenerationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor)
            operation.OperationId = descriptor.ActionName + descriptor.ControllerName;

        var deleteRequestTypes = operation.RequestBody?.Content.Where(x => x.Key.Contains("odata")).ToList();
        deleteRequestTypes?.ForEach(x => operation.RequestBody.Content.Remove(x));

        foreach (var response in operation.Responses)
        {
            var deleteResponseTypes = response.Value.Content.Where(x => x.Key.Contains("odata")).ToList();
            deleteResponseTypes.ForEach(x => response.Value.Content.Remove(x));
        }
    }
}

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) return;

        schema.Type = "string";
        schema.Enum.Clear();
        Enum.GetNames(context.Type)
            .ToList()
            .ForEach(n => schema.Enum.Add(new OpenApiString(n)));
    }
}

public static class GlobalSettingService
{
    public static SwaggerSettings GetSwaggerSettings(this IConfiguration configuration)
    {
        var swaggerOptions = new SwaggerSettings();
        configuration.GetSection("Swagger").Bind(swaggerOptions);

        return swaggerOptions;
    }
}