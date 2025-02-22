using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CapPresentacionAdmin.Controllers
{
    public class EmpleadosController : Controller
    {
        private string connectionString = "Server=DESKTOP-CNF4DJ8\\SQLEXPRESS;Database=crud_chupis;Integrated Security=True;";

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.UserEmail = User.Identity.Name; 
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Iniciar_Sesion");
            }
        }

        public JsonResult ListarEmpleados()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Usuario no autenticado" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Empleado()
        {
            if (User.Identity.IsAuthenticated)
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT Id, Nombre, NoEmpleado, Departamento, Huella FROM empleados";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            conn.Open();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dt);
                            }
                        }
                    }

                    ViewBag.Empleados = dt;

                    ViewBag.TotalEmpleados = ObtenerTotalEmpleados(); // Llama al método para obtener el total
                }
                catch (SqlException ex)
                {
                    ViewBag.ErrorMessage = "Error de SQL: " + ex.Message;
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error general: " + ex.Message;
                }

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Iniciar_Sesion");
            }
        }

        // Método para obtener el total de empleados
        [HttpGet]
        public JsonResult TotalEmpleados()
        {
            if (User.Identity.IsAuthenticated)
            {
                int totalEmpleados = ObtenerTotalEmpleados();
                return Json(new { success = true, totalEmpleados }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Usuario no autenticado" }, JsonRequestBehavior.AllowGet);
            }
        }

        // Método privado para contar empleados
        private int ObtenerTotalEmpleados()
        {
            int totalEmpleados = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM empleados";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        totalEmpleados = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al contar empleados: " + ex.Message;
            }
            return totalEmpleados;
        }

        // POST: Empleados/EditarEmpleado
        [HttpPost]
        public ActionResult EditarEmpleado(int Id, string Nombre, string NoEmpleado, string Departamento)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE empleados SET Nombre = @Nombre, NoEmpleado = @NoEmpleado, Departamento = @Departamento WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", Id);
                            cmd.Parameters.AddWithValue("@Nombre", Nombre);
                            cmd.Parameters.AddWithValue("@NoEmpleado", NoEmpleado);
                            cmd.Parameters.AddWithValue("@Departamento", Departamento);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    TempData["SuccessMessage"] = "Empleado actualizado correctamente";
                    return RedirectToAction("Empleado");
                }
                catch (SqlException ex)
                {
                    TempData["ErrorMessage"] = "Error al actualizar el empleado: " + ex.Message;
                    return RedirectToAction("Empleado");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error general: " + ex.Message;
                    return RedirectToAction("Empleado");
                }
            }
            else
            {
                return RedirectToAction("Index", "Iniciar_Sesion");
            }
        }

        
     
        [HttpPost]
        public ActionResult InsertarEmpleado(string Nombre, string NoEmpleado, string Departamento)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO empleados (Nombre, NoEmpleado, Departamento) VALUES (@Nombre, @NoEmpleado, @Departamento)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Nombre", Nombre);
                            cmd.Parameters.AddWithValue("@NoEmpleado", NoEmpleado);
                            cmd.Parameters.AddWithValue("@Departamento", Departamento);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    TempData["SuccessMessage"] = "Empleado agregado correctamente";
                    return RedirectToAction("Empleado");
                }
                catch (SqlException ex)
                {
                    TempData["ErrorMessage"] = "Error al insertar el empleado: " + ex.Message;
                    return RedirectToAction("Empleado");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error general: " + ex.Message;
                    return RedirectToAction("Empleado");
                }
            }
            else
            {
                return RedirectToAction("Index", "Iniciar_Sesion");
            }
        }
        [HttpPost]
        public JsonResult EliminarEmpleado(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM empleados WHERE Id = @Id";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Id", id);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    return Json(new { success = true });
                }
                catch (SqlException ex)
                {
                    return Json(new { success = false, message = "Error SQL: " + ex.Message });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = "Error general: " + ex.Message });
                }
            }
            else
            {
                return Json(new { success = false, message = "Usuario no autenticado" });
            }
        }

    }
}
