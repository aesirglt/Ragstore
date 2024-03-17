using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Totten.Solution.Ragstore.WebApi.AppSettings;
using Totten.Solution.Ragstore.WebApi.Behaviors;
using Totten.Solution.Ragstore.WebApi.Endpoints;
using Totten.Solution.Ragstore.WebApi.Handlers;
using Totten.Solution.Ragstore.WebApi.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StoreDatabaseSettings>(builder.Configuration.GetSection("StoreDatabase"));
builder.Services.Configure<HttpClientSettings>(builder.Configuration.GetSection("HttpClients"));

builder.Services.AddHttpClient(
        "WppHttpClient",
        (provider, client) =>
        {
            client.BaseAddress = new Uri(provider.GetService<HttpClientSettings>().WhatsClientUrl);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("dotnet-docs");
        });

builder.Services
       .AddEndpointsApiExplorer()
       .AddControllers()
       .AddNewtonsoftJson(op =>
       {
           op.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
           op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
       });

builder.Services
       .AddSwaggerGen(opts =>
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
       .Configure<RequestLocalizationOptions>(op => op.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR"))
       .AddMvc(options => options.Filters.Add(new ErrorHandlerAttribute()));

builder.Host
       .UseServiceProviderFactory(new AutofacServiceProviderFactory())
       .ConfigureContainer<ContainerBuilder>(containerBuilder =>
       {
           var configuration = MediatRConfigurationBuilder
                               .Create(typeof(Program).Assembly)
                               .WithAllOpenGenericHandlerTypesRegistered()
                               .WithRegistrationScope(RegistrationScope.Scoped)
                               .Build();

           containerBuilder.RegisterModule(new FluentValidationModule());
           containerBuilder.RegisterModule(new GlobalModule<Program>(builder.Configuration));
           containerBuilder.RegisterModule(new MediatRModule());

           containerBuilder.RegisterMediatR(configuration);
       })
    .ConfigureHostOptions(o => o.ShutdownTimeout = TimeSpan.FromSeconds(60));

var app = builder.Build();
app.UseAntiforgery();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "Titulo";
    });
}

app.UseHttpsRedirection();

app
   //Store endpoints
   .StoresEndpoints()
   //Items endpoints
   .ItemsEndpoints()
   //Callback endpoints
   .CallbacksEndpoints();

app.Run();
