using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    abstract class RegistroMapper
    {
        public static RegistroMapper getMapper(Type clase)
        {
            //TO-DO
            return null;
        }

        abstract public void insertar();

        abstract public void actualizar();

        abstract public void eliminar(); 
    }
}
