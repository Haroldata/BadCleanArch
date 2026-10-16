-- Esquema mínimo para ejecutar la API (SQL Server).
-- Crea primero una base de datos propia (no uses "master") y ejecútalo allí.
CREATE TABLE Orders (
    Id           UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Customer     NVARCHAR(200)    NOT NULL,
    Product      NVARCHAR(200)    NOT NULL,
    Qty          INT              NOT NULL,
    Price        DECIMAL(18, 2)   NOT NULL,
    CreatedAtUtc DATETIME2        NOT NULL
);
