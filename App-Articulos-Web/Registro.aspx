<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="App_Articulos_Web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <div class="card shadow-lg rounded-3">
                    <div class="card-body p-4">
                        <h1 class="text-center mb-3">Creá tu perfil</h1>
                        <p class="text-center text-muted mb-4">Completá el siguiente formulario para registrarte como usuario.</p>

                        <div class="mb-3">
                            <asp:Label Text="Email" CssClass="form-label" runat="server" />
                            <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control" />
                            <asp:Label ID="lblError"  CssClass="text-danger" runat="server" />
                        </div>
                        <div class="mb-3">
                            <asp:Label Text="Password" runat="server" CssClass="form-label" />
                            <asp:TextBox runat="server" ID="tbPass" CssClass="form-control" TextMode="Password" />
                        </div>
                        <div class="d-flex justify-content-between">
                            <asp:Button Text="Registrarse" CssClass="btn btn-primary" runat="server" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" />
                            <asp:Button Text="Cancelar" CssClass="btn btn-outline-danger" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>













</asp:Content>
