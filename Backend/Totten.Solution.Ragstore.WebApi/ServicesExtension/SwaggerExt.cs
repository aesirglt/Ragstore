namespace Totten.Solution.Ragstore.WebApi.ServicesExtension;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Totten.Solution.Ragstore.WebApi.Filters;

/// <summary>
/// 
/// </summary>
public static class SwaggerExt
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public static MvcOptions AddSwaggerMediaTypes(this MvcOptions options)
    {
        foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
            outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }
        foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
        {
            inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
        }

        return options;
    }
    /// <summary>
    /// 
    /// </summary>
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        => services.AddSwaggerGen(opts =>
        {
            opts.OperationFilter<RemoveAntiforgeryHeaderOperationFilter>();
            opts.OperationFilter<CustomHeaderSwaggerAttribute>();
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
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                                Enter 'Bearer'[space] and then your token in the text input below.
                                \r\n\r\nExample: 'Bearer 12345abcdef'",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            opts.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            opts.IncludeXmlComments(xmlPath);
            opts.CustomSchemaIds(x => x.FullName);
        })
       .Configure<RequestLocalizationOptions>(op => op.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR"));
}
