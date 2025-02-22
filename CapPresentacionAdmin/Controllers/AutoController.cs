using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CapPresentacionAdmin.Controllers
{
    public class AutoController : Controller
    {
        private readonly string connectionString = "Server=DESKTOP-CNF4DJ8\\SQLEXPRESS;Database=crud_chupis;Integrated Security=True;";

        public ActionResult Registro()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserEmail = User.Identity.Name;
                return View();
            }
            return RedirectToAction("Index", "Iniciar_Sesion");
        }

       public JsonResult ListaAutos()
{
    List<Vehiculo> autos = new List<Vehiculo>();
    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        string query = "SELECT Id, Propietario, Marca, Modelo, Año, Placa, EstadoRevision, FallaDescripcion, Tipo, Color FROM Vehiculos";
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            conn.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    autos.Add(new Vehiculo
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
    }
    return Json(new { data = autos }, JsonRequestBehavior.AllowGet);
}
        [HttpPost]
        public JsonResult InsertarAuto(Vehiculo auto)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO Vehiculos (Propietario, Marca, Modelo, Año, Placa, EstadoRevision, Tipo, Color, FallaDescripcion) " +
                                       "VALUES (@Propietario, @Marca, @Modelo, @Año, @Placa, @EstadoRevision, @Tipo, @Color, @FallaDescripcion)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
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
                    return Json(new { success = true, message = "Vehículo agregado correctamente." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error al insertar el auto: " + ex.Message });
                }
            }
            return Json(new { success = false, message = "Usuario no autenticado." });
        }

        [HttpPost]
        public JsonResult EditarAuto(Vehiculo auto)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Vehiculos SET Propietario = @Propietario, Marca = @Marca, Modelo = @Modelo, " +
                                       "Año = @Año, Placa = @Placa, EstadoRevision = @EstadoRevision, Tipo = @Tipo, " +
                                       "Color = @Color, FallaDescripcion = @FallaDescripcion WHERE Id = @Id";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", auto.Id);
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
                    return Json(new { success = true, message = "Vehículo actualizado correctamente." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error al editar el auto: " + ex.Message });
                }
            }
            return Json(new { success = false, message = "Usuario no autenticado." });
        }


        [HttpPost]
        public JsonResult EliminarAuto(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Vehiculos WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    return Json(new { success = true, message = "Auto eliminado correctamente." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error al eliminar el auto: " + ex.Message });
                }
            }
            return Json(new { success = false, message = "Usuario no autenticado." });
        }
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
