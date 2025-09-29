using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace App_Articulos_Web
{
    public partial class ListaArticulos : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            FiltroAvanzado = cbFiltroAvanzado.Checked;

            if (Session["usuario"] == null || !Seguridad.esAdmin(Session["usuario"]))
            {
                Session["error"] = "Se requieren permisos de administrador para acceder a esta pantalla";
              
                Response.Redirect("Error.aspx");
                return; 
            }

            if (!IsPostBack)
            {
                NegocioArticulo negocio = new NegocioArticulo();
                Session["ListaArticulos"] = negocio.listar();
                // Session.Add("ListaArticulos", negocio.listar());
                dgvArticulos.DataSource = Session["ListaArticulos"];
                dgvArticulos.DataBind();
            }

            FiltroAvanzado = cbFiltroAvanzado.Checked;
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            var lista = Session["ListaArticulosFiltrada"] ?? Session["ListaArticulos"];
            dgvArticulos.DataSource = lista;
            dgvArticulos.DataBind();
        }

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List < Articulo >) Session["ListaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            Session["ListaArticulosFiltrada"] = listaFiltrada;
            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.PageIndex = 0;
            dgvArticulos.DataBind();
        }

        protected void cbFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = cbFiltroAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
        }

        protected void ddl_Campo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if(ddl_Campo.SelectedItem.ToString() == "Precio")
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
                dgvArticulos.DataSource = negocio.filtrar(ddl_Campo.SelectedItem.ToString(), ddlCriterio.SelectedItem.ToString(), txtFiltroAvanzado.Text);
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }
        }

        protected void btnEliminarFiltro_Click(object sender, EventArgs e)
        {
            NegocioArticulo negocio = new NegocioArticulo();
            dgvArticulos.DataSource = negocio.listar();
            dgvArticulos.DataBind();
        }
    }
}
