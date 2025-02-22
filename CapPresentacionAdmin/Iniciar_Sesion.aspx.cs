using System;
using System.Data.SqlClient;
using System.Web.Security;

namespace CapPresentacionAdmin
{
    public partial class Iniciar_Sesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                string email = Request.Form["email"];
                string password = Request.Form["password"];

                if (AuthenticateUser(email, password))
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    Response.Redirect("Home");
                }
                else
                {
                    // Pasar el mensaje de error a la página
                    loginMessage.Text = "Correo electrónico o contraseña incorrectos Intentelo De Nuevo.";
                    loginMessage.Visible = true;
                }
            }
        }

        private bool AuthenticateUser(string email, string password)
        {
            bool isValid = false;
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["crud_chupis"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Admin WHERE email = @Email AND password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    isValid = (count > 0);
                }
            }
            return isValid;
        }
    }
}
