using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationTableMapping
{
    class EmpleadoMapper : MapperAbstracto
    {
        protected override string NombreTabla
        {
            get
            {
                return "Empleado";
            }
        }
        private DataTable tablaHabilidades
        {
            get { return dsh.Datos.Tables["HabilidadesEmpleados"]; }
        }
        public Empleado Buscar(long id)
        {
            return (Empleado)BuscarAbstracto(id);
        }

        protected override ObjetoDominio CrearObjetoDominio()
        {
            return new Empleado();
        }

        protected override void Guardar(ObjetoDominio objeto, DataRow fila)
        {
            Empleado empleado = (Empleado)objeto;
            fila["Nombre"] = empleado.Nombre;
            GuardarHabilidades(empleado);
            dsh.guardarABd("Empleado");
        }

        private void GuardarHabilidades(Empleado empleado)
        {
            EliminarHabilidades(empleado);
            foreach (Habilidad habilidad in empleado.Habilidades)
            {
                DataRow fila = tablaHabilidades.NewRow();
                fila["EmpleadoId"] = empleado.id;
                fila["HabilidadId"] = habilidad.id;
                tablaHabilidades.Rows.Add(fila);
            }
            dsh.guardarABd("HabilidadesEmpleados");
        }

        private void EliminarHabilidades(Empleado empleado)
        {
            DataRow[] filasHabilidades = FilasHabilidades(empleado);
            foreach (DataRow fila in filasHabilidades)
            {
                fila.Delete();
            }
        }

        protected override void HacerCarga(ObjetoDominio objeto, DataRow fila)
        {
            Empleado empleado = (Empleado)objeto;
            empleado.Nombre = (string)fila["Nombre"];
            CargarHabilidades(empleado);
        }

        private IList CargarHabilidades(Empleado empleado)
        {
            DataRow[] filas = FilasHabilidades(empleado);
            foreach (DataRow fila in filas)
            {
                long habilidadId = (int)fila["HabilidadId"];
                empleado.AnadirHabilidad(MapperAbstracto.Habilidad.Buscar(habilidadId));
            }
            return empleado.Habilidades;
        }

        private DataRow[] FilasHabilidades(Empleado emplado)
        {
            string filtro = String.Format(Filtro(emplado.id));
            return tablaHabilidades.Select(filtro);
        }

        protected override string Filtro(long id)
        {
            return String.Format("EmpleadoId = {0}", id);
        }
    }
}
