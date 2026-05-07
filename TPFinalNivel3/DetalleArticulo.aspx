<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="TPFinalNivel3.DetalleArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row mt-4">
        <div class="col-md-6">
            <asp:Image ID="imgArticulo" runat="server" CssClass="img-fluid rounded shadow" />
        </div>
        <div class="col-md-6">
            <h2 runat="server" id="lblNombre"></h2>
            <hr />
            <p><strong>Código:</strong> <asp:Label ID="lblCodigo" runat="server" /></p>
            <p><strong>Descripción:</strong> <asp:Label ID="lblDescripcion" runat="server" /></p>
            <p><strong>Marca:</strong> <asp:Label ID="lblMarca" runat="server" /></p>
            <p><strong>Categoría:</strong> <asp:Label ID="lblCategoria" runat="server" /></p>
            <h3><asp:Label ID="lblPrecio" runat="server" CssClass="badge bg-success" /></h3>
            <div class="mt-4">
                <a href="Default.aspx" class="btn btn-primary">Regresar</a>
            </div>
        </div>
    </div>
</asp:Content>