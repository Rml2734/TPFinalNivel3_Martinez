<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="ListaArticulos.aspx.cs" Inherits="TPFinalNivel3.ListaArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Aquí va vacío a menos que necesites CSS específico para esta página --%>
    <style>
        /* Efecto de cristal y borde redondeado para la tabla */
        .table {
            border-collapse: separate;
            border-spacing: 0;
            border-radius: 15px;
            overflow: hidden;
        }

            /* Color de encabezado elegante */
            .table thead {
                background-color: #0d6efd;
                color: white;
            }

        /* El efecto Hover personalizado */
        .table-hover tbody tr:hover {
            background-color: rgba(13, 110, 253, 0.1) !important; /* Un azul muy suave */
            transition: background-color 0.3s ease;
            cursor: pointer;
        }

        /* Ajuste para Modo Oscuro en la grilla */
        [data-bs-theme="dark"] .table {
            color: #e0e0e0;
            border-color: #444;
        }

        [data-bs-theme="dark"] .table-hover tbody tr:hover {
            background-color: rgba(255, 255, 255, 0.05) !important; /* Resalte sutil en oscuro */
        }

        /* Estilo para las celdas */
        .table td, .table th {
            vertical-align: middle;
            padding: 12px;
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
                        <asp:Label Text="Filtrar por nombre:" runat="server" />
                        <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" />
                    </div>
                </div>

                <asp:GridView ID="dgvArticulos" runat="server" DataKeyNames="Id"
                    OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
                    AutoGenerateColumns="false"
                    CssClass="table table-hover shadow-sm"
                    GridLines="None"
                    UseAccessibleHeader="true">
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
