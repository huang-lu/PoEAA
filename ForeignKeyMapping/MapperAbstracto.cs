using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeyMapping
{
    abstract class MapperAbstracto
    {
        private Hashtable objetoCargado = new Hashtable();
        protected ObjetoDominio BuscarAbstracto(long id)
        {
            ObjetoDominio resultado = (ObjetoDominio)objetoCargado[id];
            if (resultado != null)
            {
                return resultado;
            }
            SqlCommand consulta = new SqlCommand(cadenaBuscar(), BD());
            consulta.Parameters.AddWithValue("@id", id);
            consulta.Connection.Open();
            SqlDataReader fila = consulta.ExecuteReader();
            fila.Read();
            resultado = Cargar(fila);
            return resultado;
        }

        abstract protected string cadenaBuscar();

        protected ObjetoDominio Cargar(SqlDataReader fila)
        {
            long id = Convert.ToInt64(fila[0]);
            if (objetoCargado.Contains(id))
            {
                return (ObjetoDominio)objetoCargado[id];
            }
            ObjetoDominio resultado = HacerCarga(id, fila);
            HacerRegistro(id, resultado);
            return resultado;
        }

        abstract protected ObjetoDominio HacerCarga(long id, SqlDataReader fila);

        protected void HacerRegistro(long id, ObjetoDominio objeto)
        {
            objetoCargado.Add(id, objetoCargado);
        }

        static public ArtistaMapper Artista()
        {
            return new ArtistaMapper();
        }

        private SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
