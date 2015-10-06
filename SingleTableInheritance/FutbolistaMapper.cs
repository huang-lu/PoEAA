using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleTableInheritance
{
    class FutbolistaMapper : JugadorAbstractoMapper
    {
        public const string TIPO = "F";

        public FutbolistaMapper(Gateway gateway) : base(gateway) { }

        public override string Tipo
        {
            get { return TIPO; }
        }

        public Futbolista Buscar(long id)
        {
            return (Futbolista)BuscarAbstracto(id);
        }

        protected override void Cargar(ObjetoDominio objeto, DataRow fila)
        {
            base.Cargar(objeto, fila);
            Futbolista futbolista = (Futbolista)objeto;
            futbolista.club = (string)fila["Club"];
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Futbolista();
        }

        protected override void Guardar(ObjetoDominio objeto, DataRow fila)
        {
            base.Guardar(objeto, fila);
            Futbolista futbolista = (Futbolista)objeto;
            fila["Club"] = futbolista.club;
        }
    }
}
