using Microsoft.EntityFrameworkCore;
using ProductMS.Business;
using ProductMS.Data;
using ProductMS.Data.Service;
using ProductMS.DTO.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

string? connectionStrings = builder.Configuration.GetConnectionString("ProductDb");
builder.Services.AddDbContext<DbContext, ProductMSContext>(options => options.UseSqlServer(connectionStrings));


builder.Services.AddEntities();
builder.Services.AddDataServices();
builder.Services.AddDTOMappers();
builder.Services.AddServices();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ProductMSContext>();

    // This applies any pending migrations and creates the database if it doesn't exist
    context.Database.Migrate();
}

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
