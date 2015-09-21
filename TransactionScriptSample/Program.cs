using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionScriptSample
{
    class Program
    {
        static void Main(string[] args)
        {
            RecognitionService rs = new RecognitionService();
            //Console.WriteLine("Vamos a calcular el reconocimiento de ingresos del contrato 1.");
            //rs.calcularReconocimientoIngresos(1);
            decimal ingresos = rs.ingresosReconocidos(1, DateTime.Today);
            Console.WriteLine("Los ingresos para el contrato 1 a fecha de hoy son {0}", ingresos);

            //rs.calcularReconocimientoIngresos(3);
            ingresos = rs.ingresosReconocidos(3, new DateTime(2015,10,31));
            Console.WriteLine("Los ingresos para el contrato 3 a fecha de hoy son {0}", ingresos);
            Console.WriteLine("Pulse alguna tecla para continuar...");
            Console.ReadKey();
        }
    }
}
