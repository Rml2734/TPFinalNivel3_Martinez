<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="ListaArticulos.aspx.cs" Inherits="TPFinalNivel3.ListaArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--  CSS específico para esta página --%>
    <style>
        /* Estructura base de la grilla administrativa */
        .table {
            border-collapse: separate;
            border-spacing: 0;
            border-radius: 15px;
            overflow: hidden;
        }

        .table thead {
            background-color: #0d6efd;
            color: white;
        }

        .table td, .table th {
            vertical-align: middle;
            padding: 12px;
        }

        /* Interactividad de filas (Hover) adaptativo según el tema visual */
        .table-hover tbody tr:hover {
            background-color: rgba(13, 110, 253, 0.1) !important;
            transition: background-color 0.3s ease;
            cursor: pointer;
        }

        [data-bs-theme="dark"] .table {
            color: #e0e0e0;
            border-color: #444;
        }

        [data-bs-theme="dark"] .table-hover tbody tr:hover {
            background-color: rgba(255, 255, 255, 0.05) !important;
        }

        /* =========================================================================
           BLINDAJE DE ALERTA: ANULA HOVER, BORDES Y HERENCIAS DE TABLA (EMPTY STATE)
           ========================================================================= */
        .table tr.alert-warning,
        .table td.alert-warning,
        .table .alert-warning td {
            background-color: var(--bs-alert-bg, #fff3cd) !important;
            color: var(--bs-alert-color, #664d03) !important;
            border-color: var(--bs-alert-border-color, #ffecb5) !important;
            border-bottom: none !important;
            box-shadow: none !important;
        }

        /* Desactiva explícitamente el cambio a gris al pasar el mouse sobre la alerta */
        .table-hover tbody tr.alert-warning:hover,
        .table-hover tbody tr:has(.alert-warning):hover,
        .table-hover tbody tr.alert-warning:hover td {
            background-color: var(--bs-alert-bg, #fff3cd) !important;
            background: var(--bs-alert-bg, #fff3cd) !important;
            cursor: default;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2>Gestión de Artículos</h2>
        <hr />

        <div class="row">
            <div class="col">

                <div class="row mb-3">
                    <div class="col-6">
                        <asp:Label Text="Filtrar por nombre:"
                            runat="server"
                            CssClass="form-label fw-bold"
                            AssociatedControlID="txtFiltro" />
                        <asp:TextBox runat="server" ID="txtFiltro"
                            CssClass="form-control"
                            AutoPostBack="true"
                            OnTextChanged="txtFiltro_TextChanged"
                            placeholder="Escribe para buscar..." />
                    </div>
                </div>

                <asp:GridView ID="dgvArticulos" runat="server" DataKeyNames="Id"
                    OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover shadow-sm"
                    GridLines="None"
                    UseAccessibleHeader="true"
                    EmptyDataText="⚠️ No se encontraron referencias de artículos que coincidan con el criterio ingresado."
                    EmptyDataRowStyle-CssClass="alert alert-warning text-center fw-bold my-4 p-3 shadow-sm d-block">
                    <Columns>
                        <asp:BoundField HeaderText="Código" DataField="Codigo" />
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                        <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
                        <asp:BoundField HeaderText="Categoría" DataField="Categoria.Descripcion" />
                        <asp:BoundField HeaderText="Precio" DataField="Precio" DataFormatString="{0:F2}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right" />
                        <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✍️ Editar" ControlStyle-CssClass="btn btn-outline-primary btn-sm px-3 rounded-pill" />
                    </Columns>
                </asp:GridView>

                <div class="mt-3">
                    <a href="FormularioArticulo.aspx" class="btn btn-success px-4 rounded-pill shadow-sm">Agregar Nuevo Artículo</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
