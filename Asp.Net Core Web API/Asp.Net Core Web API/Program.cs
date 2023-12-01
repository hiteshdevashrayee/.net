using Asp.Net_Core_Web_API.Interface;
using Asp.Net_Core_Web_API.Models;
using Asp.Net_Core_Web_API.Utility;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
//builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();
//builder.Services.AddScoped<IMessage, Message>();
//builder.Services.AddOptions<DbConnection>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGet("Post", () => "Update Data");
app.Run();
