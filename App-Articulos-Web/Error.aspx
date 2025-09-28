<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="App_Articulos_Web.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container d-flex align-items-center justify-content-center min-vh-100">
    <div class="card shadow-lg border-0 rounded-3" style="max-width: 500px; width: 100%;">
        <div class="card-body text-center p-4">
            <div class="mb-3">
                <i class="bi bi-exclamation-triangle-fill text-danger" style="font-size: 3rem;"></i>
            </div>
            <h3 class="text-danger fw-bold mb-3">Ocurrió un error :(</h3>

            <asp:Label Text="text" ID="lblError" CssClass="text-muted small d-block mb-4" runat="server" />

            <a href="Default.aspx" class="btn btn-primary w-100">Volver al Inicio </a>
        </div>
    </div>
</div>
    
</asp:Content>
