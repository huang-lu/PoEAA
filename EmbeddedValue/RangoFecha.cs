using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmbeddedValue
{
    class RangoFecha
    {
        private DateTime inicial;
        private DateTime final;

        public RangoFecha(DateTime inicial, DateTime final)
        {
            this.inicial = inicial;
            this.final = final;
        }

        public DateTime Inicial { get { return inicial; } }
        public DateTime Final { get { return final; } }
    }
}
