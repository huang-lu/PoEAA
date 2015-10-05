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
    class AlbumMapper : Mapper
    {
        public AlbumMapper(DataTable tabla)
        {
            this.tabla = tabla;
        }
        public Album Buscar(long id)
        {
            return (Album)BuscarAbstracto(id);
        }

        protected override void Cargar(ObjetoDominio objeto, DataRow fila)
        {
            ((Album)objeto).id = Convert.ToInt64(fila["AlbumId"]);
            ((Album)objeto).setTitulo(Convert.ToString(fila["Titulo"]));
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Album(null);
        }

        protected override long getSiguienteId()
        {
            DataRowCollection filas = tabla.Rows;
            DataRow ultimaFila = filas[filas.Count-1];
            return Convert.ToInt64(ultimaFila["AlbumId"]) + 1;
        }

        protected override void Guardar(ObjetoDominio objeto, DataRow fila)
        {
            fila["AlbumId"] = ((Album)objeto).id;
            fila["Titulo"] = ((Album)objeto).getTitulo();
        }

        protected override string Filtro(long id)
        {
            return String.Format("AlbumId = {0}", id);
        }

        private SqlConnection BD()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        }
    }
}
