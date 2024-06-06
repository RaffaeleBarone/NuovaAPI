using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NuovaAPI.AutoMapper;
using NuovaAPI.Commons.DTO;
using NuovaAPI.Commons.Validators;
using NuovaAPI.DataLayer.Entities;
using NuovaAPI.DataLayer.Extensions;
using NuovaAPI.Extensions;
using NuovaAPI.Validators;
using NuovaAPI.Worker_Services;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextServices(builder.Configuration);
builder.Services.AddCustomServices();
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

app.Run();
