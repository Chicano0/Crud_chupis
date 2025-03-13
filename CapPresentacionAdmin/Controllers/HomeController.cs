using System;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CapPresentacionAdmin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private string connectionString = "Server=DESKTOP-CNF4DJ8\\SQLEXPRESS;Database=Crud_chupis;Integrated Security=True;";

        public ActionResult Index()
        {
            ViewBag.UserEmail = User.Identity.Name;
            return View();
        }

        // GET: TotalEmpleados
        [HttpGet]
        public JsonResult TotalEmpleados()
        {
            if (User.Identity.IsAuthenticated)
            {
                int totalEmpleados = ObtenerTotalEmpleados(); // Call method to get total
                return Json(new { success = true, totalEmpleados }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Usuario no autenticado" }, JsonRequestBehavior.AllowGet);
            }
        }

        // Method to count employees
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

        [HttpPost]
        public JsonResult Logout()
        {
            try
            {
                // Opcional: Realiza cualquier limpieza necesaria del lado del servidor
                System.Web.Security.FormsAuthentication.SignOut();

                return Json(new { success = true, redirectUrl = "/Iniciar_Sesion.aspx" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al cerrar sesión: " + ex.Message });
            }
        }

    }
}
