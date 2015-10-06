using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleTableInheritance
{
    class CricketerMapper : JugadorAbstractoMapper
    {
        public const string TIPO = "C";

        public CricketerMapper(Gateway gateway) : base(gateway) { }

        public override string Tipo
        {
            get { return TIPO; }
        }

        public Cricketer Buscar(long id)
        {
            return (Cricketer)BuscarAbstracto(id);
        }

        protected override void Cargar(ObjetoDominio objeto, DataRow fila)
        {
            base.Cargar(objeto, fila);
            Cricketer cricketer = (Cricketer)objeto;
            cricketer.mediaBateo = (int)fila["MediaBateo"];
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Cricketer();
        }

        protected override void Guardar(ObjetoDominio objeto, DataRow fila)
        {
            base.Guardar(objeto, fila);
            Cricketer cricketer = (Cricketer)objeto;
            fila["MediaBateo"] = cricketer.mediaBateo;
        }
    }
}
