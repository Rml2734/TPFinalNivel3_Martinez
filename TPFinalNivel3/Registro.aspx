<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPFinalNivel3.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center">
        <div class="col-md-4">
            <h2 class="text-center">Crear Cuenta</h2>

            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" />

                <%-- 1. Valida que no esté vacío --%>
                <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio."
                    ControlToValidate="txtEmail" runat="server" CssClass="text-danger" Display="Dynamic" />

                <%-- 2. Valida que tenga formato de correo (con @ y .) --%>
                <asp:RegularExpressionValidator ErrorMessage="Formato de email inválido (ej: usuario@correo.com)."
                    ControlToValidate="txtEmail"
                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                    runat="server" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Contraseña</label>
                <asp:TextBox runat="server" ID="txtPassword" title="Password" CssClass="form-control" TextMode="Password" />
                <%-- Valida que no esté vacía --%>
                <asp:RequiredFieldValidator ErrorMessage="La contraseña es obligatoria."
                    ControlToValidate="txtPassword" runat="server" CssClass="text-danger" Display="Dynamic" />
            </div>

            <div class="mb-3">
                <label class="form-label">Repetir Contraseña</label>
                <asp:TextBox runat="server" ID="txtPasswordConfirm" title="Confirm Password" CssClass="form-control" TextMode="Password" />
                <%-- 1. Valida que no esté vacía --%>
                <asp:RequiredFieldValidator ErrorMessage="Debes repetir la contraseña."
                    ControlToValidate="txtPasswordConfirm" runat="server" CssClass="text-danger" Display="Dynamic" />

                <%-- 2. Compara que AMBAS contraseñas sean iguales --%>
                <asp:CompareValidator ErrorMessage="Las contraseñas no coinciden."
                    ControlToValidate="txtPasswordConfirm" ControlToCompare="txtPassword"
                    runat="server" CssClass="text-danger" Display="Dynamic" />
            </div>

            <hr />
            <asp:Button Text="Registrarse" ID="btnRegistro" OnClick="btnRegistro_Click" CssClass="btn btn-primary w-100" runat="server" />
            <a href="Login.aspx" class="d-block text-center mt-2">¿Ya tienes cuenta? Inicia sesión</a>
        </div>
    </div>
</asp:Content>
