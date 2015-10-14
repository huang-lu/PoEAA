using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    class AlbumMapper : RegistroMapper
    {
        public override void actualizar(ObjetoDominio objeto)
        {
            actualizarAbstracto(objeto);
        }

        public override void eliminar(int id)
        {
            eliminarAbstracto(id);
        }

        public override void insertar(ObjetoDominio objeto)
        {
            insertarAbstracto(objeto);
        }

        public override ObjetoDominio buscar(int id)
        {
            return (Album)buscarAbstracto(id);
        }

        protected override string cadenaBuscar()
        {
            return "SELECT * FROM Album WHERE AlbumId = @id";
        }

        protected override string cadenaActualizar()
        {
            return "UPDATE Album SET Titulo = @titulo WHERE AlbumId = @id";
        }

        protected override string cadenaInsertar()
        {
            return "INSERT INTO Album VALUES (@id, @titulo)";
        }

        protected override string cadenaEliminar()
        {
            return "DELETE FROM Album WHERE AlbumId = @id";
        }

        protected override ObjetoDominio hacerCarga(int id, SqlDataReader fila)
        {
            int albumId = Convert.ToInt32(fila["AlbumId"]);
            string titulo = Convert.ToString(fila["Titulo"]);
            return new Album(albumId, titulo);
        }

        protected override void agregarParametros(SqlCommand consulta, ObjetoDominio objeto)
        {
            consulta.Parameters.AddWithValue("@id", ((Album)objeto).getId());
            consulta.Parameters.AddWithValue("@titulo", ((Album)objeto).getTitulo());
        }

        protected override void agregarParametro(SqlCommand consulta, int id)
        {
            consulta.Parameters.AddWithValue("@id", id);
        }
    }
}
