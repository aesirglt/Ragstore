using Totten.Solution.Ragstore.Infra.Data.EntityFrameworkIdentity;
using Totten.Solution.Ragstore.WebApi.AppSettings;
using Totten.Solution.Ragstore.WebApi.Endpoints;
using Totten.Solution.Ragstore.WebApi.Handlers;
using Totten.Solution.Ragstore.WebApi.ServicesExtension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppSettingsClass(builder.Configuration);
builder.Services.ConfigureIdentity();

builder.Services.AddHttpClient(
        "WppHttpClient",
        (provider, client) =>
        {
            client.BaseAddress = new Uri(provider?.GetService<HttpClientSettings>()?.WhatsClientUrl ?? "");
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
       .ConfigureSwagger()
       .AddMvc(options => options.Filters.Add(new ErrorHandlerAttribute()));

builder.Host
       .ConfigureAutofac(builder.Configuration);

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

app.MapGroup("identity")
   .MapIdentityApi<MyUserIdenty>()
   .WithTags("Identity");

app
   //Store endpoints
   .StoresEndpoints()
   //Items endpoints
   .ItemsEndpoints()
   //Callback endpoints
   .CallbacksEndpoints();

app.Run();
