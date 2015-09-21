using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DomainModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Producto word = Producto.nuevoProcesadorTexto("Procesador Word");
            Producto hojaCalculo = Producto.nuevaHojaCalculo("Excel");
            Producto baseDatos = Producto.nuevaBaseDatos("SQL Server");
            Contrato contrato = new Contrato(word, 10, DateTime.Today);

            contrato.calcularReconocimientos();
            Console.WriteLine(String.Format("Ingreso reconocido para {0} a fecha de hoy: {1}",
                contrato.getNombreProducto(), contrato.ingresoReconocido(DateTime.Today)));
            Console.WriteLine("Pulse alguna tecla para continuar...");
            Console.ReadKey();
        }
    }
}
