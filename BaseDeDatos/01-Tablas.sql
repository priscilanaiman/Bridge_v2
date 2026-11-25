CREATE DATABASE Bridge
GO

USE Bridge
GO

CREATE TABLE Usuarios (
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Contrasena VARCHAR(50) NOT NULL,
    FechaRegistro DATETIME NOT NULL
)
GO

CREATE TABLE AdultosMayores (
    IdAdultoMayor INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
    Apellido VARCHAR(50) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    CondicionSalud VARCHAR(500)
)
GO

CREATE TABLE Vinculos (
    IdVinculo INT IDENTITY(1,1) PRIMARY KEY,
    IdAdultoMayor INT NOT NULL REFERENCES AdultosMayores(IdAdultoMayor),
    IdUsuario INT NOT NULL REFERENCES Usuarios(IdUsuario),
    Rol VARCHAR(20) NOT NULL,
    EsResponsable BIT NOT NULL
)
GO
