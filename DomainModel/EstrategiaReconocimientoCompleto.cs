using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    class EstrategiaReconocimientoCompleto : EstrategiaReconocimiento
    {
        public override void calcularReconocimientoIngresos(Contrato contrato)
        {
            contrato.anadirReconocimientoIngreso(new ReconocimientoIngreso(contrato.getIngreso(), contrato.getFechaFirma()));
        }
    }
}
