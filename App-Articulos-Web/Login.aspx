<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="App_Articulos_Web.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div class="container mt-5">
       <div class="row justify-content-center">
           <div class="col-md-6">
               <div class="card shadow-lg rounded-3">
                   <div class="card-body p-4">
                       <h1 class="text-center mb-3">Inicia Sesión</h1>
                       <p class="text-center text-muted mb-4">Si ya tenés cuenta, completá el formulario con tus datos.</p>

                       <div class="mb-3">
                           <asp:Label Text="Email" CssClass="form-label" runat="server" />
                           <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control" />
                       </div>
                       <div class="mb-3">
                           <asp:Label Text="Password" runat="server" CssClass="form-label" />
                           <asp:TextBox runat="server" ID="tbPass" CssClass="form-control" TextMode="Password" />
                       </div>
                       <div class="d-flex justify-content-between">
                           <asp:Button Text="Ingresar" CssClass="btn btn-primary" runat="server" ID="btnLogin"  OnClick="btnLogin_Click"/>
                           <asp:Button Text="Cancelar" CssClass="btn btn-outline-danger" ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" />
                       </div>
                   </div>
               </div>
           </div>
       </div>
   </div>

</asp:Content>
