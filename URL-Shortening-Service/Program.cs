using Microsoft.EntityFrameworkCore;
using URL_Shortening_Service.Context;
using URL_Shortening_Service.Context.respositories;
using URL_Shortening_Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Get connection string
var connectionString = ConnectionStringProvider.GetConnectionString(builder.Configuration);

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
