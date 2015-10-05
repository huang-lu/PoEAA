using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependentMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            AlbumMapper albumMap = new AlbumMapper();
            Album album = (Album)albumMap.Buscar(2);
            Cancion[] canciones = album.getCanciones();
            Console.WriteLine(album.getTitulo());
            foreach (Cancion c in canciones)
            {
                Console.WriteLine("\t{0}", c.getTitulo());
            }

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
