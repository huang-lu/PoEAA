using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    class ReconocimientoIngreso
    {
        private decimal ingreso;
        private DateTime fecha;

        public ReconocimientoIngreso(decimal ingreso, DateTime fecha)
        {
            this.ingreso = ingreso;
            this.fecha = fecha;
        }

        public decimal getIngreso()
        {
            return ingreso;
        }

        public bool esReconocibleEn(DateTime fecha)
        {
            bool hoyOAnterior = false;
            if (this.fecha.CompareTo(fecha) <= 0)
            {
                hoyOAnterior = true;
            }
            return hoyOAnterior;
        }
    }
}
