IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Bridge')
BEGIN
    CREATE DATABASE Bridge
END
GO

USE Bridge
GO

IF OBJECT_ID('Usuarios') IS NULL
BEGIN
    CREATE TABLE Usuarios (
        IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
        Nombre VARCHAR(50) NOT NULL,
        Apellido VARCHAR(50) NOT NULL,
        Email VARCHAR(100) NOT NULL UNIQUE,
        Contrasena VARCHAR(50) NOT NULL,
        FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
    )
END
GO
