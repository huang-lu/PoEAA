using System;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork
{
    class UnitOfWork
    {
        private ArrayList objetosNuevos = new ArrayList();
        private ArrayList objetosModificados = new ArrayList();
        private ArrayList objetosEliminados = new ArrayList();
        private static LocalDataStoreSlot hilo = Thread.AllocateNamedDataSlot("hilo");

        public void registrarNuevo(ObjetoDominio objeto)
        {
            Debug.Assert(objeto.getId() != 0, "Id no nula");
            Debug.Assert(!objetosModificados.Contains(objeto), "Objeto no modificado");
            Debug.Assert(!objetosEliminados.Contains(objeto), "Objeto no eliminado");
            Debug.Assert(!objetosNuevos.Contains(objeto), "El objeto ya ha sido registrado");
            objetosNuevos.Add(objeto);
        }

        public void registrarModificado(ObjetoDominio objeto)
        {
            Debug.Assert(objeto.getId() != 0, "Id no nula");
            Debug.Assert(!objetosEliminados.Contains(objeto), "Objeto no eliminado");
            if (!objetosModificados.Contains(objeto) && !objetosNuevos.Contains(objeto))
            {
                objetosModificados.Add(objeto);
            }
        }

        public void registrarEliminado(ObjetoDominio objeto)
        {
            Debug.Assert(objeto.getId() != 0, "Id no nula");
            if (objetosNuevos.Contains(objeto))
            {
                objetosNuevos.Remove(objeto);
                return;
            }
            objetosModificados.Remove(objeto);
            if (!objetosEliminados.Contains(objeto))
            {
                objetosEliminados.Remove(objeto);
            }
            objetosEliminados.Add(objeto);
        }

        public void registrarLimpio(ObjetoDominio objeto)
        {
            Debug.Assert(objeto.getId() != 0, "Id no nula");
        }

        public void commit()
        {
            insertarNuevo();
            actualizarModificados();
            borrarEliminados();
        }

        private void insertarNuevo()
        {
            foreach (ObjetoDominio objeto in objetosNuevos)
            {
                RegistroMapper.getMapper(objeto.GetType()).insertar(objeto);
            }
        }

        private void actualizarModificados()
        {
            foreach (ObjetoDominio objeto in objetosModificados)
            {
                RegistroMapper.getMapper(objeto.GetType()).actualizar(objeto);
            }
        }

        private void borrarEliminados()
        {
            foreach (ObjetoDominio objeto in objetosEliminados)
            {
                RegistroMapper.getMapper(objeto.GetType()).eliminar(objeto.getId());
            }
        }

        public static void nuevoHilo()
        {
            setHilo(new UnitOfWork());
        }

        public static void setHilo(UnitOfWork uow)
        {
            Thread.SetData(hilo, uow);
        }

        public static UnitOfWork getHilo()
        {
            return (UnitOfWork)Thread.GetData(hilo);
        }
    }
}
