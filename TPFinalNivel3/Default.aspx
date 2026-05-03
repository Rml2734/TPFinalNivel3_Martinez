<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TPFinalNivel3.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>¡Bienvenido a mi Catálogo!</h1>
    <div class="container text-center">
    <div class="row row-cols-1 row-cols-md-4 g-4"> <%-- Cambié md-3 a md-4 para que entren más por fila --%>
        <asp:Repeater ID="repRepetidor" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 shadow-sm"> <%-- Agregué sombra --%>
                        <img src="<%# Eval("ImagenUrl") %>" 
                             class="card-img-top" 
                             alt="<%# Eval("Nombre") %>"
                             style="height: 200px; object-fit: contain; padding: 10px;" <%-- Tamaño fijo y centrado --%>
                             onerror="this.src='https://tuningpro.co/wp-content/uploads/2023/03/placeholder.png';">
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text text-muted" style="font-size: 0.9rem;"><%# Eval("Descripcion") %></p>
                            <p class="card-text"><strong>$<%# string.Format("{0:N2}", Eval("Precio")) %></strong></p>
                        </div>
                        <div class="card-footer bg-transparent border-top-0">
                            <a href="Detalle.aspx?id=<%# Eval("Id") %>" class="btn btn-primary btn-sm">Ver Detalle</a>
                            <asp:Button ID="btnEjemplo" runat="server" Text="Favorito" CssClass="btn btn-outline-danger btn-sm" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
</asp:Content>
