using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    class Contrato
    {
        private List<ReconocimientoIngreso> reconocimientoIngresos = new List<ReconocimientoIngreso>();
        private Producto producto;
        private decimal ingreso;
        private DateTime fechaFirma;
        private long contratoId;

        public Contrato(Producto producto, decimal ingreso, DateTime fecha)
        {
            this.producto = producto;
            this.ingreso = ingreso;
            this.fechaFirma = fecha;
        }
        
        public decimal ingresoReconocido(DateTime fecha)
        {
            decimal resultado = 0;
            foreach (ReconocimientoIngreso ingreso in reconocimientoIngresos)
            {
                if (ingreso.esReconocibleEn(fecha))
                {
                    resultado += ingreso.getIngreso();
                }
            }

            return resultado;
        }

        public void anadirReconocimientoIngreso(ReconocimientoIngreso reconocimientoIngreso)
        {
            reconocimientoIngresos.Add(reconocimientoIngreso);
        }

        public void calcularReconocimientos()
        {
            producto.calcularReconocimientoIngresos(this);
        }

        public decimal getIngreso()
        {
            return ingreso;
        }

        public DateTime getFechaFirma()
        {
            return fechaFirma;
        }

        public string getNombreProducto()
        {
            return producto.getNombre();
        }
    }
}
