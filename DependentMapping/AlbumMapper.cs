using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependentMapping
{
    class AlbumMapper
    {
        protected string cadenaBuscar()
        {
            return @"SELECT a.AlbumId, a.Titulo, c.Titulo as TituloCancion
                     FROM Album a, Cancion c
                     WHERE a.AlbumId = @id AND c.AlbumId = a.AlbumId";
        }

        public ObjetoDominio Buscar(long id)
        {
            SqlCommand consulta = new SqlCommand(cadenaBuscar(), BD());
            consulta.Parameters.AddWithValue("@id", id);
            consulta.Connection.Open();
            SqlDataReader filas = consulta.ExecuteReader();
            filas.Read();
            ObjetoDominio resultado = Cargar(id, filas);
            consulta.Connection.Close();
            return resultado;
        }

        public ObjetoDominio Cargar(long id, SqlDataReader filas)
        {
            string titulo = (string)filas["Titulo"];
            Album resultado = new Album(id, titulo);
            CargarCanciones(resultado, filas);
            return resultado;
        }

        public void CargarCanciones(Album objeto, SqlDataReader filas)
        {
            objeto.AnadirCancion(NuevaCancion(filas));
            while (filas.Read())
            {
                objeto.AnadirCancion(NuevaCancion(filas));
            }
        }

        private Cancion NuevaCancion(SqlDataReader fila)
        {
            string titulo = (string)fila["TituloCancion"];
            Cancion nuevaCancion = new Cancion(titulo);
            return nuevaCancion;
        }

        private SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
