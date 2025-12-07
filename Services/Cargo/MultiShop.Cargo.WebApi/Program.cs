using Microsoft.EntityFrameworkCore;
using MultiShop.Cargo.DataAccessLayer.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CargoContext>(options =>
            options.UseSqlServer("Server = localhost,1441 ; initial Catalog = MultiShopCargoDb; User = sa; Password = Database2540!;TrustServerCertificate=True "));

builder.Services.AddControllers();
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
app.UseAuthorization();
app.MapControllers();
app.Run();