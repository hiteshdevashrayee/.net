using Asp.Net_Core_Web_API.Controllers;
using Asp.Net_Core_Web_API.Interface;
using Asp.Net_Core_Web_API.Models;
using Asp.Net_Core_Web_API.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IApplication, Application>();
builder.Services.AddScoped<IUsers, Users>();
builder.Services.AddTransient<IRequest, Request>();
builder.Services.AddTransient<IProduct, Products>();
builder.Services.AddTransient<RequestService, RequestService>();
builder.Services.AddScoped<IDbContext, DbContext>();
builder.Services.AddApiVersioning(apiVersioningOptions =>
{
    apiVersioningOptions.ApiVersionReader = new HeaderApiVersionReader(new string[] { "api-version" }); // It means version will be define in header.and header name would be "api-version".
    apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
    var apiVersion = new Version(Convert.ToString("1.0"));
    apiVersioningOptions.DefaultApiVersion = new ApiVersion(apiVersion.Major, apiVersion.Minor);
    apiVersioningOptions.ReportApiVersions = true;
    apiVersioningOptions.UseApiBehavior = true; // It means include only api controller not mvc controller.
    apiVersioningOptions.Conventions.Controller<ProductV3Controller>().HasApiVersion(apiVersioningOptions.DefaultApiVersion);
    apiVersioningOptions.Conventions.Controller<ProductV4Controller>().HasApiVersion(apiVersioningOptions.DefaultApiVersion);
    apiVersioningOptions.ApiVersionSelector = new CurrentImplementationApiVersionSelector(apiVersioningOptions);

});
//builder.Services.AddVersionedApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();
//builder.Services.AddScoped<IMessage, Message>();
//builder.Services.AddOptions<DbConnection>();

builder.Services.AddAuthentication().AddJwtBearer(jwt=>{
    jwt.RequireHttpsMetadata = false;
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("1234567890abcdefghijklmnopqrstuvwxyz")),
        ValidateIssuer = false,
        ValidateAudience = false,
        RequireExpirationTime = false,
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseApiVersioning();
app.MapControllers();
app.MapGet("Post", () => "Update Data");
app.Run();
