using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityField
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet esquema = new DataSet();
            Rellenar(esquema);
            AlbumMapper albumMapper = new AlbumMapper(esquema.Tables["Album"]);
            Album album = albumMapper.Buscar(2);
            Console.WriteLine(album.getTitulo());
            
            albumMapper.Insertar(new Album("dsdsadaf"));
            Imprimir(esquema.Tables["Album"]);

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void Rellenar(DataSet esquema)
        {
            RellenarTabla(esquema, "Album");
        }
        private static void RellenarTabla(DataSet esquema, string tabla)
        {
            SqlConnection conexion = conectarBD();

            conexion.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(String.Format("SELECT * FROM Album", tabla),
                conexion);
            dataAdapter.FillSchema(esquema, SchemaType.Source, tabla);
            dataAdapter.Fill(esquema, tabla);
            conexion.Close();
        }

        private static void Imprimir(DataTable tabla)
        {
            foreach (DataRow fila in tabla.Rows)
            {
                Console.WriteLine("{0} {1}", fila["AlbumId"], fila["Titulo"]);
            }
        }

        private static SqlConnection conectarBD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
