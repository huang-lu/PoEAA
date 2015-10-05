using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependentMapping
{
    class Cancion
    {
        private readonly string titulo;

        public Cancion(string titulo)
        {
            this.titulo = titulo;
        }

        public string getTitulo()
        {
            return titulo;
        }
    }
}
