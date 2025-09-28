<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="App_Articulos_Web.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .detalle-texto {
            font-size: 1.2rem;
        }
        .mb-2 {
            font-weight: bold;
        }
        .valor {
            font-weight: normal;
        }
        .card-detalle {
            min-height: 500px;
        }
        .img-detalle {
            object-fit: contain;
            width: 100%;
            height: 100%;
            max-height: 500px;
        }
    </style>

    <div class="container mt-4">
        <div class="row">
            <div class="col-12">
                <div class="card shadow-lg border-0 rounded-4 overflow-hidden">
                    <div class="row g-0">
                        <div class="col-md-6 d-flex align-items-center bg-light p-3 card-detalle">
                            <asp:Image ID="imgArticulo" runat="server" CssClass="img-fluid rounded-start img-detalle" AlternateText="Imagen no disponible" />
                        </div>

                        <div class="col-md-6">
                            <div class="card-body p-4">
                                <h3 class="card-title fw-bold text-primary mb-3">
                                    <asp:Label ID="lblNombre" runat="server" />
                                </h3>
                                <p class="mb-3 detalle-texto">
                                    <span class="text-success fs-4 fw-bold">$<asp:Label ID="lblPrecio" runat="server" />
                                    </span>
                                </p>
                                <p class="mb-2 detalle-texto">
                                    Descripcion: 
                                    <asp:Label CssClass="valor" ID="lblDescr" runat="server" />
                                </p>

                                <p class="mb-2 detalle-texto">
                                    Marca: 
                                    <asp:Label CssClass="valor" ID="lblMarca" runat="server"></asp:Label>
                                </p>
                                <p class="mb-2 detalle-texto">
                                    Categoria: 
                                    <asp:Label CssClass="valor" ID="lblCat" runat="server" />
                                </p>

                                <a href="Default.aspx" class="btn btn-outline-secondary w-100 mt-4">Volver al catálogo</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
