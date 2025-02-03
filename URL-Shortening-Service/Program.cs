using Microsoft.EntityFrameworkCore;
using URL_Shortening_Service.Context;
using URL_Shortening_Service.Context.respositories;
using URL_Shortening_Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Obtener credenciales de las variables de entorno
var userId = Environment.GetEnvironmentVariable("DB_USER_ID");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") +
                       $"User Id={userId};Password={password}";

// inyectar dependencias
builder.Services.AddScoped<ShortUrlRepository>();
builder.Services.AddScoped<ShortUrlService>();

// Add services to the container.
// configuracion de entity framework
builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// cors policy * para permitir cualquier origen
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//app.UseRouting();



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
