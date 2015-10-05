using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignKeyMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            AlbumMapper albumMapper = new AlbumMapper();
            Album album = albumMapper.Buscar(2);
            Artista artista = album.getArtista();
            Console.WriteLine(String.Format("El album '{0}' de {1}", album.getTitulo(), artista.getNombre()));

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
