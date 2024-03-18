namespace Totten.Solution.Ragstore.WebApi.ServicesExtension;

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Totten.Solution.Ragstore.WebApi.Behaviors;

/// <summary>
/// 
/// </summary>
public static class SwaggerExt
{
    /// <summary>
    /// 
    /// </summary>
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        => services.AddSwaggerGen(opts =>
        {
            opts.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API RagnaStore - sua fonte de dados",
                Version = "v1",
                Description = @$"{"\n"}
                            API voltada para listagem e informações de lojas e items de servidores de ragnarok online.
                            Aqui é possivel salvar lojas e obter dados de itens, historico e quem está vendendo eles.",
                Contact = new OpenApiContact
                {
                    Name = "Vesper",
                    Email = "aleffmds@gmail.com",
                    Url = new Uri("https://www.instagram.com/aleff.moura"),
                    Extensions =
                {
                    { "x-company", new OpenApiString("Totten Solutions") }
                }
                },
                License = new OpenApiLicense
                {
                    Name = "License",
                    Url = new Uri("https://google.com.br")
                },
                TermsOfService = new Uri("https://mail.google.com"),
                Extensions = new Dictionary<string, IOpenApiExtension>
            {
                { "x-company", new OpenApiString("Company Name") },
                { "x-contact", new OpenApiString("contact@email.com") }
            }
            });
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opts.IncludeXmlComments(xmlPath);
            opts.CustomSchemaIds(x => x.FullName);
            opts.OperationFilter<RemoveAntiforgeryHeaderOperationFilter>();
        })
       .Configure<RequestLocalizationOptions>(op => op.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR"));
}
