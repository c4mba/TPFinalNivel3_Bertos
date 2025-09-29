<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ListaArticulos.aspx.cs" Inherits="App_Articulos_Web.ListaArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Lista de articulos</h1>

    <asp:TextBox placeholder="Buscar..." runat="server" ID="txtFiltro" AutoPostBack="true" OnTextChanged="filtro_TextChanged" />
    <asp:CheckBox Text="Filtro avanzado" ID="cbFiltroAvanzado" AutoPostBack="true" OnCheckedChanged="cbFiltroAvanzado_CheckedChanged"
        CssClass="form-check-label fw-bold text-secondary" runat="server" />

    <%if (FiltroAvanzado)
        {  %>
    <div class="row">
        <div class="col-3">
            <div class="mb-3">
                <asp:Label Text="Campo" ID="lblCampo" runat="server" />
                <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddl_Campo"  OnSelectedIndexChanged="ddl_Campo_SelectedIndexChanged">
                    <asp:ListItem Text="Selecciona una opción." />
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
            <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
        </div>
    </div>
    </div>
    <div class="row">
        <div class="col-3">
            <div class="mb-3">
                <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" ID="btnBuscar"  OnClick="btnBuscar_Click" />
            </div>
            <div class="mb-3">
                 <asp:Button Text="Eliminar Filtro" runat="server" CssClass="btn btn-outline-danger" ID="btnEliminarFiltro"  OnClick="btnEliminarFiltro_Click" />
            </div>
        </div>
    </div>
    <%} %>

    <asp:GridView ID="dgvArticulos" CssClass="table" AutoGenerateColumns="false" runat="server"
        DataKeyNames="Id" OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged" OnPageIndexChanging="dgvArticulos_PageIndexChanging"
        AllowPaging="true" PageSize="5">
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" />
            <asp:BoundField HeaderText="Tipo" DataField="Categoria.Descripcion" />
            <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍" />
        </Columns>
    </asp:GridView>
    <a href="FormularioArticulo.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
