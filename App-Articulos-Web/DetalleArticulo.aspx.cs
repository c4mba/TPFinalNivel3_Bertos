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
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int idArti;
                    if (int.TryParse(Request.QueryString["id"], out idArti))
                        cargarDetalle(idArti);
                    else
                        Response.Redirect("Default.aspx");
                }
            }
        }

        private void cargarDetalle(int id)
        {
            NegocioArticulo negocio = new NegocioArticulo();
            Articulo arti = negocio.obtenerArticulo(id);

            if (arti != null)
            {
                lblNombre.Text = arti.Nombre;
                lblDescr.Text = arti.Descripcion;
                lblMarca.Text = arti.marca.Descripcion;
                lblCat.Text = arti.categoria.Descripcion;
                lblPrecio.Text = arti.Precio.ToString();
                imgArticulo.ImageUrl = string.IsNullOrEmpty(arti.UrlImagen) ? "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png" : arti.UrlImagen;
            }
        }
    }
}