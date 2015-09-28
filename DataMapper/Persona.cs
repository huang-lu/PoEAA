using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    class Persona : ObjetoDominio
    {
        private string nombre;
        private string apellidos;
        private int numeroDependendientes;
        private int PersonaId;

        public string Nombre
        {
            get
            {
                return nombre;
            }
        }

        public string Apellidos
        {
            get
            {
                return apellidos;
            }
        }
                

        public Persona(int id, string nombre, string apellidos, int numeroDependientes)
        {
            this.PersonaId = id;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.numeroDependendientes = numeroDependientes;
        }
    }
}
