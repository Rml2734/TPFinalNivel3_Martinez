<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPFinalNivel3.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>¡Bienvenido a mi Catálogo!</h1>

    <div class="row mb-4">
        <div class="col-6">
            <div class="input-group">
                <span class="input-group-text">🔍</span>
                <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control"
                    placeholder="Buscar por nombre..." AutoPostBack="true"
                    OnTextChanged="txtFiltro_TextChanged" />
            </div>
        </div>
    </div>

    <%-- Mensaje de búsqueda fallida --%>
    <div class="text-center mt-5">
        <asp:Label ID="lblSinResultados" runat="server"
            Text="No se encontraron artículos que coincidan con tu búsqueda... ☹️"
            CssClass="h4 text-muted"
            Visible="false" />
    </div>

    <div class="container text-center">
        <div class="row row-cols-1 row-cols-md-4 g-4">
            <%--  --%>
            <asp:Repeater ID="repRepetidor" runat="server">
               <ItemTemplate>
    <div class="col">
        <%-- Card con transición y sin bordes para un look moderno --%>
        <div class="card h-100 shadow-sm border-0 transition-all card-custom" style="transition: transform .2s; border-radius: 15px; overflow: hidden;">
            <div class="position-relative bg-light">
                <img src="<%# Eval("ImagenUrl") %>" 
                     class="card-img-top" 
                     style="height: 180px; object-fit: contain; padding: 15px;"
                     alt="<%# Eval("Nombre") %>"
                     onerror="this.onerror=null; this.src='https://tuningpro.co/wp-content/uploads/2023/03/placeholder.png';">
                
                <%-- Badge de Categoría (Etapa 3 Opcional) --%>
                <span class="position-absolute top-0 start-0 m-2 badge rounded-pill bg-dark opacity-75 shadow-sm">
                    <%# Eval("Categoria.Descripcion") %>
                </span>
            </div>
            
            <div class="card-body">
                <h5 class="card-title fw-bold text-primary mb-1"><%# Eval("Nombre") %></h5>
                <p class="card-text text-muted small mb-2" style="height: 40px; overflow: hidden;"><%# Eval("Descripcion") %></p>
                <%-- Precio resaltado en moneda local --%>
                <h4 class="text-success fw-bold">
                    ₡<%# string.Format("{0:N2}", Eval("Precio")) %>
                </h4>
            </div>
            
            <div class="card-footer bg-white border-0 pb-3">
                <div class="d-grid gap-2 d-md-flex justify-content-md-between">
                    <a href="DetalleArticulo.aspx?id=<%# Eval("Id") %>" class="btn btn-outline-primary btn-sm px-3 rounded-pill">Ver Detalle</a>
                    <asp:LinkButton ID="btnFavorito" runat="server" 
                        OnClick="btnFavorito_Click" 
                        CommandArgument='<%# Eval("Id") %>' 
                        CssClass="btn btn-link text-danger p-0 text-decoration-none" 
                        style="font-size: 1.2rem;">
                        ❤️ <small style="font-size: 0.7rem;" class="text-muted">Fav</small>
                    </asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
