using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTableInheritance
{
    class BowlerMapper : CricketerMapper
    {
        new public const string TIPO = "B";
        public override string Tipo
        {
            get { return TIPO; }
        }
        protected new static string NombreTabla = "Bowler";

        public BowlerMapper(Gateway gateway) : base(gateway) { }
        
        new public Bowler Buscar(long id)
        {
            return (Bowler)base.BuscarAbstracto(id, NombreTabla);
        }

        protected override void Cargar(ObjetoDominio objeto)
        {
            base.Cargar(objeto);
            DataRow fila = BuscarFila(objeto.id, Tabla(NombreTabla));
            Bowler bowler = (Bowler)objeto;
            bowler.mediaBowling = (int)fila["MediaBowling"];            
        }

        protected override void Guardar(ObjetoDominio objeto)
        {
            base.Guardar(objeto);
            DataRow fila = BuscarFila(objeto.id, Tabla(NombreTabla));
            Bowler bowler = (Bowler)objeto;
            fila["MediaBowling"] = bowler.mediaBowling;
            gateway.guardarABd(NombreTabla);
        }

        protected override void AnadirFila(ObjetoDominio objeto)
        {
            base.AnadirFila(objeto);
            InsertarFila(objeto, Tabla(NombreTabla));
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Bowler();
        }
    }
}
