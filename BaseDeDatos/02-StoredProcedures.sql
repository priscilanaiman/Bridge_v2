USE Bridge
GO

CREATE PROCEDURE RegistrarUsuario
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @Email VARCHAR(100),
    @Contrasena VARCHAR(50),
    @FechaRegistro DATETIME
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Usuarios WHERE Email = @Email)
    BEGIN
        INSERT INTO Usuarios (Nombre, Apellido, Email, Contrasena, FechaRegistro)
        VALUES (@Nombre, @Apellido, @Email, @Contrasena, @FechaRegistro)
    END
END
GO

CREATE PROCEDURE IniciarSesion
    @Email VARCHAR(100),
    @Contrasena VARCHAR(50)
AS
BEGIN
    SELECT IdUsuario, Nombre, Apellido, Email
    FROM Usuarios
    WHERE Email = @Email AND Contrasena = @Contrasena
END
GO

CREATE PROCEDURE CrearGrupo
    @IdUsuario INT,
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @FechaNacimiento DATE,
    @CondicionSalud VARCHAR(500),
    @Rol VARCHAR(20)
AS
BEGIN
    DECLARE @IdAdultoMayor INT

    INSERT INTO AdultosMayores (Nombre, Apellido, FechaNacimiento, CondicionSalud)
    VALUES (@Nombre, @Apellido, @FechaNacimiento, @CondicionSalud)

    SELECT @IdAdultoMayor = MAX(IdAdultoMayor) FROM AdultosMayores

    INSERT INTO Vinculos (IdAdultoMayor, IdUsuario, Rol, EsResponsable)
    VALUES (@IdAdultoMayor, @IdUsuario, @Rol, 1)

    SELECT @IdAdultoMayor
END
GO

CREATE PROCEDURE ObtenerMisGrupos
    @IdUsuario INT
AS
BEGIN
    SELECT AdultosMayores.IdAdultoMayor, AdultosMayores.Nombre, AdultosMayores.Apellido, AdultosMayores.FechaNacimiento, Vinculos.Rol, Vinculos.EsResponsable
    FROM Vinculos
    INNER JOIN AdultosMayores ON Vinculos.IdAdultoMayor = AdultosMayores.IdAdultoMayor
    WHERE Vinculos.IdUsuario = @IdUsuario
    ORDER BY AdultosMayores.Nombre
END
GO

CREATE PROCEDURE ObtenerGrupo
    @IdAdultoMayor INT,
    @IdUsuario INT
AS
BEGIN
    SELECT AdultosMayores.IdAdultoMayor, AdultosMayores.Nombre, AdultosMayores.Apellido, AdultosMayores.FechaNacimiento, AdultosMayores.CondicionSalud, Vinculos.Rol, Vinculos.EsResponsable
    FROM Vinculos
    INNER JOIN AdultosMayores ON Vinculos.IdAdultoMayor = AdultosMayores.IdAdultoMayor
    WHERE Vinculos.IdUsuario = @IdUsuario AND AdultosMayores.IdAdultoMayor = @IdAdultoMayor
END
GO

CREATE PROCEDURE ObtenerMiembrosDelGrupo
    @IdAdultoMayor INT
AS
BEGIN
    SELECT Usuarios.Nombre, Usuarios.Apellido, Vinculos.Rol, Vinculos.EsResponsable
    FROM Vinculos
    INNER JOIN Usuarios ON Vinculos.IdUsuario = Usuarios.IdUsuario
    WHERE Vinculos.IdAdultoMayor = @IdAdultoMayor
    ORDER BY Vinculos.EsResponsable DESC, Usuarios.Nombre
END
GO
