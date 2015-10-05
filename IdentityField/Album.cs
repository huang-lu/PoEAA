using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityField
{
    class Album : ObjetoDominio
    {
        private string titulo;

        public Album(string titulo)
        {
            this.id = ID_SUSTITUTA ;
            this.titulo = titulo;
        }

        public void setTitulo(string titulo)
        {
            this.titulo = titulo;
        }

        public string getTitulo()
        {
            return titulo;
        }        
    }
}
