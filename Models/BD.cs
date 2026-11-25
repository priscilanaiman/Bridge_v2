using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;

namespace Bridge.Models;

public class BD
{
    private string _connectionString = @"Server=localhost\SQLEXPRESS;DataBase=Bridge;Integrated Security=True;TrustServerCertificate=True;";

    public bool RegistrarUsuario(string nombre, string apellido, string email, string contrasena)
    {
        int registrosAfectados = 0;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "RegistrarUsuario";
            registrosAfectados = connection.Execute(
                storedProcedure,
                new { Nombre = nombre, Apellido = apellido, Email = email, Contrasena = contrasena, FechaRegistro = DateTime.Now },
                commandType: CommandType.StoredProcedure
            );
        }
        return registrosAfectados > 0;
    }

    public Usuario? IniciarSesion(string email, string contrasena)
    {
        Usuario? usuario = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "IniciarSesion";
            usuario = connection.QueryFirstOrDefault<Usuario>(
                storedProcedure,
                new { Email = email, Contrasena = contrasena },
                commandType: CommandType.StoredProcedure
            );
        }
        return usuario;
    }
}
