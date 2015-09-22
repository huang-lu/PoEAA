using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableModule
{
    class Program
    {
        static Hashtable dataAdapters = new Hashtable();
        static SqlDataAdapter dataAdapter;

        static void Main(string[] args)
        {
            DataSet esquema = new DataSet();

            rellenar(esquema);
            imprimirDataSet(esquema, "ReconocimientoIngreso");

            Contrato contrato = new Contrato(esquema);
            Producto producto = new Producto(esquema);

            contrato.calcularReconocimiento(1); // Producto con Id 1
            imprimirDataSet(esquema, "ReconocimientoIngreso");
            actualizarBD(esquema);

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void actualizarBD(DataSet esquema)
        {
            actualizarTabla(esquema, "Contrato");
            actualizarTabla(esquema, "Producto");
            actualizarTabla(esquema, "ReconocimientoIngreso");
        }

        private static void actualizarTabla(DataSet esquema, string tabla)
        {
            SqlCommandBuilder cb;
            SqlConnection conexion = conectarBD();

            dataAdapter = (SqlDataAdapter)dataAdapters[tabla];
            cb = new SqlCommandBuilder(dataAdapter);
            dataAdapter.Update(esquema, tabla);
        }

        private static void imprimirDataSet(DataSet esquema, string tabla)
        {
            Console.WriteLine("ContratoId\tCantidad\tFecha");
            foreach (DataRow fila in esquema.Tables[tabla].Rows)
            {
                Console.WriteLine(String.Format("{0}\t\t{1}\t\t{2}", fila["ContratoId"],
                    fila["Cantidad"], fila["FechaReconocimiento"]));
            }
        }

        private static void rellenar(DataSet esquema)
        {
            rellenarTabla(esquema, "Contrato");
            rellenarTabla(esquema, "Producto");
            rellenarTabla(esquema, "ReconocimientoIngreso");
        }

        private static void rellenarTabla(DataSet esquema, string tabla)
        {
            SqlConnection conexion = conectarBD();

            conexion.Open();
            dataAdapter = new SqlDataAdapter(String.Format("SELECT * FROM {0}", tabla), conexion);
            dataAdapter.FillSchema(esquema, SchemaType.Source, tabla);
            dataAdapter.Fill(esquema, tabla);
            dataAdapters.Add(tabla, dataAdapter);
            conexion.Close();
        }

        private static SqlConnection conectarBD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
