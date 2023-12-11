//using ASP_.NET_Core_Web_API_1._0.Class;
using ASP_.NET_Core_Web_API_1._0.Class;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();
builder.Services.AddApiVersioning(setup =>
{
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.ReportApiVersions = true;
    setup.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("api-version"),
        new QueryStringApiVersionReader("api-version"),
        new MediaTypeApiVersionReader("version"));

});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true; 
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant()
                    );
        }
        //options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
    });
    app.UseDeveloperExceptionPage();
    //ApiVersioningExtensions.UseSwaggerUI(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
