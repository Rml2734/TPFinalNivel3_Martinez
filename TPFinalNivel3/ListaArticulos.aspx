<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="ListaArticulos.aspx.cs" Inherits="TPFinalNivel3.ListaArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Aquí va vacío a menos que necesites CSS específico para esta página --%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Artículos</h2>
        <hr />
        <div class="row">
            <div class="col">

                <div class="row mb-3">
                    <div class="col-6">
                        <asp:Label Text="Filtrar por nombre:" runat="server" />
                        <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" />
                    </div>
                </div>

                <asp:GridView ID="dgvArticulos" runat="server" DataKeyNames="Id"
                    OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
                    AutoGenerateColumns="false" CssClass="table table-hover table-bordered shadow-sm">
                    <Columns>
                        <asp:BoundField HeaderText="Código" DataField="Codigo" />
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
                        <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
                        <asp:BoundField HeaderText="Precio" DataField="Precio" DataFormatString="{0:C}" />
                        <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍️ Editar" ControlStyle-CssClass="btn btn-outline-primary btn-sm" />
                    </Columns>
                </asp:GridView>
                <a href="FormularioArticulo.aspx" class="btn btn-success">Agregar Nuevo</a>
            </div>
        </div>
    </div>
</asp:Content>
