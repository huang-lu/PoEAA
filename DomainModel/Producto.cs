using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    class Producto
    {
        private string nombre;
        private EstrategiaReconocimiento estrategiaReconocimiento;

        public Producto(string nombre, EstrategiaReconocimiento estrategiaReconocimiento)
        {
            this.nombre = nombre;
            this.estrategiaReconocimiento = estrategiaReconocimiento;
        }

        public static Producto nuevoProcesadorTexto(string name)
        {
            return new Producto(name, new EstrategiaReconocimientoCompleto());
        }

        public static Producto nuevaHojaCalculo(string name)
        {
            return new Producto(name, new EstrategiaReconocimiento3pagos(60, 90));
        }

        public static Producto nuevaBaseDatos(string name)
        {
            return new Producto(name, new EstrategiaReconocimiento3pagos(30, 60));
        }

        public void calcularReconocimientoIngresos(Contrato contrato)
        {
            estrategiaReconocimiento.calcularReconocimientoIngresos(contrato);
        }

        public string getNombre()
        {
            return nombre;
        }
    }
}
