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

        public Album(int id, string titulo)
        {
            albumId = id;
            this.titulo = titulo;
        }

        public override int getId()
        {
            return albumId;
        }

        public void setTitulo(string titulo)
        {
            this.titulo = titulo;
            marcarModificado();
        }

        public string getTitulo()
        {
            return titulo;
        }
        public static Album crear(int id, string nombre)
        {
            Album album = new Album(id, nombre);
            album.marcarNuevo();
            return album;
        }

        public static void eliminar(Album album)
        {
            album.marcarEliminado();
        }
    }
}
