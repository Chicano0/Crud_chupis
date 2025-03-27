using System;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;

namespace CapPresentacionAdmin
{
    public partial class Iniciar_Sesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string email = Request.Form["email"]?.Trim();
                string password = Request.Form["password"]?.Trim();

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && AuthenticateUser(email, password))
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    Response.Redirect("Home");
                }
                else
                {
                    loginMessage.Text = "Correo electrónico o contraseña incorrectos. Inténtelo de nuevo.";
                    loginMessage.Visible = true;
                }
            }
        }

        private bool AuthenticateUser(string email, string password)
        {
            bool isValid = false;
            string connectionString = ConfigurationManager.ConnectionStrings["crud_chupis"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // La consulta ahora compara las contraseñas directamente, sin encriptación
                string query = "SELECT COUNT(*) FROM Admin WHERE email = @Email AND password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    conn.Open();
                    isValid = (int)cmd.ExecuteScalar() > 0;
                }
            }
            return isValid;
        }
    }
}