using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registry
{
    class Persona
    {
        private long personaId;
        private string nombre;
        private string apellidos;
        private int numeroDependientes;

        private static string cadenaBusqueda = "SELECT * FROM Persona WHERE PersonaId = @id";
        private static string cadenaActualizar = @"UPDATE Persona
                                                   SET Nombre = @nombre,
                                                       Apellidos = @apellidos,
                                                       numeroDependientes = @numDep
                                                   WHERE PersonaId = @id";
        private static string cadenaInsertar = @"INSERT INTO Persona 
                                                VALUES (@id, @nombre, @apellidos, @numDep, null)";

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

        public Persona(long personaId, string nombre, string apellidos, int numeroDependientes)
        {
            this.PersonaId = personaId;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.NumeroDependientes = numeroDependientes;
        }

        public static Persona Buscar(long id)
        {
            Persona resultado = Registry.getPersona(id);
            if (resultado != null)
            {
                return resultado;
            }
            try
            {
                SqlCommand consulta = new SqlCommand(cadenaBusqueda, BD());
                consulta.Parameters.AddWithValue("@id", id);
                consulta.Connection.Open();
                SqlDataReader fila = consulta.ExecuteReader();
                fila.Read();
                resultado = Load(fila);
                return resultado;
            }
            catch (SqlException)
            {
                throw new ApplicationException(String.Format("Error al buscar la persona {0}.", id));
            }
        }

        public static Persona Load(SqlDataReader fila)
        {
            long personaId = Convert.ToInt64(fila["PersonaId"]);
            Persona resultado = Registry.getPersona(personaId);
            if (resultado != null)
            {
                return resultado;
            }
            string nombre = Convert.ToString(fila["Nombre"]);
            string apellidos = Convert.ToString(fila["Apellidos"]);
            int numeroDependientes = Convert.ToInt32(fila["numeroDependientes"]);
            resultado = new Persona(personaId, nombre, apellidos, numeroDependientes);
            Registry.anadirPersona(resultado);
            return resultado;
        }

        public void Actualizar()
        {
            try
            {
                SqlCommand consulta = new SqlCommand(cadenaActualizar, BD());
                consulta.Parameters.AddWithValue("@nombre", Nombre);
                consulta.Parameters.AddWithValue("@apellidos", Apellidos);
                consulta.Parameters.AddWithValue("@numDep", NumeroDependientes);
                consulta.Parameters.AddWithValue("@id", PersonaId);
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw new ApplicationException(String.Format("Imposible actualizar la persona {0}.", PersonaId));
            }
        }

        public void Insertar()
        {
            try
            {
                SqlCommand consulta = new SqlCommand(cadenaInsertar, BD());
                consulta.Parameters.AddWithValue("@nombre", Nombre);
                consulta.Parameters.AddWithValue("@apellidos", Apellidos);
                consulta.Parameters.AddWithValue("@numDep", NumeroDependientes);
                consulta.Parameters.AddWithValue("@id", PersonaId);
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
                Registry.anadirPersona(this);
            }
            catch (SqlException e)
            {
                throw new ApplicationException(String.Format("Imposible añadir la persona {0}\n{1}.", personaId, e));
            }
        }

        public decimal getDesgravacion()
        {
            decimal desgravacionBase = new decimal(1500);
            decimal desgravacionDependencia = new decimal(750);
            return desgravacionBase + desgravacionDependencia * NumeroDependientes;
        }

        private static SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
