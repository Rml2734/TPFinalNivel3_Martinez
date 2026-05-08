<%@ Page Title="Login" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPFinalNivel3.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Aquí solo van scripts o estilos específicos del head -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-4">
            <h2 class="text-center">Iniciar Sesión</h2>
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label class="form-label">Contraseña</label>
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <%-- Este Label estará oculto por defecto --%>
                <asp:Label ID="lblError" runat="server" CssClass="text-danger small fw-bold" Visible="false" />
            </div>
            <asp:Button Text="Ingresar" ID="btnLogin" OnClick="btnLogin_Click" CssClass="btn btn-primary w-100" runat="server" />
        </div>
    </div>
</asp:Content>
