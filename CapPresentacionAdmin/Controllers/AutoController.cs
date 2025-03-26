using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CapPresentacionAdmin.Controllers
{
    public class AutoController : Controller
    {
        private const string ConnectionString = "Server=DESKTOP-CNF4DJ8\\SQLEXPRESS;Database=crud_chupis;Integrated Security=True;";

        public ActionResult Registro()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Iniciar_Sesion");

            ViewBag.UserEmail = User.Identity.Name;
            return View();
        }

        public JsonResult ListaAutos()
        {
            try
            {
                var vehiculos = ObtenerVehiculosDesdeBD();
                return Json(new { data = vehiculos }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al obtener vehículos: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult InsertarAuto(Vehiculo auto)
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { success = false, message = "Usuario no autenticado." });

            try
            {
                InsertarVehiculoEnBD(auto);
                return Json(new { success = true, message = "Vehículo agregado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al insertar el auto: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult EditarEstadoRevision(Vehiculo auto)
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { success = false, message = "Usuario no autenticado." });

            try
            {
                ActualizarEstadoRevisionEnBD(auto);
                return Json(new { success = true, message = "Estado de Revisión actualizado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al editar el estado: {ex.Message}" });
            }
        }

        [HttpPost]
        public JsonResult EliminarAuto(int id)
        {
            if (!User.Identity.IsAuthenticated)
                return Json(new { success = false, message = "Usuario no autenticado." });

            try
            {
                EliminarVehiculoDeBD(id);
                return Json(new { success = true, message = "Auto eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al eliminar el auto: {ex.Message}" });
            }
        }

        #region Métodos Privados
        private List<Vehiculo> ObtenerVehiculosDesdeBD()
        {
            var vehiculos = new List<Vehiculo>();
            const string query = "SELECT Id, Propietario, Marca, Modelo, Año, Placa, EstadoRevision, FallaDescripcion, Tipo, Color FROM Vehiculos";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vehiculos.Add(new Vehiculo
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Propietario = reader["Propietario"].ToString(),
                            Marca = reader["Marca"].ToString(),
                            Modelo = reader["Modelo"].ToString(),
                            Año = Convert.ToInt32(reader["Año"]),
                            Placa = reader["Placa"].ToString(),
                            EstadoRevision = reader["EstadoRevision"].ToString(),
                            FallaDescripcion = reader["FallaDescripcion"].ToString(),
                            Tipo = reader["Tipo"].ToString(),
                            Color = reader["Color"].ToString()
                        });
                    }
                }
            }
            return vehiculos;
        }

        private void InsertarVehiculoEnBD(Vehiculo auto)
        {
            const string query = @"INSERT INTO Vehiculos 
                                (Propietario, Marca, Modelo, Año, Placa, EstadoRevision, Tipo, Color, FallaDescripcion) 
                                VALUES 
                                (@Propietario, @Marca, @Modelo, @Año, @Placa, @EstadoRevision, @Tipo, @Color, @FallaDescripcion)";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Propietario", auto.Propietario);
                cmd.Parameters.AddWithValue("@Marca", auto.Marca);
                cmd.Parameters.AddWithValue("@Modelo", auto.Modelo);
                cmd.Parameters.AddWithValue("@Año", auto.Año);
                cmd.Parameters.AddWithValue("@Placa", auto.Placa);
                cmd.Parameters.AddWithValue("@EstadoRevision", auto.EstadoRevision);
                cmd.Parameters.AddWithValue("@Tipo", auto.Tipo);
                cmd.Parameters.AddWithValue("@Color", auto.Color);
                cmd.Parameters.AddWithValue("@FallaDescripcion", auto.FallaDescripcion);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void ActualizarEstadoRevisionEnBD(Vehiculo auto)
        {
            const string query = "UPDATE Vehiculos SET EstadoRevision = @EstadoRevision WHERE Id = @Id";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", auto.Id);
                cmd.Parameters.AddWithValue("@EstadoRevision", auto.EstadoRevision);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void EliminarVehiculoDeBD(int id)
        {
            const string query = "DELETE FROM Vehiculos WHERE Id = @Id";

            using (var conn = new SqlConnection(ConnectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }

    public class Vehiculo
    {
        public int Id { get; set; }
        public string Propietario { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public string Placa { get; set; }
        public string EstadoRevision { get; set; }
        public string Tipo { get; set; }
        public string Color { get; set; }
        public string FallaDescripcion { get; set; }
    }
}