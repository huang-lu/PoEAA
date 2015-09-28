using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            Persona persona = mapeador.Buscar(2);
            Console.WriteLine(persona.Nombre);

            ArrayList personas = mapeador.BuscarPorApellidos("Ortiz%");

            foreach (Persona item in personas)
            {
                Console.WriteLine("{0} {1}", item.Nombre, item.Apellidos);
            }
            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
