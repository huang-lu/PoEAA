using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTableInheritance
{
    abstract class Mapper
    {
        protected Gateway gateway;

        public Mapper(Gateway gateway)
        {
            this.gateway = gateway;
        }

        protected ObjetoDominio BuscarAbstracto(long id, string nombreTabla)
        {
            DataRow fila = BuscarFila(id, Tabla(nombreTabla));
            if (fila == null)
            {
                return null;
            }
            else
            {
                ObjetoDominio resultado = CrearObjetoDominio();
                resultado.id = id;
                Cargar(resultado);
                return resultado;
            }
        }

        protected DataTable Tabla(string nombreTabla)
        {
            return gateway.Datos.Tables[nombreTabla];
        }

        protected DataRow BuscarFila(long id, DataTable tabla)
        {
            string filtro = String.Format("id = {0}", id);
            DataRow[] filas = tabla.Select(filtro);
            if (filas.Length == 0)
            {
                return null;
            }
            else
            {
                return filas[0];
            }
        }

        protected DataRow BuscarFila(long id, string nombreTabla)
        {
            return BuscarFila(id, Tabla(nombreTabla));
        }
        protected ObjetoDominio Buscar(DataRow fila)
        {
            ObjetoDominio objeto = CrearObjetoDominio();
            Cargar(objeto);
            return objeto;
        }

        abstract protected ObjetoDominio CrearObjetoDominio();
        abstract protected void Cargar(ObjetoDominio objeto);

        public virtual void Actualizar(ObjetoDominio objeto)
        {
            Guardar(objeto);
        }
        abstract protected void Guardar(ObjetoDominio objeto);

        public virtual long Insertar(ObjetoDominio objeto)
        {
            objeto.id = gateway.siguienteId("Jugador");
            AnadirFila(objeto);
            Guardar(objeto);
            return objeto.id;
        }       

        protected virtual void InsertarFila(ObjetoDominio objeto, DataTable tabla)
        {
            DataRow fila = tabla.NewRow();
            fila["Id"] = objeto.id;
            tabla.Rows.Add(fila);
        }
        abstract protected void AnadirFila(ObjetoDominio objeto);
    }
}
