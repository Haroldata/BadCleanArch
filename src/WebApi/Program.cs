using Application.Abstractions;
using Application.Orders;
using Infrastructure.Logging;
using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("Sql")
    ?? throw new InvalidOperationException(
        "Falta la cadena de conexión 'ConnectionStrings:Sql'. Configúrela con user-secrets o variable de entorno.");

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? [];

builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options => options.AddPolicy("Default", policy => policy
    .WithOrigins(allowedOrigins)
    .WithMethods("GET", "POST")
    .WithHeaders("Content-Type")));

// Inversión de dependencias: cada puerto (interfaz) se enlaza con su implementación concreta.
builder.Services.AddSingleton<IAppLogger, ConsoleAppLogger>();
builder.Services.AddSingleton(new SqlOrderRepository(connectionString));
builder.Services.AddSingleton<IOrderWriter>(sp => sp.GetRequiredService<SqlOrderRepository>());
builder.Services.AddSingleton<IOrderReader>(sp => sp.GetRequiredService<SqlOrderRepository>());

builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddScoped<GetLastOrdersUseCase>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseCors("Default");

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));
app.MapControllers();

await app.RunAsync();
