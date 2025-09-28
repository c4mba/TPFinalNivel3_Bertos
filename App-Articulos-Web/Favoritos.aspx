<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="App_Articulos_Web.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .titulo {
    font-size: 2.5rem;
    font-weight: 700;
    color: #2c3e50;
    text-align: center;
    margin-top: 30px;
    animation: fadeInDown 0.8s ease-in-out;
}

.descripcion {
    font-size: 1.1rem;
    color: #555;
    text-align: center;
    max-width: 700px;
    margin: 15px auto 40px auto;
    line-height: 1.6;
    animation: fadeInUp 1s ease-in-out;
}
    </style>

    <h1 class="titulo">Favoritos ⭐</h1>
    <p class="descripcion">En esta lista verás tus artículos favoritos!</p>

    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater runat="server" ID="rpFavoritos" OnItemCommand="rpFavoritos_ItemCommand">
            <ItemTemplate>
                <div class="card custom-card h-100">
                    <div class="card-img-wrapper">
                        <img src="<%#Eval("UrlImagen") %>" class="card-img-top" alt="..." />
                    </div>
                    <div class="card-body">
                        <h1 class="card-title"><%#Eval("Nombre") %></h1>
                        <h5 class="precio">$ <%#Eval("Precio") %></h5>

                    </div>
                    <div class="card-footer bg-transparent border-0">
                        <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>" class="btn btn-primary">Ver Detalles</a>

                        <div>
                            <asp:LinkButton ID="btnEliminarFav" runat="server" CssClass="btn btn-danger"
                                CommandName="EliminarFavorito" CommandArgument='<%# Eval("Id") %>'>
                <i class="bi bi-trash"></i> Quitar de favoritos
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
