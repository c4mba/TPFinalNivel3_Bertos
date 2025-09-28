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
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null)
                {
                    Response.Redirect("Login.aspx");
                }

                cargarFavoritos();
            }
        } 

        void cargarFavoritos()
        {
            User usuario = (User)Session["usuario"];
            int idUser = usuario.Id;

            NegocioArticulo negArt = new NegocioArticulo();
            List<Articulo> articulos = negArt.listar();

            NegocioFavorito favneg = new NegocioFavorito();
            List<int> favoritosIds = favneg.listarFavoritos(idUser).Select(a => a.Id).ToList();
            ViewState["FavoritosIds"] = favoritosIds;

            List<Articulo> artfavs = articulos.Where(a => favoritosIds.Contains(a.Id)).ToList();


            rpFavoritos.DataSource = artfavs;
            rpFavoritos.DataBind();
        }

        protected void clk_Elimina_Fav(object sender, EventArgs e)
        {
            
        }

        protected void rpFavoritos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "EliminarFavorito")
            {
                int idArti = Convert.ToInt32(e.CommandArgument);
                User usuario = (User)Session["usuario"];
                int idUser = usuario.Id;

                NegocioFavorito negFav = new NegocioFavorito();
                negFav.eliminarFavorito(idUser, idArti);

                cargarFavoritos();
            }
        }
    }
}