using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    abstract class EstrategiaReconocimiento
    {
        public abstract void calcularReconocimientoIngresos(Contrato contrato);
    }
}
