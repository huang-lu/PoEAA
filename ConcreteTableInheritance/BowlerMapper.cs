using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcreteTableInheritance
{
    class BowlerMapper : CricketerMapper
    {
        public BowlerMapper(Gateway gatW) : base(gatW) { }

        public override string NombreTabla
        {
            get
            {
                return "Bowler";
            }
        }

        new public Bowler Buscar(long id)
        {
            return (Bowler)BuscarAbstracto(id);
        }

        protected override void Cargar(ObjetoDominio objeto, DataRow fila)
        {
            base.Cargar(objeto, fila);
            Bowler cricketer = (Bowler)objeto;
            cricketer.mediaBowling = (int)fila["MediaBowling"];
        }

        protected override void Guardar(ObjetoDominio objeto, DataRow fila)
        {
            base.Guardar(objeto, fila);
            Bowler bowler = (Bowler)objeto;
            fila["MediaBowling"] = bowler.mediaBowling;
            gateway.guardarABd(NombreTabla);
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Bowler();
        }
    }
}
