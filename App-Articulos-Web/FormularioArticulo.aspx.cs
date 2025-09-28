using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace App_Articulos_Web
{
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        public bool confirmaEliminacion {  get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            confirmaEliminacion = false;
            try
            {
                if (!IsPostBack)
                {
                    NegocioCategoria negocio1 = new NegocioCategoria();
                    NegocioMarca negocio2 = new NegocioMarca();

                    List<Categoria> listaCateg = negocio1.listar();
                    List<Marca> listaMarca = negocio2.listar();


                    ddlCategoria.DataSource = listaCateg;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                    ddlMarca.DataSource = listaMarca;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();
                }

                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    NegocioArticulo negocio = new NegocioArticulo();
                    Articulo seleccionado = (negocio.listar(id))[0];

                    txtId.Text = id;
                    txtNombre.Text = seleccionado.Nombre;
                    txtCodigo.Text = seleccionado.Codigo;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtImagenUrl.Text = seleccionado.UrlImagen;
                    txtPrecio.Text = seleccionado.Precio.ToString();

                    ddlCategoria.SelectedValue = seleccionado.categoria.Id.ToString();
                    ddlMarca.SelectedValue = seleccionado.marca.Id.ToString();
                    txtImagenUrl_TextChanged(sender, e);
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }

            
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                NegocioArticulo negocio = new NegocioArticulo();
                NegocioCategoria negocat = new NegocioCategoria();
                NegocioMarca negomarc = new NegocioMarca();

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.UrlImagen = txtImagenUrl.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);

                nuevo.categoria = new Categoria();
                nuevo.categoria.Id = int.Parse(ddlCategoria.SelectedValue);
                nuevo.marca = new Marca();
                nuevo.marca.Id = int.Parse(ddlMarca.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    negocio.modificar(nuevo);
                }
                else
                    negocio.agregar(nuevo);
                

                Response.Redirect("ListaArticulos.aspx", false);


            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
               
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgArti.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            confirmaEliminacion = true;

        }

        protected void btnConfirmaElim_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbConfirmar.Checked)
                {
                    NegocioArticulo negocio = new NegocioArticulo();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("ListaArticulos.aspx");
                }
               
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}