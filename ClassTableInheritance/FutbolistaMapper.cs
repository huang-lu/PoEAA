using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTableInheritance
{
    class FutbolistaMapper : JugadorAbstractoMapper
    {
        public const string TIPO = "F";
        public override string Tipo
        {
            get { return TIPO; }
        }
        protected new static string NombreTabla = "Futbolista";

        public FutbolistaMapper(Gateway gateway) : base(gateway) { }
        
        public Futbolista Buscar(long id)
        {
            return (Futbolista)BuscarAbstracto(id, NombreTabla);
        }

        protected override void Cargar(ObjetoDominio objeto)
        {
            base.Cargar(objeto);
            DataRow fila = BuscarFila(objeto.id, Tabla(NombreTabla));
            Futbolista futbolista = (Futbolista)objeto;
            futbolista.club = (string)fila["Club"];
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Futbolista();
        }

        protected override void Guardar(ObjetoDominio objeto)
        {
            base.Guardar(objeto);
            DataRow fila = BuscarFila(objeto.id, Tabla(NombreTabla));
            Futbolista futbolista = (Futbolista)objeto;
            fila["Club"] = futbolista.club;
            gateway.guardarABd(NombreTabla);
        }

        protected override void AnadirFila(ObjetoDominio objeto)
        {
            base.AnadirFila(objeto);
            InsertarFila(objeto, Tabla(NombreTabla));
        }
    }
}
