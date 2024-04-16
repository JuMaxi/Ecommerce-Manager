using EcommerceManager.API.Mappers;
using EcommerceManager.Db;
using EcommerceManager.DbAccess;
using EcommerceManager.Domain.Interfaces;
using EcommerceManager.Domain.Services;
using EcommerceManager.Domain.Validators;
using EcommerceManager.Infra.DbAccess;
using EcommerceManager.Interfaces;
using EcommerceManager.Mappers;
using EcommerceManager.Services;
using EcommerceManager.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryDbAccess, CategoryDbAccess>();
builder.Services.AddTransient<IValidateCategory, ValidateCategory>();
builder.Services.AddTransient<ICategoryMapper, CategoryMapper>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<IBrandDbAccess, BrandDbAccess>();
builder.Services.AddTransient<IBrandMapper, BrandMapper>();
builder.Services.AddTransient<IValidateBrand, ValidateBrand>();
builder.Services.AddTransient<IProductMapper, ProductMapper>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductDbAccess, ProductDbAccess>();
builder.Services.AddTransient<IValidateProduct, ValidateProduct>();

string connectionString = builder.Configuration.GetValue<string>("ConnectionStringDBContext");
builder.Services.AddDbContext<EcommerceManagerDbContext>(DB => DB.UseSqlServer(connectionString));

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
