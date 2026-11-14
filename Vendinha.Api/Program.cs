using Microsoft.EntityFrameworkCore;
using Vendinha.Contexts;
using Vendinha.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "http://127.0.0.1:5173",
                "http://localhost:4173",
                "http://127.0.0.1:4173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// configurando os serviços da aplicação
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<DividaService>();

var connstring = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<VendinhaDbContext>(o => o.UseNpgsql(connstring));

var app = builder.Build();
// PIPELINES
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("Frontend");
app.MapControllers();

app.Run();
