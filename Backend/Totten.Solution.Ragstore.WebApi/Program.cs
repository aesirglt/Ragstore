using Coravel;
using Coravel.Invocable;
using Microsoft.AspNetCore.OData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Totten.Solution.Ragstore.ApplicationService.Services;
using Totten.Solution.Ragstore.Infra.Data.Contexts.EntityFrameworkIdentity;
using Totten.Solution.Ragstore.WebApi.AppSettings;
using Totten.Solution.Ragstore.WebApi.Endpoints;
using Totten.Solution.Ragstore.WebApi.ServicesExtension;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScheduler();

builder.Services.ConfigureAppSettingsClass(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.AddAntiforgery();
builder.Services.AddProblemDetails().AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddHttpClient(
        "WppHttpClient",
        (provider, client) =>
        {
            client.BaseAddress = new Uri(provider?.GetService<HttpClientSettings>()?.UrlApiWPP ?? "");
            client.DefaultRequestHeaders.Add("apiKey", "guirono44o5xb5i8neynzj");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("RagnaStoreApi");
        });

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(op =>
    {
        op.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services
       .AddControllers(opt =>
       {
           opt.AddSwaggerMediaTypes();
       })
       .AddOData(opt => opt.Filter().Expand().Select().OrderBy().SetMaxTop(30).Count())
       .AddNewtonsoftJson(op =>
       {
           op.SerializerSettings.Formatting = Formatting.Indented;
           op.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
           op.SerializerSettings.Converters.Add(new StringEnumConverter());
           op.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
       });

builder.Services
       .AddEndpointsApiExplorer()
       .ConfigureSwagger();

builder.Host
       .ConfigureAutofac(builder.Configuration);

var app = builder.Build();
app.Services.UseScheduler(sch =>
{
    sch.Schedule<ScheduleCallbackService>().Cron("*/15 * * * *");
});
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "Titulo";
    });
}
app.UseODataQueryRequest();
app.MapGroup("identity")
   .MapIdentityApi<MyUserIdenty>()
   .WithTags("Identity");

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
