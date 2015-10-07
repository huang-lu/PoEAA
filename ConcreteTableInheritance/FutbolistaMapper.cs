using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcreteTableInheritance
{
    class FutbolistaMapper : JuagadorAbstractoMapper
    {
        public FutbolistaMapper(Gateway gatW) : base(gatW) { }

        public override string NombreTabla
        {
            get
            {
                return "Futbolista";
            }
        }

        public Futbolista Buscar(long id)
        {
            return (Futbolista)BuscarAbstracto(id);
        }

        protected override void Cargar(ObjetoDominio objeto, DataRow fila)
        {
            base.Cargar(objeto, fila);
            Futbolista cricketer = (Futbolista)objeto;
            cricketer.club = (string)fila["Club"];
        }

        protected override void Guardar(ObjetoDominio objeto, DataRow fila)
        {
            base.Guardar(objeto, fila);
            Futbolista futbolista = (Futbolista)objeto;
            fila["Club"] = futbolista.club;
            gateway.guardarABd(NombreTabla);
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Futbolista();
        }
    }
}
