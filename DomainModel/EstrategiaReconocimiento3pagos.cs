using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    class EstrategiaReconocimiento3pagos : EstrategiaReconocimiento
    {
        private int primerOffsetReconocimiento;
        private int segundoOffsetReconocimiento;

        public EstrategiaReconocimiento3pagos(int primerOffset, int segundoOffset)
        {
            primerOffsetReconocimiento = primerOffset;
            segundoOffsetReconocimiento = segundoOffset;
        }
        public override void calcularReconocimientoIngresos(Contrato contrato)
        {
            decimal[] ingresoFraccionado = fraciona(contrato.getIngreso(), 3);

            contrato.anadirReconocimientoIngreso(new ReconocimientoIngreso(ingresoFraccionado[0], contrato.getFechaFirma()));
            contrato.anadirReconocimientoIngreso(new ReconocimientoIngreso(ingresoFraccionado[1],
                contrato.getFechaFirma().AddDays(primerOffsetReconocimiento)));
            contrato.anadirReconocimientoIngreso(new ReconocimientoIngreso(ingresoFraccionado[2],
                contrato.getFechaFirma().AddDays(segundoOffsetReconocimiento)));
        }

        private decimal[] fraciona(decimal ingresoTotal, int nPartes)
        {
            decimal[] resultado = new decimal[3];
            decimal valorMinimo = ingresoTotal / nPartes;
            decimal valorMaximo = valorMinimo + 1;
            int resto = (int)ingresoTotal % nPartes;
            for (int i = 0; i < resto; i++)
            {
                resultado[i] = valorMaximo;
            }
            for (int i = resto; i < resultado.Length; i++)
            {
                resultado[i] = valorMinimo;
            }
            return resultado;
        }
    }
}
