using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowDataGateway
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonaBuscador buscador = new PersonaBuscador();
            PersonaGateway persona = buscador.Buscar(1);
            imprimir(persona);

            persona = new PersonaGateway("Andrés", "Cabello Pinto", 3);
            persona.Insertar();

            List<PersonaGateway> personas = buscador.BuscarResponsables();
            foreach (PersonaGateway per in personas)
            {
                imprimir(per);
            }

            Console.WriteLine("Pulse cualquier tecla para continuar");
            Console.ReadKey();
        }

        private static void imprimir(PersonaGateway persona)
        {
            Console.WriteLine("{0} {1} {2} {3}", persona.PersonaId, persona.Nombre, persona.Apellidos, persona.NumeroDependientes);
        }
    }
}
