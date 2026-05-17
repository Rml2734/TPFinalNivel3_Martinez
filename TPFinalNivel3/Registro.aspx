<%@ Page Title="Crear Cuenta - Mi Catálogo" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="TPFinalNivel3.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row justify-content-center mt-5">
        <div class="col-md-4">
            
            <div class="shadow p-4 rounded border border-light">
                <h2 class="text-center mb-4 fw-bold ">Crear Cuenta</h2>
                <hr class="mb-4" />

                <div class="mb-3">
                    <label for="<%= txtEmail.ClientID %>" class="form-label fw-bold text-secondary">Email</label>
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="nombre@correo.com" />

                    <asp:RequiredFieldValidator ErrorMessage="El email es obligatorio."
                        ControlToValidate="txtEmail" 
                        runat="server" 
                        CssClass="text-danger small fw-bold" 
                        Display="Dynamic" />

                    <asp:RegularExpressionValidator ErrorMessage="Formato inválido (ej: usuario@correo.com)."
                        ControlToValidate="txtEmail"
                        ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                        runat="server" 
                        CssClass="text-danger small fw-bold" 
                        Display="Dynamic" />
                </div>

                <div class="mb-3">
                    <label for="<%= txtPassword.ClientID %>" class="form-label fw-bold text-secondary">Contraseña</label>
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" placeholder="••••••••" />
                    
                    <asp:RequiredFieldValidator ErrorMessage="La contraseña es obligatoria."
                        ControlToValidate="txtPassword" 
                        runat="server" 
                        CssClass="text-danger small fw-bold" 
                        Display="Dynamic" />
                </div>

                <div class="mb-4">
                    <label for="<%= txtPasswordConfirm.ClientID %>" class="form-label fw-bold text-secondary">Repetir Contraseña</label>
                    <asp:TextBox runat="server" ID="txtPasswordConfirm" CssClass="form-control" TextMode="Password" placeholder="••••••••" />
                    
                    <asp:RequiredFieldValidator ErrorMessage="Debes repetir la contraseña."
                        ControlToValidate="txtPasswordConfirm" 
                        runat="server" 
                        CssClass="text-danger small fw-bold" 
                        Display="Dynamic" />

                    <asp:CompareValidator ErrorMessage="Las contraseñas no coinciden."
                        ControlToValidate="txtPasswordConfirm" 
                        ControlToCompare="txtPassword"
                        runat="server" 
                        CssClass="text-danger small fw-bold" 
                        Display="Dynamic" />
                </div>

                <div class="d-grid gap-2 mt-4">
                    <asp:Button Text="Registrarse e Ingresar" ID="btnRegistro" OnClick="btnRegistro_Click" CssClass="btn btn-primary btn-lg rounded-pill shadow-sm" runat="server" />
                </div>
                
                <div class="text-center mt-3">
                    <a href="Login.aspx" class="text-decoration-none small fw-bold text-primary">¿Ya tienes una cuenta? Inicia sesión</a>
                </div>
            </div>

        </div>
    </div>
</asp:Content>