using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTableInheritance
{
    class CricketerMapper : JugadorAbstractoMapper
    {
        public const string TIPO = "C";
        public override string Tipo
        {
            get { return TIPO; }
        }

        protected new static string NombreTabla = "Cricketer";

        public CricketerMapper(Gateway gateway) : base(gateway) { }
        
        public Cricketer Buscar(long id)
        {
            return (Cricketer)BuscarAbstracto(id, NombreTabla);
        }

        protected override void Cargar(ObjetoDominio objeto)
        {
            base.Cargar(objeto);
            DataRow fila = BuscarFila(objeto.id, Tabla(NombreTabla));
            Cricketer cricketer = (Cricketer)objeto;
            cricketer.mediaBateo = (int)fila["MediaBateo"];
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Cricketer();
        }

        protected override void Guardar(ObjetoDominio objeto)
        {
            base.Guardar(objeto);
            DataRow fila = BuscarFila(objeto.id, Tabla(NombreTabla));
            Cricketer cricketer = (Cricketer)objeto;
            fila["MediaBateo"] = cricketer.mediaBateo;
            gateway.guardarABd(NombreTabla);
        }

        protected override void AnadirFila(ObjetoDominio objeto)
        {
            base.AnadirFila(objeto);
            InsertarFila(objeto, Tabla(NombreTabla));
        }
    }
}
