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
            album.setTitulo(album.getTitulo() + ".");
            album.EliminarCancion(2);            
            albumMap.Actualizar(album);
            Console.WriteLine("{0} {1}", album.id, album.getTitulo());
            foreach (Cancion c in album.getCanciones())
            {
                Console.WriteLine("\t{0}. {1}", c.id, c.getTitulo());
            }

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
