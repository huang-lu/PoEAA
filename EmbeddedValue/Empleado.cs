using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeddedValue
{
    class Empleado : ObjetoDominio
    {
        private string nombre;
        private RangoFecha periodo;
        private Dinero salario;

        public Empleado(long id, string nombre, RangoFecha periodo, Dinero salario) : base(id)
        {
            this.nombre = nombre;
            this.periodo = periodo;
            this.salario = salario;
        }

        public Empleado(long id) : base(id)
        {
            nombre = null;
            periodo = null;
            salario = null;
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public RangoFecha Periodo
        {
            get { return periodo; }
            set { periodo = value; }
        }

        public Dinero Salario
        {
            get { return salario; }
            set { salario = value; }

        }
    }
}
