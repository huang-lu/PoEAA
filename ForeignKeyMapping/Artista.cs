using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeyMapping
{
    class Artista : ObjetoDominio
    {
        private string nombre;
        public Artista(long id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }

        public string getNombre()
        {
            return nombre;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }
    }
}
