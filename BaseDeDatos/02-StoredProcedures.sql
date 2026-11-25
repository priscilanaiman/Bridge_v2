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
