<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPFinalNivel3.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-4">
            <h2 class="text-center">Crear Cuenta</h2>
            <div class="mb-3">
                <label class="form-label">Email (será tu usuario)</label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label class="form-label">Contraseña</label>
                <asp:TextBox runat="server" ID="txtPassword" IsRequired="true" TextMode="Password" CssClass="form-control" />
            </div>
            <hr />
            <asp:Button Text="Registrarse" ID="btnRegistro" OnClick="btnRegistro_Click" CssClass="btn btn-primary w-100" runat="server" />
            <a href="Login.aspx" class="d-block text-center mt-2">¿Ya tienes cuenta? Inicia sesión</a>
        </div>
    </div>
</asp:Content>
