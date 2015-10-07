using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcreteTableInheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            Gateway gateway = new Gateway();
            JugadorMapper jugadorMapper = new JugadorMapper(gateway);
            ImprimirTodos(jugadorMapper);

            Jugador jugador = jugadorMapper.Buscar(2);
            jugador.Nombre = "Ambrosio";
            jugadorMapper.Actualizar(jugador);
            ImprimirTodos(jugadorMapper);

            Console.ReadKey();
        }

        private static void ImprimirTodos(JugadorMapper jMapper)
        {
            int indice = 1;
            Jugador jugador = jMapper.Buscar(indice);
            while (jugador != null)
            {
                Imprimir(jugador);
                indice++;
                jugador = jMapper.Buscar(indice);
            }
            Console.WriteLine();
        }

        private static void Imprimir(Jugador jugador)
        {
            if (jugador is Bowler)
            {
                Bowler bowler = (Bowler)jugador;
                Console.WriteLine("Bowler: {0} {1} {2}", bowler.Nombre, bowler.mediaBateo, bowler.mediaBowling);
            }
            else if (jugador is Cricketer)
            {
                Cricketer cricketer = (Cricketer)jugador;
                Console.WriteLine("Cricketer: {0} {1}", cricketer.Nombre, cricketer.mediaBateo);
            }
            else
            {
                Futbolista futbolista = (Futbolista)jugador;
                Console.WriteLine("Futbolista: {0} {1}", futbolista.Nombre, futbolista.club);
            }
        }
    }
}
