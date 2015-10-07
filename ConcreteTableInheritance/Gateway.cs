using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcreteTableInheritance
{
    class Gateway
    {
        public DataSet Datos = new DataSet();
        private Hashtable dataAdapters = new Hashtable();

        public Gateway()
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            //cargarTabla("Jugador");
            cargarTabla("Cricketer");
            cargarTabla("Bowler");
            cargarTabla("Futbolista");
        }

        private void cargarTabla(string tabla)
        {
            SqlConnection conexion = conectarBD();

            conexion.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(String.Format("SELECT * FROM {0}", tabla), conexion);
            dataAdapter.FillSchema(Datos, SchemaType.Source, tabla);
            dataAdapter.Fill(Datos, tabla);
            dataAdapters.Add(tabla, dataAdapter);
            conexion.Close();
        }

        public void guardarABd(string tabla)
        {
            SqlCommandBuilder cb;
            SqlConnection conexion = conectarBD();

            SqlDataAdapter dataAdapter = (SqlDataAdapter)dataAdapters[tabla];
            cb = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Update(Datos, tabla);
        }

        public long siguienteId(string tabla)
        {
            SqlConnection conexion = conectarBD();
            long resultado;

            SqlCommand consulta = new SqlCommand(String.Format("SELECT MAX(ISNULL(Id, 0)) FROM {0}", tabla), conexion);
            consulta.Connection.Open();
            SqlDataReader maxId = consulta.ExecuteReader();
            maxId.Read();
            resultado = (int)maxId[0];
            consulta.Connection.Close();
            return resultado + 1;
        }

        private static SqlConnection conectarBD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
