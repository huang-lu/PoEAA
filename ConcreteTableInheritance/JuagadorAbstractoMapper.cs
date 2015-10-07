using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcreteTableInheritance
{
    abstract class JuagadorAbstractoMapper : Mapper
    {
        public JuagadorAbstractoMapper(Gateway gatW) : base(gatW) { }

        protected override void Cargar(ObjetoDominio objeto, DataRow fila)
        {
            base.Cargar(objeto, fila);
            Jugador jugador = (Jugador)objeto;
            jugador.Nombre = (string)fila["Nombre"];
        }

        protected override void Guardar(ObjetoDominio objeto, DataRow fila)
        {
            Jugador jugador = (Jugador)objeto;
            fila["Nombre"] = jugador.Nombre;
        }
    }
}
