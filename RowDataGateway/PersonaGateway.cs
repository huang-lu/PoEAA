using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RowDataGateway
{
    class PersonaGateway
    {
        private long personaId;
        private string nombre;
        private string apellidos;
        private int numeroDependientes;

        private static string cadenaActualizar = @"UPDATE Persona
                                                   SET Nombre = @nombre,
                                                       Apellidos = @apellidos,
                                                       numeroDependientes = @numDependientes
                                                    WHERE PersonaId = @personaId";
        private static string cadenaInsertar = @"INSERT INTO Persona
                                                 VALUES (@personaId, 
                                                         @nombre, 
                                                         @apellidos,
                                                         @numeroDependientes)";

        private PersonaGateway(long personaId, string nombre, string apellidos, int numDependientes)
        {
            this.personaId = personaId;
            this.nombre = nombre;
            this.apellidos = apellidos;
            numeroDependientes = numDependientes;
        }

        public PersonaGateway(string nombre, string apellidos, int numDependientes)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.numeroDependientes = numDependientes;
        }

        public static PersonaGateway Load(SqlDataReader fila)
        {
            long personaId = Convert.ToInt64(fila["PersonaId"]);
            string nombre = Convert.ToString(fila["Nombre"]);
            string apellidos = Convert.ToString(fila["Apellidos"]);
            int numDependientes = Convert.ToInt32(fila["numeroDependientes"]);
            return new PersonaGateway(personaId, nombre, apellidos, numDependientes);
        }

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Apellidos
        {
            get
            {
                return apellidos;
            }

            set
            {
                apellidos = value;
            }
        }

        public int NumeroDependientes
        {
            get
            {
                return numeroDependientes;
            }

            set
            {
                numeroDependientes = value;
            }
        }

        public long PersonaId
        {
            get
            {
                return personaId;
            }

            set
            {
                personaId = value;
            }
        }

        public void Actualizar()
        {
            SqlCommand consulta = new SqlCommand(cadenaActualizar, BD());
            consulta.Parameters.AddWithValue("@nombre", nombre);
            consulta.Parameters.AddWithValue("@apellidos", apellidos);
            consulta.Parameters.AddWithValue("@numDependientes", numeroDependientes);
            consulta.Parameters.AddWithValue("@personaId", GetSiguientePersonaId());
            consulta.Connection.Open();
            consulta.ExecuteNonQuery();
        }

        public long Insertar()
        {
            SqlCommand consulta = new SqlCommand(cadenaInsertar, BD());
            long personaId = GetSiguientePersonaId();
            consulta.Parameters.AddWithValue("@nombre", nombre);
            consulta.Parameters.AddWithValue("@apellidos", apellidos);
            consulta.Parameters.AddWithValue("@numeroDependientes", numeroDependientes);
            consulta.Parameters.AddWithValue("@personaId", personaId);
            consulta.Connection.Open();
            consulta.ExecuteNonQuery();
            return personaId;
        }

        private long GetSiguientePersonaId()
        {
            string sql = "SELECT ISNULL(MAX(PersonaId), 0) FROM Persona";
            SqlCommand consulta = new SqlCommand(sql, BD());
            consulta.Connection.Open();
            SqlDataReader fila = consulta.ExecuteReader();
            fila.Read();
            return Convert.ToInt64(fila[0]) + 1;
        }

        private SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
