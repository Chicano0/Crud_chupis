using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Empleados
    {
        // Propiedad para el ID autoincremental
        public int Id { get; set; }

        // Propiedad para el nombre del empleado
        public string Nombre { get; set; }

        // Propiedad para el número de empleado
        public int NoEmpleado { get; set; }

        // Propiedad para el departamento
        public string Departamento { get; set; }

        // Propiedad para almacenar la huella digital como arreglo de bytes
        public byte[] Huella { get; set; }
    }

}
