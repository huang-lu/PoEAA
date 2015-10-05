using System;
using System.Data;

namespace AssociationTableMapping
{
    class HabilidadMapper : MapperAbstracto
    {
        protected override string NombreTabla
        {
            get
            {
                return "Habilidad";
            }
        }

        public Habilidad Buscar(long id)
        {
            return (Habilidad)BuscarAbstracto(id);
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Habilidad();
        }

        protected override string Filtro(long id)
        {
            return String.Format("HabilidadId = {0}", id);
        }

        protected override void Guardar(ObjetoDominio objeto, DataRow fila)
        {
            Habilidad habilidad = (Habilidad)objeto;
            fila["Nombre"] = habilidad.Nombre;
            dsh.guardarABd("Habilidad");
        }

        protected override void HacerCarga(ObjetoDominio objeto, DataRow fila)
        {
            Habilidad habilidad = (Habilidad)objeto;
            habilidad.Nombre = (string)fila["Nombre"];
        }
    }
}