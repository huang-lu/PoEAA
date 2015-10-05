using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeyMapping
{
    class Album : ObjetoDominio
    {
        private string titulo;
        private Artista artista;

        public Album(long id, string titulo, Artista artista)
        {
            this.id = id;
            this.titulo = titulo;
            this.artista = artista;
        }

        public string getTitulo()
        {
            return titulo;
        }

        public void setTitulo(string titulo)
        {
            this.titulo = titulo;
        }

        public Artista getArtista()
        {
            return artista;
        }

        public void setArtista(Artista artista)
        {
            this.artista = artista;
        }
    }
}
