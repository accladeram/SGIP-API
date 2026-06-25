\# SGIP - Sistema de Gestión de Inversiones y Préstamos



API REST para simulación y gestión de préstamos con cálculos financieros, cronogramas de pago y procesamiento de transacciones con garantía de idempotencia.



Swagger: https://sgip-api-production-0877.up.railway.app/swagger/index.html



No requiere autenticación. Usar `userId` de prueba: `user-001` o `user-002`.



\---



\## Tecnologías



\- .NET 10 / ASP.NET Core Web API

\- Entity Framework Core + Npgsql

\- PostgreSQL (Railway)

\- Swagger / OpenAPI



\---



\## Arquitectura



```

src/

├── SGIP.API/              # Controllers, DTOs, configuración

├── SGIP.Application/      # Servicios, interfaces, orquestación

├── SGIP.Domain/           # Entidades, enums, lógica de negocio pura

└── SGIP.Infrastructure/   # EF Core, repositorios, migraciones

```



Las dependencias fluyen hacia adentro: `API → Application → Domain`. `Infrastructure` implementa interfaces definidas en `Application`.



\*\*Patrones aplicados\*\*



\- Repository + Unit of Work: abstrae el acceso a datos y garantiza que operaciones relacionadas (ej: aprobar préstamo + crear transacción de desembolso) sean atómicas.

\- Strategy: separa los algoritmos de cálculo de cuotas. `FrenchLoanStrategy` (sistema francés, cuota fija) y `GermanLoanStrategy` (sistema alemán, cuota decreciente) son intercambiables sin modificar el servicio.



\*\*Lógica financiera\*\*



```

TEM  = (1 + TEA)^(1/12) - 1



// Sistema francés (cuota fija)

Cuota = Monto × \[TEM × (1+TEM)^n] / \[(1+TEM)^n - 1]



// Sistema alemán (cuota decreciente)

Amortización = Monto / n

Interés      = Saldo × TEM

```



TEA base: 24%. Monto: $500–$50,000. Plazo: 6–60 meses.



\---



\## Correr localmente



\*\*Prerrequisitos:\*\* .NET 10 SDK, PostgreSQL 14+



```bash

git clone https://github.com/accladeram/SGIP-API.git

cd SGIP-API/src/SGIP.API

```



La cadena de conexión se guarda con User Secrets (no se sube al repo):



```bash

dotnet user-secrets init

dotnet user-secrets set "ConnectionStrings:DefaultConnection" \\

&#x20; "Host=localhost;Port=5432;Database=sgip\_db;Username=tu\_usuario;Password=tu\_password"

```



```bash

dotnet restore



\# Aplicar migraciones

dotnet ef database update --project ../SGIP.Infrastructure --startup-project .



\# Levantar en puerto 8080

dotnet run --urls "http://localhost:8080"

```



Swagger en: `http://localhost:8080/swagger`



\*\*Tests\*\*



```bash

cd src/SGIP.Tests

dotnet test

```



Cubre: cálculo de cuota fija, generación de cronograma, validaciones de monto/plazo y deduplicación por `IdempotencyKey`.

