using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationTableMapping
{
    class Empleado : ObjetoDominio
    {
        private IList datosHabilidades = new ArrayList();
        public string Nombre { get; set; }

        public IList Habilidades
        {
            get
            {
                return ArrayList.ReadOnly(datosHabilidades);
            }
            set
            {
                datosHabilidades = new ArrayList(value);
            }
        }

        public void AnadirHabilidad(Habilidad habilidad)
        {
            datosHabilidades.Add(habilidad);
        }

        public void EliminarHabilidad(Habilidad habilidad)
        {
            datosHabilidades.Remove(habilidad);
        }
    }
}
