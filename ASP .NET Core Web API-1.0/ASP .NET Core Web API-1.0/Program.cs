//using ASP_.NET_Core_Web_API_1._0.Class;
using ASP_.NET_Core_Web_API_1._0.Class;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.OpenApi.Models;
using JWTLibrary;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
var tokenOptions = builder.Configuration.GetSection(TokenOptions.DefaultAuthenticatorProvider).Get<TokenOptions>();
//var token = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]));
var token = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890abcdefghijklmnopqrstuvwxyz"));
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Description = "JWT Authorization header using the Bearer scheme."
        }
        );
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[]{"Bearer "}
        }
    });
});
builder.Services.AddAuthentication(options =>
{
    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddScheme<JwtBearerOptions, JwtBearerHandler>(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        var jwtBearerSettings = builder.Configuration.GetSection("JWTBearer").Get<JWTBearerSettings>();

        if (jwtBearerSettings == null)
        {
            throw new NullReferenceException();
        }

        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens
        .TokenValidationParameters()
        {
            ValidIssuer = jwtBearerSettings.Issuer,
            ValidAudience = jwtBearerSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtBearerSettings.SigningKey)
                ),
            ClockSkew = TimeSpan.Zero,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true
        };

    });

//builder.Services.AddAuthorization();
//builder.Services.AddAuthentication().AddJwtBearer();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme
////options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
////options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
////options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//).AddJwtBearer(JWT =>
//{
//    JWT.RequireHttpsMetadata = false;
//    JWT.SaveToken = true;

//    JWT.TokenValidationParameters = new TokenValidationParameters()
//    {
//        //ValidIssuer = tokenOptions.AuthenticatorIssuer,
//        //IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
//        //ValidIssuer = builder.Configuration["AppSettings:JWTIssuer"],
//        //ValidAudience = builder.Configuration["AppSettings:JWTAudience"],
//        //ValidateLifetime = true,
//        //ClockSkew = TimeSpan.Zero

//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567890abcdefghijklmnopqrstuvwxyz")),
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        RequireExpirationTime = false,
//    };
//});
//var securityScheme = new OpenApiSecurityScheme()
//{
//    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
//    Name = "Authorization",
//    In = ParameterLocation.Header,
//    Scheme = "Bearer",
//    //Type = SecuritySchemeType.Http,
//    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
//    BearerFormat = "JWT" // Optional
//};

//var securityRequirement = new OpenApiSecurityRequirement
//{
//    {
//        new OpenApiSecurityScheme
//        {
//            Reference = new OpenApiReference
//            {
//                Type = ReferenceType.SecurityScheme,
//                Id = "Bearer"
//            }
//        },
//        new string[] {}
//    }
//};
//builder.Services.AddSwaggerGen(options =>
//{
//    //options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
//    options.AddSecurityDefinition("bearer", securityScheme);
//    options.AddSecurityRequirement(securityRequirement);
//    //options.AddSecurityDefinition("Bearer,", 
//    //    new OpenApiSecurityScheme
//    //    {
//    //        Description = "JWT Authorization",
//    //        Name = "Authorization",        
//    //        In = ParameterLocation.Header,
//    //        Scheme = "Bearer",
//    //        Type = SecuritySchemeType.ApiKey,
//    //        BearerFormat = "JWT"
//    //    });
//    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    //{
//    //    {
//    //        new OpenApiSecurityScheme
//    //        {
//    //            Reference = new OpenApiReference
//    //            {
//    //                Type = ReferenceType.SecurityScheme,
//    //                Id = "Bearer"
//    //            }
//    //        },
//    //       new List<string>()
//    //    }
//    //});
//});
//builder.Services.AddAuthentication("Bearer").AddJwtBearer();

#region API Versioning
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
#endregion
builder.Services.AddOptions<JWTBearerSettings>()
    .Bind(builder.Configuration.GetSection("JWTBearer"))
    .ValidateDataAnnotations();
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
