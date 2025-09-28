<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="App_Articulos_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .custom-card {
            border: none;
            border-radius: 15px;
            overflow: hidden;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            background-color: #fff;
            width: 18rem;
            display: flex;
            flex-direction: column;
            height: 100%;
        }

        .row-cols-1.row-cols-md-3.g-4{
            display: flex;
            flex-wrap: wrap;
            gap: 1rem;
        }
            .custom-card:hover {
                transform: translateY(-8px) scale(1.03);
                box-shadow: 0 12px 20px rgba(0,0,0,0.2);
            }
            .card-img-top{
                height: 200px;
                object-fit: cover;
                width: 100%;
            }

        .card-img-wrapper {
            height: 200px;
            overflow: hidden;
        }

            .card-img-wrapper img {
                transition: transform 0.4s ease;
                width: 100%;
                height: 100%;
                object-fit: fill;
            }

        .custom-card:hover .card-img-wrapper img {
            transform: scale(1.1);
        }

        .custom-card {
            
            display: flex;
            flex-direction: column;
            height: 100%;
        }
        .card-body{
            flex-grow: 1;
        }


        .bienvenida {
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

        .precio {
            font-weight: bold;
        }

        @keyframes fadeInDown {
            from {
                opacity: 0;
                transform: translateY(-20px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(20px);
            }

            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

    </style>



    <h1 class="bienvenida">Hola! Bienvenido!</h1>
    <p class="descripcion">Esta es la pantalla de inicio de la app web de Articulos.</p>

    <asp:UpdatePanel ID="upArticulos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
           
         <%/*   <asp:Label Text="Buscar: "  runat="server" CssClass="form-label me-2 mb-0" />*/%>
            <asp:TextBox runat="server"  placeholder="Buscar" ID="txtFiltro"  AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" CssClass="form-control form-control-lg me-2 search-input"  />
            <asp:CheckBox Text="Filtro Avanzado" runat="server" ID="cbFiltroAvanzado" AutoPostBack="true" OnCheckedChanged="cbFiltroAvanzado_CheckedChanged" CssClass="form-check-label fw-bold text-secondary" />


            <%if (FiltroAvanzado)
                {%>
            <div class="row">
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Campo" ID="lblCampo" runat="server" />
                        <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddl_Campo" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                            <asp:ListItem Text="" />
                            <asp:ListItem Text="Nombre" />
                            <asp:ListItem Text="Precio" />
                            <asp:ListItem Text="Categoria" />
                            <asp:ListItem Text="Marca" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Criterio" runat="server" />
                        <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label Text="Filtro" runat="server"></asp:Label>
                        <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" />
                    </div>
                    <div class="mb-3">
                        <asp:Button Text="Eliminar Filtro" runat="server" CssClass="btn btn-outline-danger" ID="btnEliminarFiltro" OnClick="btnEliminarFiltro_Click" />
                    </div>
                </div>
            </div>
            <%}  %>


            <div class="row row-cols-1 row-cols-md-3 g-4">


                <asp:Repeater runat="server" ID="rpRepetidor" OnItemDataBound="rpRepetidor_ItemDataBound" OnItemCommand="rpRepetidor_ItemCommand">
                    <ItemTemplate>
                        <div class="card custom-card h-100">
                            <div class="card-img-wrapper">
                                <img src="<%#Eval("UrlImagen") %>" class="card-img-top" alt="...">
                            </div>

                            <div class="card-body">

                                <h1 class="card-title"><%#Eval("Nombre")%></h1>
                                <h5 class="precio">$ <%#Eval("Precio") %></h5>
                                <asp:LinkButton ID="btnFavorito" runat="server" CssClass="btn btn-outline-info" 
                                    CommandName="AgregarFavorito" CommandArgument='<%#Eval("Id")%>' >
                                    <i class='<%#(ViewState["FavoritosIds"] != null && ((List<int>)ViewState["FavoritosIds"]).Contains((int)Eval("Id"))) ? "bi bi-star-fill" : "bi bi-star" %>'></i>
                                    </asp:LinkButton>


                            </div>
                            <div class="card-footer bg-transparent border-0">
                                <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>" class="btn btn-primary">Ver Detalles</a>
                                
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtFiltro" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="cbFiltroAvanzado" EventName="CheckedChanged" />
        </Triggers>
    </asp:UpdatePanel>

    
</asp:Content>
