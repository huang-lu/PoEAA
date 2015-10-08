using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetadataMapping
{
    abstract class Mapper
    {
        protected DataMap dataMap;

        public Mapper()
        {
            CargarDataMap();
        }
        protected abstract void CargarDataMap();

        public ObjetoDominio BuscarObjeto(long id)
        {
            string sql = "SELECT " + GetColumnaId() + dataMap.GetListaColumnas() + " FROM " + dataMap.GetNombreTabla() +
                " WHERE " + GetColumnaId() + " = @Id";
            SqlCommand consulta = new SqlCommand(sql, BD());
            consulta.Connection.Open();
            consulta.Parameters.AddWithValue("@Id", id);
            SqlDataReader fila = consulta.ExecuteReader();
            if (!fila.HasRows)
            {
                return null;
            }
            fila.Read();
            ObjetoDominio resultado = Cargar(fila);
            consulta.Connection.Close();
            return resultado;
        }

        protected abstract string GetColumnaId();

        private ObjetoDominio Cargar(SqlDataReader fila)
        {
            long id = (int)fila[GetColumnaId()];
            ObjetoDominio resultado = (ObjetoDominio)Activator.CreateInstance(dataMap.GetClaseDominio());
            resultado.SetId(id);
            CargarAtributos(fila, resultado);
            return resultado;
        }

        private void CargarAtributos(SqlDataReader fila, ObjetoDominio objeto)
        {
            foreach (ColumnMap columna in dataMap.GetColumnas())
            {
                var valorColumna = fila[columna.GetNombreColumna()];
                columna.SetAtributo(objeto, valorColumna);
            }
        }

        public void Actualizar(ObjetoDominio objeto)
        {
            String sql = "UPDATE " + dataMap.GetNombreTabla() + dataMap.GetListaActualizar() + " WHERE " + GetColumnaId() + " = @Id";
            try
            {
                SqlCommand consulta = new SqlCommand(sql, BD());
                foreach (ColumnMap columna in dataMap.GetColumnas())
                {
                    consulta.Parameters.AddWithValue(String.Format("@{0}", columna.GetNombreColumna()),
                        columna.GetValor(objeto));
                }
                consulta.Parameters.AddWithValue("@Id", objeto.GetId());
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
                consulta.Connection.Close();
            }
            catch (Exception)
            {
                throw new ApplicationException("Imposible hacer la actualización de la tabla");
            }
        }

        public void Insertar(ObjetoDominio objeto)
        {
            String sql = "INSERT INTO " + dataMap.GetNombreTabla() + " VALUES (@" + GetColumnaId() +
                dataMap.GetListaInsertar() + ")";
            try
            {
                SqlCommand consulta = new SqlCommand(sql, BD());
                foreach (ColumnMap columna in dataMap.GetColumnas())
                {
                    consulta.Parameters.AddWithValue(String.Format("@{0}", columna.GetNombreColumna()),
                        columna.GetValor(objeto));
                }
                consulta.Parameters.AddWithValue("@" + GetColumnaId(), objeto.GetId());
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
                consulta.Connection.Close();
            }
            catch (Exception)
            {
                throw new ApplicationException("Imposible hacer la inserción en la tabla");
            }
        }

        public void Eliminar(ObjetoDominio objeto)
        {
            String sql = "DELETE FROM " + dataMap.GetNombreTabla() + " WHERE " + GetColumnaId() + " = @Id";
            try
            {
                SqlCommand consulta = new SqlCommand(sql, BD());
                consulta.Parameters.AddWithValue("@Id", objeto.GetId());
                consulta.Connection.Open();
                consulta.ExecuteNonQuery();
                consulta.Connection.Close();
            }
            catch (Exception)
            {
                throw new ApplicationException("Imposible eliminar la fila de la tabla");
            }
        }


        //public DataMap GetDataMap()
        //{
        //    return dataMap;
        //}

        //public static Mapper GetMapper(Type tipo)
        //{
        //    if (tipo.Equals(typeof(Persona)))
        //    {
        //        return new PersonaMapper();
        //    }
        //    return null;
        //}

        private SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
