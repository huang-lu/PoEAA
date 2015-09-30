using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork.nuevoHilo();
            actualizarTitulo(1, "The Flame");

            for (int i = 4; i < 11; i++)
            {
                insertarAlbum(i, "Devil come to me");
            }
            UnitOfWork.getHilo().commit();
            UnitOfWork.nuevoHilo();
            for (int i = 4; i < 11; i++)
            {
                actualizarTitulo(i, String.Format("Devil come to me {0}", i));
            }

            eliminarAlbum(5);
            UnitOfWork.getHilo().commit();

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void eliminarAlbum(int id)
        {
            AlbumMapper mapper = (AlbumMapper)RegistroMapper.getMapper(typeof(Album));
            Album album = (Album)mapper.buscar(id);
            Album.eliminar(album);
        }

        private static void insertarAlbum(int id, string titulo)
        {
            Album.crear(id, titulo);
        }

        private static void actualizarTitulo(int id, string titulo)
        {
            AlbumMapper mapper = (AlbumMapper)RegistroMapper.getMapper(typeof(Album));
            Album album = (Album)mapper.buscar(id);
            album.setTitulo(titulo);
        }
    }
}
