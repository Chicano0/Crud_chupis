using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CapaEntidad;
using CapaDatos;

namespace CapaComedor
{
    public class Cn_Empleado
    {
        private CD_Empleados objCapaDato = new CD_Empleados();

        // Método asincrónico en la capa de negocio
        public async Task<List<Empleados>> Listar()
        {
            return await objCapaDato.Listar();
        }
    }
}
