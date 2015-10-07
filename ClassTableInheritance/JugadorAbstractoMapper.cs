using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTableInheritance
{
    abstract class JugadorAbstractoMapper : Mapper
    {
        protected static string NombreTabla = "Jugador";

        public JugadorAbstractoMapper(Gateway gateway) : base(gateway) { }
        
        abstract public string Tipo { get; }

        protected override void Cargar(ObjetoDominio objeto)
        {
            DataRow fila = BuscarFila(objeto.id, Tabla(NombreTabla));
            Jugador jugador = (Jugador)objeto;
            jugador.Nombre = (string)fila["Nombre"];
        }

        protected override void Guardar(ObjetoDominio objeto)
        {
            Jugador jugador = (Jugador)objeto;
            DataRow fila = BuscarFila(objeto.id, Tabla(NombreTabla));
            fila["Nombre"] = jugador.Nombre;
            fila["Tipo"] = Tipo;
            gateway.guardarABd(NombreTabla);
        }

        protected override void AnadirFila(ObjetoDominio objeto)
        {
            InsertarFila(objeto, Tabla(NombreTabla));
        }
    }
}
