using Newtonsoft.Json;
using Totten.Solution.Ragstore.Infra.Cross.Errors;
using Totten.Solution.Ragstore.Infra.Data.Contexts.EntityFrameworkIdentity;
using Totten.Solution.Ragstore.WebApi.AppSettings;
using Totten.Solution.Ragstore.WebApi.Endpoints;
using Totten.Solution.Ragstore.WebApi.ServicesExtension;
using JsonConvert = Newtonsoft.Json.JsonConvert;
var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppSettingsClass(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.AddAntiforgery();
builder.Services
       .AddProblemDetails()
       .AddExceptionHandler<GlobalExceptionHandler>();

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
           op.SerializerSettings.Formatting = Formatting.Indented;
           op.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
       });

builder.Services
       .ConfigureSwagger();

builder.Host
       .ConfigureAutofac(builder.Configuration);

var app = builder.Build();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
//app.UseStatusCodePages();
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
   .StoresItemsEndpoints()
   //Items endpoints
   .ItemsEndpoints()
   //Callback endpoints
   .CallbacksEndpoints();

app.Run();
