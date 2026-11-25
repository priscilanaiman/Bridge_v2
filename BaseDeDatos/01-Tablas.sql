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

CREATE TABLE Grupos (
    IdGrupo INT IDENTITY(1,1) PRIMARY KEY,
    IdAdultoMayor INT NOT NULL REFERENCES AdultosMayores(IdAdultoMayor),
    IdUsuarioResponsable INT NOT NULL REFERENCES Usuarios(IdUsuario)
)
GO

CREATE TABLE Miembros (
    IdMiembro INT IDENTITY(1,1) PRIMARY KEY,
    IdGrupo INT NOT NULL REFERENCES Grupos(IdGrupo),
    IdUsuario INT NOT NULL REFERENCES Usuarios(IdUsuario),
    Rol VARCHAR(20) NOT NULL
)
GO
