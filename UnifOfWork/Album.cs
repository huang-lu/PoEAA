    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    class Album : ObjetoDominio
    {
        private int albumId;
        private string titulo;

        private Album(int id, string titulo)
        {
            albumId = id;
            this.titulo = titulo;
        }

        public override int getId()
        {
            return albumId;
        }

        public static Album crear(string nombre)
        {
            Album album = new Album(10, nombre);
            album.marcarNuevo();
            return album;
        }

        public void setTitulo(string titulo)
        {
            this.titulo = titulo;
            marcarModificado();
        }
    }
}
