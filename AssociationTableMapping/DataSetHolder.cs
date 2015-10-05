using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationTableMapping
{
    class DataSetHolder
    {
        public DataSet Datos = new DataSet();
        private Hashtable dataAdapters = new Hashtable();

        public DataSetHolder()
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            cargarTabla("Empleado");
            cargarTabla("Habilidad");
            cargarTabla("HabilidadesEmpleados");
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

        private static SqlConnection conectarBD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
