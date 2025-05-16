using Microsoft.AspNetCore.OData;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.EntityFrameworkIdentity;
using Totten.Solution.RagnaComercio.WebApi.Endpoints;
using Totten.Solution.RagnaComercio.WebApi.ServicesExtension;
var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppSettingsClass(builder.Configuration);
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.AddAntiforgery();
builder.Services.AddProblemDetails().AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowFrontend",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
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
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DocumentTitle = "RagnaStore API DOC";
    });
}
app.UseODataQueryRequest();
//app.MapGroup("identity")
//   .MapIdentityApi<MyUserIdenty>()
//   .WithTags("Identity");

//app.UseHttpsRedirection();
app.MapControllers();

app.Run();
