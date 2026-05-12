<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="TPFinalNivel3.FormularioArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row">
            <div class="col-6">
                <h2>Formulario de Artículo</h2>
                <hr />
                <div class="mb-3">
                    <label for="txtCodigo" class="form-label">Código</label>
                    <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" MaxLength="50" />
                </div>
                <div class="mb-3">
                    <label for="txtNombre" class="form-label">Nombre</label>
                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" MaxLength="50" />
                    <%-- Valida que el nombre no esté vacío --%>
                    <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio"
                        ControlToValidate="txtNombre" runat="server" CssClass="text-danger" />
                </div>
                <div class="mb-3">
                    <label for="txtDescripcion" class="form-label">Descripción</label>
                    <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" TextMode="MultiLine" MaxLength="50" />
                </div>
                <div class="mb-3">
                    <label for="ddlMarca" class="form-label">Marca</label>
                    <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="ddlCategoria" class="form-label">Categoría</label>
                    <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="txtPrecio" class="form-label">Precio</label>
                    <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" />

                    <!-- Valida que no esté vacío -->
                    <asp:RequiredFieldValidator 
                        ErrorMessage="El precio es obligatorio"
                        ControlToValidate="txtPrecio" 
                        runat="server" 
                        CssClass="text-danger" />

                    <!-- Valida que sea solo números (puedes usar decimales con coma o punto según tu PC) -->
                    <asp:RegularExpressionValidator
                        ErrorMessage="Formato inválido. Use máximo 2 decimales."
                        ControlToValidate="txtPrecio"
                        ValidationExpression="^[0-9]+([.,][0-9]{1,2})?$"
                        runat="server"
                        CssClass="text-danger"
                        Display="Dynamic" />
                </div>



                <div class="mb-3">
                    <asp:Button Text="Aceptar" ID="btnAceptar" CssClass="btn btn-primary"
                        OnClick="btnAceptar_Click" runat="server" OnClientClick="if (Page_ClientValidate()) { this.disabled=true; this.value='Enviando...'; }"
                        UseSubmitBehavior="false" />
                    <a href="ListaArticulos.aspx" class="btn btn-secondary">Cancelar</a>
                    <asp:Button Text="Eliminar" ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" runat="server"
                        OnClientClick="return confirm('¿Estás seguro de que deseas eliminar este artículo?');" />
                </div>
            </div>

            <div class="col-6">
                <div class="mb-3">
                    <label for="txtImagenUrl" class="form-label">URL Imagen</label>
                    <asp:TextBox runat="server" ID="ID_txtImagenUrl" CssClass="form-control"
                        AutoPostBack="true" OnTextChanged="ID_txtImagenUrl_TextChanged" />
                </div>
                <asp:Image ImageUrl="https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png"
                    ID="imgArticulo" runat="server" Width="60%" />
            </div>
        </div>
    </div>
</asp:Content>
