using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace App_Articulos_Web
{
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            string email = tbEmail.Text.Trim();
            NegocioUser negocio = new NegocioUser();
            try
            {
                if (negocio.existeCorreo(email))
                {
                    lblError.Text = "Ya existe un registro con este correo. Elige otro.";
                    return;
                }
                User usuario = new User();
                
                usuario.Email = tbEmail.Text;
                usuario.Pass = tbPass.Text;
                usuario.Id = negocio.insertarNuevo(usuario);
                Session.Add("usuario", usuario);

                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
        }
    }
}