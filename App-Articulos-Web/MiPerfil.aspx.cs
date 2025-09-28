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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sesionActiva(Session["usuario"]))
                    {
                        User usuario = (User)Session["usuario"];
                        tbEmail.Text = usuario.Email;
                        tbEmail.ReadOnly = true;
                        tbNombre.Text = usuario.Nombre;
                        tbApellido.Text = usuario.Apellido;
                        if (!string.IsNullOrEmpty(usuario.UrilImagenPerfil))
                            imgNuevoPerfil.ImageUrl = "~/Images/" + usuario.UrilImagenPerfil;
                    }
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                NegocioUser negocio = new NegocioUser();
               
                User usuario = (User)Session["usuario"];
                if(tbImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Images/");
                    tbImagen.PostedFile.SaveAs(ruta + "perfil-" + usuario.Id + ".jpg");
                    usuario.UrilImagenPerfil = "perfil-" + usuario.Id + ".jpg";
                }
                usuario.Nombre = tbNombre.Text;
                usuario.Apellido = tbApellido.Text;
                negocio.actualizar(usuario);
                Image img = (Image)Master.FindControl("imgPerfil");
                img.ImageUrl = "~/Images/" + usuario.UrilImagenPerfil;
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
        }
    }
}