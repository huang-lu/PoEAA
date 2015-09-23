using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableDataGateway
{
    class PersonaGateway
    {
        public SqlDataReader BuscarTodos()
        {
            string sql = "SELECT * FROM Persona";
            SqlCommand consulta = new SqlCommand(sql, conexionBD());
            consulta.Connection.Open();
            return consulta.ExecuteReader();
        }

        public SqlDataReader BuscarPorApellidos(string apellidos)
        {
            string sql = String.Format("SELECT * FROM Persona WHERE Apellidos LIKE '%{0}%'", apellidos);
            SqlCommand consulta = new SqlCommand(sql, conexionBD());
            consulta.Connection.Open();
            return consulta.ExecuteReader();
        }

        public SqlDataReader BuscarWhere(string clausulaWhere)
        {
            string sql = String.Format("SELECT * FROM Persona WHERE {0}", clausulaWhere);
            SqlCommand consulta = new SqlCommand(sql, conexionBD());
            consulta.Connection.Open();
            return consulta.ExecuteReader();
        }

        public Object[] BuscarFila(long key)
        {
            string sql = String.Format("SELECT * FROM Persona WHERE PersonaId = {0}", key);
            SqlCommand consulta = new SqlCommand(sql, conexionBD());
            consulta.Connection.Open();
            SqlDataReader fila = consulta.ExecuteReader();
            fila.Read();
            Object[] resultado = new Object[fila.FieldCount];
            fila.GetValues(resultado);
            fila.Close();
            return resultado;
        }

        public long Insertar(string nombre, string apellidos, int numeroDependientes)
        {
            string sql = "INSERT INTO Persona VALUES (@PersonaId, @Nombre, @Apellidos, @numeroDependientes)";
            long personaId = GetSiguientePersonaId();
            SqlCommand consulta = new SqlCommand(sql, conexionBD());
            consulta.Parameters.Add(new SqlParameter("@PersonaId", personaId));
            consulta.Parameters.Add(new SqlParameter("@Nombre", nombre));
            consulta.Parameters.Add(new SqlParameter("@Apellidos", apellidos));
            consulta.Parameters.Add(new SqlParameter("@numeroDependientes", numeroDependientes));
            consulta.Connection.Open();
            consulta.ExecuteNonQuery();
            return personaId;
        }

        public void Actualizar(long personaId, string nombre, string apellidos, int numeroDependientes)
        {
            string sql = @"UPDATE Persona 
                            SET Nombre = ?, Apellidos = ?, numeroDependientes = ?
                            WHERE PersonaId = ?";
            SqlCommand consulta = new SqlCommand(sql, conexionBD());
            consulta.Parameters.AddWithValue("PersonaId", personaId);
            consulta.Parameters.AddWithValue("Nombre", nombre);
            consulta.Parameters.AddWithValue("Apellidos", apellidos);
            consulta.Parameters.AddWithValue("numeroDependientes", numeroDependientes);
            consulta.ExecuteNonQuery();
        }

        public void Eliminar(long personaId)
        {
            string sql = "DELETE FROM Persona WHERE PersonaId = ?";
            SqlCommand consulta = new SqlCommand(sql, conexionBD());
            consulta.Parameters.AddWithValue("PersonaId", personaId);
            consulta.ExecuteNonQuery();
        }

        private long GetSiguientePersonaId()
        {
            string sql = "SELECT ISNULL(MAX(PersonaId), 0) FROM Persona";
            SqlCommand consulta = new SqlCommand(sql, conexionBD());
            consulta.Connection.Open();
            SqlDataReader fila = consulta.ExecuteReader();
            fila.Read();
            return Convert.ToInt64(fila[0])+1;
        }

        private static SqlConnection conexionBD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
