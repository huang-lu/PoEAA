using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleTableInheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            Gateway gateway = new Gateway();
            JugadorMapper jugadorMapper = new JugadorMapper(gateway);
            Jugador jugador = jugadorMapper.Buscar(2);
            Imprimir(jugador);

            jugador.Nombre = "Esteban";
            jugadorMapper.Actualizar(jugador);
            Imprimir(jugadorMapper.Buscar(2));

            Bowler bowler = new Bowler();
            bowler.Nombre = "Rafa";
            bowler.mediaBateo = 4;
            bowler.mediaBowling = 9;
            jugadorMapper.Insertar(bowler);
            Imprimir(bowler);

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
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
