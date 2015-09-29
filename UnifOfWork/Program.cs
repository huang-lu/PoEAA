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
            actualizarTitulo(1, "The Flame");

            for (int i = 4; i < 11; i++)
            {
                insertarAlbum(i, "Devil come to me"); 
            }

            for (int i = 4; i < 11; i++)
            {
                actualizarTitulo(i, String.Format("Devil come to me {0}",i));
            }

            eliminarAlbum(5);

            Console.WriteLine("Pulse cualquier tecla para continuar...");
            Console.ReadKey();
        }

        private static void eliminarAlbum(int id)
        {
            UnitOfWork.nuevoHilo();
            AlbumMapper mapper = (AlbumMapper)RegistroMapper.getMapper(typeof(Album));
            Album album = (Album)mapper.buscar(id);
            Album.eliminar(album);
            UnitOfWork.getHilo().commit();
        }

        private static void insertarAlbum(int id, string titulo)
        {
            UnitOfWork.nuevoHilo();
            AlbumMapper mapper = (AlbumMapper)RegistroMapper.getMapper(typeof(Album));
            Album.crear(id, titulo);
            UnitOfWork.getHilo().commit();
        }

        private static void actualizarTitulo(int id, string titulo)
        {
            UnitOfWork.nuevoHilo();
            AlbumMapper mapper = (AlbumMapper)RegistroMapper.getMapper(typeof(Album));
            Album album = (Album)mapper.buscar(id);
            album.setTitulo(titulo);
            UnitOfWork.getHilo().commit();
        }
    }
}
