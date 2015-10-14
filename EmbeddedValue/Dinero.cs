using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeddedValue
{
    class Dinero
    {
        private double importe;
        private string moneda;

        public Dinero(double importe, string moneda)
        {
            this.importe = importe;
            this.moneda = moneda;
        }

        public double Importe { get { return importe; } }

        public string Moneda { get { return moneda; } }
    }
}
