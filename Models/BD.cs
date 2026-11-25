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

    public int CrearGrupo(int idUsuario, string nombre, string apellido, DateTime fechaNacimiento, string? condicionSalud, string rol)
    {
        int idGrupo = 0;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "CrearGrupo";
            idGrupo = connection.QueryFirstOrDefault<int>(
                storedProcedure,
                new { IdUsuario = idUsuario, Nombre = nombre, Apellido = apellido, FechaNacimiento = fechaNacimiento, CondicionSalud = condicionSalud, Rol = rol },
                commandType: CommandType.StoredProcedure
            );
        }
        return idGrupo;
    }

    public List<Grupo> ObtenerMisGrupos(int idUsuario)
    {
        List<Grupo> grupos = new List<Grupo>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "ObtenerMisGrupos";
            grupos = connection.Query<Grupo>(
                storedProcedure,
                new { IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure
            ).ToList();
        }
        return grupos;
    }

    public Grupo? ObtenerGrupo(int idGrupo, int idUsuario)
    {
        Grupo? grupo = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "ObtenerGrupo";
            grupo = connection.QueryFirstOrDefault<Grupo>(
                storedProcedure,
                new { IdGrupo = idGrupo, IdUsuario = idUsuario },
                commandType: CommandType.StoredProcedure
            );
        }
        return grupo;
    }

    public List<Miembro> ObtenerMiembrosDelGrupo(int idGrupo)
    {
        List<Miembro> miembros = new List<Miembro>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string storedProcedure = "ObtenerMiembrosDelGrupo";
            miembros = connection.Query<Miembro>(
                storedProcedure,
                new { IdGrupo = idGrupo },
                commandType: CommandType.StoredProcedure
            ).ToList();
        }
        return miembros;
    }
}
