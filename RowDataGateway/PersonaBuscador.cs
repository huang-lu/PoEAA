using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowDataGateway
{
    class PersonaBuscador
    {
        private static string cadenaBusqueda = "SELECT * FROM Persona WHERE PersonaId = @personaId";
        private static string cadenaBusquedaDependientes = @"SELECT * FROM Persona
                                                             WHERE numeroDependientes > 0";
        
        public PersonaGateway Buscar(long personaId)
        {
            SqlCommand consulta = new SqlCommand(cadenaBusqueda, BD());
            consulta.Parameters.AddWithValue("@personaId", personaId);
            consulta.Connection.Open();
            SqlDataReader fila = consulta.ExecuteReader();
            fila.Read();
            return PersonaGateway.Load(fila);
        }

        public List<PersonaGateway> BuscarResponsables()
        {
            SqlCommand consulta = new SqlCommand(cadenaBusquedaDependientes, BD());
            consulta.Connection.Open();
            SqlDataReader filas = consulta.ExecuteReader();

            List<PersonaGateway> resultado = new List<PersonaGateway>();
            while (filas.Read())
            {
                resultado.Add(PersonaGateway.Load(filas));
            }

            return resultado;
        }

        private SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
