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
    DECLARE @IdGrupo INT

    INSERT INTO AdultosMayores (Nombre, Apellido, FechaNacimiento, CondicionSalud)
    VALUES (@Nombre, @Apellido, @FechaNacimiento, @CondicionSalud)

    SELECT @IdAdultoMayor = MAX(IdAdultoMayor) FROM AdultosMayores

    INSERT INTO Grupos (IdAdultoMayor, IdUsuarioResponsable)
    VALUES (@IdAdultoMayor, @IdUsuario)

    SELECT @IdGrupo = MAX(IdGrupo) FROM Grupos

    INSERT INTO Miembros (IdGrupo, IdUsuario, Rol)
    VALUES (@IdGrupo, @IdUsuario, @Rol)

    SELECT @IdGrupo
END
GO

CREATE PROCEDURE ObtenerMisGrupos
    @IdUsuario INT
AS
BEGIN
    SELECT Grupos.IdGrupo, AdultosMayores.Nombre, AdultosMayores.Apellido, AdultosMayores.FechaNacimiento
    FROM Miembros
    INNER JOIN Grupos ON Miembros.IdGrupo = Grupos.IdGrupo
    INNER JOIN AdultosMayores ON Grupos.IdAdultoMayor = AdultosMayores.IdAdultoMayor
    WHERE Miembros.IdUsuario = @IdUsuario
    ORDER BY AdultosMayores.Nombre
END
GO

CREATE PROCEDURE ObtenerGrupo
    @IdGrupo INT,
    @IdUsuario INT
AS
BEGIN
    SELECT Grupos.IdGrupo, Grupos.IdUsuarioResponsable, AdultosMayores.Nombre, AdultosMayores.Apellido, AdultosMayores.FechaNacimiento, AdultosMayores.CondicionSalud
    FROM Miembros
    INNER JOIN Grupos ON Miembros.IdGrupo = Grupos.IdGrupo
    INNER JOIN AdultosMayores ON Grupos.IdAdultoMayor = AdultosMayores.IdAdultoMayor
    WHERE Miembros.IdUsuario = @IdUsuario AND Grupos.IdGrupo = @IdGrupo
END
GO

CREATE PROCEDURE ObtenerMiembrosDelGrupo
    @IdGrupo INT
AS
BEGIN
    SELECT Usuarios.Nombre, Usuarios.Apellido, Miembros.Rol
    FROM Miembros
    INNER JOIN Usuarios ON Miembros.IdUsuario = Usuarios.IdUsuario
    WHERE Miembros.IdGrupo = @IdGrupo
    ORDER BY Usuarios.Nombre
END
GO
