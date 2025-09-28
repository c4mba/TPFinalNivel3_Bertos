using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace App_Articulos_Web
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgPerfil.ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/8/89/Portrait_Placeholder.png";
            if (!(Page is Login || Page is Registro || Page is Default || Page is Error || Page is DetalleArticulo))
            {
                if (!Seguridad.sesionActiva(Session["usuario"]))
                    Response.Redirect("Login.aspx", false);
                else
                {
                    mostrarUsuario();
                }
            }
            else
            {
                if (Seguridad.sesionActiva(Session["usuario"]))
                    mostrarUsuario();
            }

            if (Seguridad.sesionActiva(Session["usuario"]))
            {
               

                btnLogin.Visible = false;
                btnRegis.Visible = false;
                btn_Logout.Visible = true;
                lblUser.Visible = true;
            }
            else
            {

                btnLogin.Visible = true;
                btnRegis.Visible = true;
                btn_Logout.Visible = false;
                lblUser.Visible = false;
            }
        }

        protected void btnLogout_click(object sender, EventArgs e)
        {

        }

        protected void btn_Logout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        private void mostrarUsuario()
        {
            User usuario = (User)Session["usuario"];
            lblUser.Text = "Bienvenido " + usuario.Email + "!";
            if (!string.IsNullOrEmpty(usuario.UrilImagenPerfil))
                imgPerfil.ImageUrl = "~/Images/" + usuario.UrilImagenPerfil;
        }
    }


}