using Microsoft.EntityFrameworkCore;
using System;
using WebVanChuyen.Api.Data;
using WebVanChuyen.Api.Repositorys;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Using SQL Server
builder.Services.AddDbContext<DataContextWeb>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

// Add Repository
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPackageRespository, PackageRespository>();
builder.Services.AddScoped<IProductRespository, ProductRespository>();
builder.Services.AddScoped<IShipmentTripResposity, ShipmentResposity>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IRoutePathRepository, RoutePathRepository>();

var app = builder.Build();

// Seeding Route
//var services = app.Services.CreateScope().ServiceProvider;
//await DataContextWebSeedRoute.Initialize(services);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
