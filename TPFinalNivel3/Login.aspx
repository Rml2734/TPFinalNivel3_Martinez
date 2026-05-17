<%@ Page Title="Login" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TPFinalNivel3.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Aquí solo van scripts o estilos específicos del head -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center mt-5">
        <div class="col-md-4">

            <div class="shadow p-4 rounded  border border-light">

                <h2 class="text-center mb-4 fw-bold ">Iniciar Sesión</h2>
                <hr class="mb-4" />

                <div class="mb-3">
                    <label for="<%= txtEmail.ClientID %>" class="form-label fw-bold text-secondary">Email</label>
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="nombre@correo.com" />
                </div>

                <div class="mb-3">
                    <label for="<%= txtPassword.ClientID %>" class="form-label fw-bold text-secondary">Contraseña</label>
                    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control" placeholder="••••••••" />
                </div>

                <div class="mb-3">
                    <%-- Este Label estará oculto por defecto --%>
                    <asp:Label ID="lblError" runat="server" CssClass="text-danger small fw-bold" Visible="false" />
                </div>

                <div class="d-grid gap-2 mt-4">
                    <asp:Button Text="Ingresar al Sistema" ID="btnLogin" OnClick="btnLogin_Click" CssClass="btn btn-primary btn-lg rounded-pill shadow-sm" runat="server" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>
