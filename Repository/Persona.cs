﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    class Persona : ObjetoDominio
    {
        private string nombre;
        private string apellidos;
        private int numeroDependientes;
        private long benefactor;
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

        public long Benefactor
        {
            get
            {
                return benefactor;
            }
            set
            {
                benefactor = value;
            }
        }

        public Persona() : base() { }

        public Persona(long id)
        {
            this.id = id;
        }

        public Persona(long id, string nombre, string apellidos, int numDep)
        {
            this.id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            NumeroDependientes = numDep;
        }

        public IList Dependientes()
        {
            return Registro.RepositorioPersona().DependientesDe(this);
        }

        public override string ToString()
        {
            return String.Format("Id: {0}\n\tNombre: {1}\n\tApellidos: {2}\n\tNumero de dependientes: {3}\n",
                id, nombre, apellidos, numeroDependientes);
        }
    }
}
