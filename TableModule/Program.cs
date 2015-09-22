using System;
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
        static SqlDataAdapter contratos;
        static SqlDataAdapter productos;
        static SqlDataAdapter reconocimientoIngreso;
        static void Main(string[] args)
        {

            DataSet esquema = new DataSet();
            rellenar(esquema);
            imprimirDataSet(esquema, "ReconocimientoIngreso");

            Contrato contrato = new Contrato(esquema);
            Producto producto = new Producto(esquema);
            contrato.calcularReconocimiento(1);

            Console.WriteLine("{0}", producto[1]["Nombre"]);
            producto[1]["Nombre"] = "Pepito";
            imprimirDataSet(esquema, "ReconocimientoIngreso");

            rellenar(esquema);
            actualizarBD(esquema);

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void actualizarBD(DataSet esquema)
        {
            SqlCommandBuilder cb = new SqlCommandBuilder(contratos);
            SqlConnection conexion = conectarBD();

            contratos.Update(esquema, "Contrato");
            contratos.UpdateCommand = cb.GetUpdateCommand(true);
            contratos.InsertCommand = cb.GetInsertCommand(true);

            cb = new SqlCommandBuilder(productos);
            productos.UpdateCommand = cb.GetUpdateCommand(true);
            productos.InsertCommand = cb.GetInsertCommand(true);
            productos.Update(esquema, "Producto");

            cb = new SqlCommandBuilder(reconocimientoIngreso);
            reconocimientoIngreso.InsertCommand = cb.GetInsertCommand(true);
            reconocimientoIngreso.UpdateCommand = cb.GetUpdateCommand(true);
            reconocimientoIngreso.Update(esquema, "ReconocimientoIngreso");
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
            SqlConnection conexion = conectarBD();
            conexion.Open();
            contratos = new SqlDataAdapter("SELECT * FROM Contrato", conexion);
            productos = new SqlDataAdapter("SELECT * FROM Producto", conexion);
            reconocimientoIngreso = new SqlDataAdapter("SELECT * FROM ReconocimientoIngreso", conexion);

            contratos.FillSchema(esquema, SchemaType.Source, "Contrato");
            contratos.Fill(esquema, "Contrato");

            productos.FillSchema(esquema, SchemaType.Source, "Producto");
            productos.Fill(esquema, "Producto");

            reconocimientoIngreso.FillSchema(esquema, SchemaType.Source, "ReconocimientoIngreso");
            reconocimientoIngreso.Fill(esquema, "ReconocimientoIngreso");
            conexion.Close();
        }

        private static SqlConnection conectarBD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
