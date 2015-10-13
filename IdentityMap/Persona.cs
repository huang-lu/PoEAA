using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityMap
{
    class Persona : ObjetoDominio
    {
        private string nombre;
        private string apellidos;
        private int numeroDependientes;

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Apellidos
        {
            get
            {
                return apellidos;
            }

            set
            {
                apellidos = value;
            }
        }

        public int NumeroDependientes
        {
            get
            {
                return numeroDependientes;
            }

            set
            {
                numeroDependientes = value;
            }
        }

        public Persona(long personaId, string nombre, string apellidos, int numeroDependientes)
        {
            Id = personaId;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.NumeroDependientes = numeroDependientes;
        }
    }
}
