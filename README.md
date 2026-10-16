# BadCleanArch (refactorizado a Clean Architecture)

API de pedidos en .NET 8 organizada por capas. La regla de dependencia apunta siempre hacia adentro:

```
WebApi ──► Application ──► Domain
   └─────► Infrastructure ─► Application
```

| Capa | Responsabilidad | Depende de |
|---|---|---|
| Domain | Entidad `Order` y reglas de negocio | nada |
| Application | Casos de uso y puertos (`IOrderWriter`, `IOrderReader`, `IAppLogger`) | Domain |
| Infrastructure | SQL Server (`SqlOrderRepository`) y logging (`ConsoleAppLogger`) | Application |
| WebApi | Controlador, contratos JSON y registro de dependencias | Application, Infrastructure |

## Ejecutar

1. Crear una base de datos en SQL Server y ejecutar `database/schema.sql`.
2. Guardar la cadena de conexión fuera del código:

```
dotnet user-secrets set "ConnectionStrings:Sql" "Server=localhost;Database=Pedidos;User Id=...;Password=...;TrustServerCertificate=True" --project src/WebApi
```

3. `dotnet run --project src/WebApi`

## Endpoints

- `GET /health`
- `POST /orders` con JSON `{ "customerName": "Ana", "productName": "Teclado", "quantity": 2, "unitPrice": 50000 }`
- `GET /orders/last?count=10`
