using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Empleados
    {
        // Método asincrónico para obtener la lista de empleados
        public async Task<List<Empleados>> Listar()
        {
            List<Empleados> lista = new List<Empleados>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT Id, Nombre, Noempleado, Departamento, Huella FROM Empleados";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    await oconexion.OpenAsync(); // Apertura asincrónica de la conexión

                    using (SqlDataReader dr = await cmd.ExecuteReaderAsync()) // Ejecución asincrónica del lector
                    {
                        while (await dr.ReadAsync()) // Lectura asincrónica
                        {
                            lista.Add(new Empleados()
                            {
                                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                                Nombre = dr.GetString(dr.GetOrdinal("Nombre")),
                                NoEmpleado = dr.GetInt32(dr.GetOrdinal("Noempleado")),
                                Departamento = dr.GetString(dr.GetOrdinal("Departamento")),
                                Huella = dr.IsDBNull(dr.GetOrdinal("Huella")) ? null : (byte[])dr["Huella"]
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Empleados>();
            }

            return lista;
        }
    }
}
