using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    abstract class RegistroMapper
    {
        protected Hashtable objetosCargados = new Hashtable();

        abstract protected string cadenaBuscar();
        abstract protected string cadenaActualizar();
        abstract protected string cadenaInsertar();
        abstract protected string cadenaEliminar();

        public static RegistroMapper getMapper(Type clase)
        {
            if (clase == typeof(Album))
            {
                return new AlbumMapper();
            }
            else
            {
                return null;
            }
        }

        abstract public ObjetoDominio buscar(int id);

        protected ObjetoDominio buscarAbstracto(int id)
        {
            ObjetoDominio resultado = (ObjetoDominio)objetosCargados[id];
            if (resultado != null)
            {
                return resultado;
            }
            SqlCommand consulta = new SqlCommand(cadenaBuscar(), BD());
            consulta.Parameters.AddWithValue("@id", id);
            consulta.Connection.Open();
            SqlDataReader fila = consulta.ExecuteReader();
            fila.Read();
            resultado = cargar(fila);
            return resultado;
        }

        protected ObjetoDominio cargar(SqlDataReader fila)
        {
            int id = fila.GetInt32(0);
            if (objetosCargados.ContainsKey(id))
            {
                return (ObjetoDominio)objetosCargados[id];
            }
            ObjetoDominio resultado = hacerCarga(id, fila);
            objetosCargados.Add(id, resultado);
            return resultado;
        }

        abstract protected ObjetoDominio hacerCarga(int id, SqlDataReader fila);

        abstract public void actualizar(ObjetoDominio objeto);

        protected void actualizarAbstracto(ObjetoDominio objeto)
        {
            try
            {
                SqlCommand consulta = new SqlCommand(cadenaActualizar(), BD());
                agregarParametros(consulta, objeto);
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
                consulta.Connection.Close();
            }
            catch (SqlException)
            {
                Console.WriteLine("Hubo un error al actualizar el registro {0}", objeto.getId());
            }
        }

        abstract protected void agregarParametros(SqlCommand consulta, ObjetoDominio objeto);

        abstract public void insertar(ObjetoDominio objeto);

        protected void insertarAbstracto(ObjetoDominio objeto)
        {
            try
            {
                SqlCommand consulta = new SqlCommand(cadenaInsertar(), BD());
                agregarParametros(consulta, objeto);
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
                consulta.Connection.Close();
            }
            catch (SqlException)
            {
                Console.WriteLine("Hubo un error al insertar el registro {0}.", objeto.getId());
            }
        }

        abstract public void eliminar(int id);

        protected void eliminarAbstracto(int id)
        {
            try
            {
                SqlCommand consulta = new SqlCommand(cadenaEliminar(), BD());
                agregarParametro(consulta, id);
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
                consulta.Connection.Close();
            }
            catch (SqlException)
            {
                Console.WriteLine("Hubo un error al eliminar el registro {0}", id);
            }
        }

        abstract protected void agregarParametro(SqlCommand consulta, int id);

        private SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
