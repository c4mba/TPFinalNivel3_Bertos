using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;

namespace App_Articulos_Web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> listaArticulo { get; set; }
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            FiltroAvanzado = cbFiltroAvanzado.Checked;
            if (!IsPostBack)
            {
                NegocioArticulo negocio = new NegocioArticulo();
                Session.Add("CartasArticulos", negocio.listar());
                rpRepetidor.DataSource = Session["CartasArticulos"];
                rpRepetidor.DataBind();
            }

        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["CartasArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            rpRepetidor.DataSource = listaFiltrada;
            rpRepetidor.DataBind();

        }

        protected void btnEjemplo_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;
        }

        protected void cbFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = cbFiltroAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();

            if (ddl_Campo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");

            }
            else
            {
                ddlCriterio.Items.Add("Contiene");
                ddlCriterio.Items.Add("Empieza con");
                ddlCriterio.Items.Add("Termina con");

            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                NegocioArticulo negocio = new NegocioArticulo();
                rpRepetidor.DataSource = negocio.filtrar(ddl_Campo.SelectedItem.ToString(), ddlCriterio.SelectedItem.ToString(), txtFiltroAvanzado.Text);
                rpRepetidor.DataBind();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
            }
        }

        protected void btnEliminarFiltro_Click(object sender, EventArgs e)
        {
            NegocioArticulo negocio = new NegocioArticulo();
            rpRepetidor.DataSource = negocio.listar();
            rpRepetidor.DataBind();

        }

        protected void rpRepetidor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        { 
           /*
          { if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Articulo arti = (Articulo)e.Item.DataItem;
                Image img = (System.Web.UI.WebControls.Image)e.Item.FindControl("UrlImagen");

                if (string.IsNullOrEmpty(arti.UrlImagen))
                    img.ImageUrl = "https://developers.elementor.com/docs/assets/img/elementor-placeholder-image.png";
                else
                    img.ImageUrl = arti.UrlImagen;

            }*/
        }

        protected void rpRepetidor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "AgregarFavorito")
            {
                int idArti = Convert.ToInt32(e.CommandArgument);
                User usuario = (User)Session["usuario"];
                int idUser = usuario.Id;

                NegocioFavorito favNeg = new NegocioFavorito();
                if (favNeg.esFavorito(idUser, idArti))
                {
                    favNeg.eliminarFavorito(idUser, idArti);
                }
                else
                {
                    favNeg.agregarFavorito(idUser, idArti);
                }

                List<int> favoritosIds = favNeg.listarFavoritos(idUser).Select(a => a.Id).ToList();
                ViewState["FavoritosIds"] = favoritosIds;

                NegocioArticulo artneg = new NegocioArticulo();
                List<Articulo> articulos = artneg.listar();

                
                
                 

                rpRepetidor.DataSource = articulos;
                rpRepetidor.DataBind();

               
            }
        }
    }
}