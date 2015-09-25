using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonaMapper mapeador = new PersonaMapper();
            Persona persona = mapeador.buscar(2);
            Console.WriteLine(persona.Nombre);
            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
