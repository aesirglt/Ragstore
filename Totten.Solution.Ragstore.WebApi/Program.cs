using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Totten.Solution.Ragstore.WebApi.Behaviors;
using Totten.Solution.Ragstore.WebApi.Configurations.AppSettings;
using Totten.Solution.Ragstore.WebApi.Endpoints;
using Totten.Solution.Ragstore.WebApi.Handlers;
using Totten.Solution.Ragstore.WebApi.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StoreDatabaseSettings>(builder.Configuration.GetSection("StoreDatabase"));

builder.Services
       .AddEndpointsApiExplorer()
       .AddControllers()
       .AddNewtonsoftJson(op =>
       {
           op.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
           op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
       });

builder.Services
       .AddSwaggerGen(c => c.OperationFilter<RemoveAntiforgeryHeaderOperationFilter>())
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
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app
   //Store endpoints
   .StoreGetEndpoint()
   .StorePostEndpoint()
   .StoreGetByIdEndpoint()
   //Items endpoints
   .ItemGetByNameEndpoint();

app.Run();
