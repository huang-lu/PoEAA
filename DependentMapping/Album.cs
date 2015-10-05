using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependentMapping
{
    class Album : ObjetoDominio
    {
        private ArrayList canciones = new ArrayList();
        private string titulo;

        public Album(long id, string titulo)
        {
            this.titulo = titulo;
            this.id = id;
        }

        public string getTitulo()
        {
            return titulo;
        }
        public void AnadirCancion(Cancion cancion)
        {
            canciones.Add(cancion);
        }

        public void EliminarCancion(Cancion cancion)
        {
            canciones.Remove(cancion);
        }

        public void EliminarCancion(int i)
        {
            canciones.RemoveAt(i);
        }

        public Cancion[] getCanciones()
        {
            return (Cancion[])canciones.ToArray(typeof(Cancion));
        }
    }
}
