using Microsoft.EntityFrameworkCore;
using ModelContextProtocol.AspNetCore;
using Vendinha.Contexts;
using Vendinha.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMcpServer()
    .WithHttpTransport(op =>
    {
        op.SessionMode = HttpServerSessionMode.Stateless;
    })
    .WithToolsFromAssembly();

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<DividaService>();

var connstring = builder.Configuration.GetConnectionString("Postgres");
builder.Services.AddDbContext<VendinhaDbContext>(o => o.UseNpgsql(connstring));

var app = builder.Build();

app.MapMcp("/mcp");

app.Run();
