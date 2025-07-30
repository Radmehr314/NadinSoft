using System.Net;
using System.Text;
using System.Text.Json;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NadinSoft.Api.Framework;
using NadinSoft.Application.Contract.Exceptions;
using NadinSoft.Infrastructure.Config;
using NadinSoft.Infrastructure.Persistance.SQl;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers(a => { a.Conventions.Add(new CqrsModelConvention()); });

builder.Services.AddControllers();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacModule(builder.Configuration.GetConnectionString("DbConnection"))));
Host.CreateDefaultBuilder(args).UseServiceProviderFactory(new AutofacServiceProviderFactory());
var autofac = new ContainerBuilder();
autofac.RegisterModule(new AutofacModule(builder.Configuration.GetConnectionString("DbConnection")));


//sql
builder.Services.AddDbContextPool<DataBaseContext>(c => c.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
//end sql

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = 1024 * 1024 * 100; // 100 MB
    options.MultipartBodyLengthLimit = 1024 * 1024 * 100; // 100MB
});
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = builder.Configuration.GetValue<long>("Kestrel:Limits:MaxRequestBodySize");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "NadinSoft API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7055/",
            ValidAudience = "https://localhost:5232/",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("139199f8-ae6f-447e-9f1a-3cabc187e8ee")),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    try
    {
        DatabaseInitializer.Initialize(app.Services);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "یک خطا در ایجاد یا مایگریت دیتابیس رخ داد.");
    }
}
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        // تنظیم پیش‌فرض
        var errorCode = "ERR_500";
        var errorMessage = "یک خطای ناشناس رخ داده ، با پشتیبانی تماس بگیرید!!!";
        var statusCode = HttpStatusCode.InternalServerError;

        // مدیریت خطاهای خاص
        if (exception is NotFoundException)
        {
            errorCode = "ERR_404";
            errorMessage = exception.Message;
            statusCode = HttpStatusCode.NotFound;
        }
        else if (exception is UserAccessException)
        {
            errorCode = "ERR_403";
            errorMessage = exception.Message;
            statusCode = HttpStatusCode.Forbidden;
        }
        else if (exception is ValidationException validationException)
        {
            errorCode = "ERR_400";
            errorMessage = string.Join(", ", validationException.Errors);
            statusCode = HttpStatusCode.BadRequest;
        }
        
        // تنظیم کد وضعیت HTTP
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        // ارسال پاسخ به کلاینت
        await context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            ErrorCode = errorCode,
            ErrorMessage = errorMessage
        }));
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

