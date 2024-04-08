using EcommerceManager.Db;
using EcommerceManager.DbAccess;
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
