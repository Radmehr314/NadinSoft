using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NadinSoft.Api.Framework;
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
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "NadinSoft Api", Version = "v1" });

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
builder.Services.AddHttpClient();




// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

