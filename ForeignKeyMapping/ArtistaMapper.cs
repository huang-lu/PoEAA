using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeyMapping
{
    class ArtistaMapper : MapperAbstracto
    {
        public Artista Buscar(long id)
        {
            return (Artista)BuscarAbstracto(id);
        }

        protected override string cadenaBuscar()
        {
            return "SELECT * FROM Artista WHERE ArtistaId = @id";
        }

        protected override ObjetoDominio HacerCarga(long id, SqlDataReader fila)
        {
            string nombre = Convert.ToString(fila["Nombre"]);
            return new Artista(id, nombre);
        }
    }
}
