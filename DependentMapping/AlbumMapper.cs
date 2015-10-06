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
            return @"SELECT a.AlbumId, a.ArtistaId, a.Titulo, c.CancionId, c.Titulo as TituloCancion
                     FROM Album a, Cancion c
                     WHERE a.AlbumId = @id AND c.AlbumId = a.AlbumId";
        }

        protected string cadenaActualizarAlbum()
        {
            return "UPDATE Album SET Titulo = @titulo WHERE AlbumId = @id";
        }

        protected string cadenaBorrarCanciones()
        {
            return "DELETE FROM Cancion WHERE AlbumId = @id";
        }

        protected string cadenaInsertarCanciones()
        {
            return @"INSERT INTO Cancion VALUES (@id, @albumId, @titulo)";
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
            long cancionId = (int)fila["CancionId"];
            Cancion nuevaCancion = new Cancion(titulo);
            nuevaCancion.id = cancionId;
            return nuevaCancion;
        }

        public void Actualizar(ObjetoDominio objeto)
        {
            Album album = (Album)objeto;
            SqlCommand consulta = new SqlCommand(cadenaActualizarAlbum(), BD());
            consulta.Parameters.AddWithValue("@titulo", album.getTitulo());
            consulta.Parameters.AddWithValue("@id", album.id);
            consulta.Connection.Open();
            consulta.ExecuteNonQuery();
            consulta.Connection.Close();
            ActualizarCanciones(album);
        }

        private void ActualizarCanciones(Album album)
        {
            SqlCommand consulta = new SqlCommand(cadenaBorrarCanciones(), BD());
            consulta.Parameters.AddWithValue("@id", album.id);
            consulta.Connection.Open();
            consulta.ExecuteNonQuery();
            consulta.Connection.Close();
            foreach (Cancion cancion in album.getCanciones())
            {
                InsertarCancion(cancion, album);
            }
        }

        private void InsertarCancion(Cancion cancion, Album album)
        {
            SqlCommand consulta = new SqlCommand(cadenaInsertarCanciones(), BD());
            consulta.Parameters.AddWithValue("@id", cancion.id);
            consulta.Parameters.AddWithValue("@AlbumId", album.id);
            consulta.Parameters.AddWithValue("@titulo", cancion.getTitulo());
            consulta.Connection.Open();
            consulta.ExecuteNonQuery();
            consulta.Connection.Close();
        }

        private SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
