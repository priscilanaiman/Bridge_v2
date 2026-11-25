USE Bridge
GO

EXEC RegistrarUsuario 'Usuario', 'De Prueba', 'prueba@bridge.com', '1234', '2026-11-24'
GO

EXEC RegistrarUsuario 'Laura', 'Fernández', 'prueba2@bridge.com', '1234', '2026-11-24'
GO

DECLARE @IdUsuario INT
SELECT @IdUsuario = IdUsuario FROM Usuarios WHERE Email = 'prueba2@bridge.com'
EXEC CrearGrupo @IdUsuario, 'Rosa', 'Benítez', '1944-03-12', 'Alzheimer leve e hipertensión', 'Hijo/a'
GO
