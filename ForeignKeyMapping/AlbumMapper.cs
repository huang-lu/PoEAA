using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeyMapping
{
    class AlbumMapper : MapperAbstracto
    {
        public Album Buscar(long id)
        {
            return (Album)BuscarAbstracto(id);
        }

        protected override string cadenaBuscar()
        {
            return "SELECT * FROM Album WHERE AlbumId = @id";
        }

        protected override ObjetoDominio HacerCarga(long id, SqlDataReader fila)
        {
            string titulo = Convert.ToString(fila["Titulo"]);
            long artistaId = Convert.ToInt64(fila["ArtistaId"]);
            Artista artista = MapperAbstracto.Artista().Buscar(artistaId);
            return new Album(id, titulo, artista);
        }
    }
}
