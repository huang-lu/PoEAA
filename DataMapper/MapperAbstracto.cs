using System;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    abstract class MapperAbstracto
    {
        protected Hashtable objetosCargados = new Hashtable();
        abstract protected string cadenaBuscar();

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

        public ArrayList BuscarCualquier(Consulta consulta)
        {
            SqlCommand consul = new SqlCommand(consulta.Sql(), BD());
            foreach (var parametro in consulta.Parametros().Keys)
            {
                consul.Parameters.AddWithValue(parametro.ToString(), consulta.Parametros()[parametro]);
            }
            consul.Connection.Open();
            SqlDataReader resultado = consul.ExecuteReader();
            return cargarTodos(resultado);
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

        protected ArrayList cargarTodos(SqlDataReader filas)
        {
            ArrayList resultado = new ArrayList();
            while (filas.Read())
            {
                resultado.Add(cargar(filas));
            }
            return resultado;
        }

        private SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
