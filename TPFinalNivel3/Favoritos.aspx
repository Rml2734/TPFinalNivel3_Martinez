<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="TPFinalNivel3.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="mb-4">Mis Productos Favoritos ❤️</h2>

    <div class="row row-cols-1 row-cols-md-4 g-4">
        <asp:Repeater ID="repFavoritos" runat="server">
            <ItemTemplate>
                <div class="col">
                    <%-- Usamos la clase card-custom que definimos en la Master --%>
                    <div class="card h-100 shadow-sm border-0 transition-all card-custom" style="transition: transform .2s; border-radius: 15px; overflow: hidden;">
                        <!--<div class="bg-light">
                            <img src="<%# Eval("ImagenUrl") %>" 
                                 class="card-img-top" 
                                 style="height: 180px; object-fit: contain; padding: 15px;"
                                 onerror="this.onerror=null; this.src='https://tuningpro.co/wp-content/uploads/2023/03/placeholder.png';">
                        </div> -->

                        <div class="bg-light">
                            <asp:Image ID="imgArticulo" runat="server"
                                ImageUrl='<%# Eval("ImagenUrl") %>'
                                CssClass="card-img-top"
                                Style="height: 180px; object-fit: contain; padding: 15px;"
                                AlternateText='<%# "Imagen de " + Eval("Nombre") %>' />
                        </div>

                        <div class="card-body">
                            <h5 class="card-title fw-bold text-primary mb-3"><%# Eval("Nombre") %></h5>

                            <div class="d-grid">
                                <asp:LinkButton runat="server" ID="btnEliminarFav"
                                    OnClick="btnEliminarFav_Click"
                                    CommandArgument='<%# Eval("Id") %>'
                                    CssClass="btn btn-outline-danger btn-sm rounded-pill">
                                    🗑️ Quitar de Favoritos
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

    <%-- Mensaje por si la lista está vacía (Opcional pero recomendado) --%>
    <% if (repFavoritos.Items.Count == 0)
        { %>
    <div class="text-center mt-5">
        <p class="h4 text-muted">Aún no tienes productos favoritos... ¡ve a buscar algo lindo! 🛍️</p>
        <a href="Default.aspx" class="btn btn-primary mt-3">Volver al Catálogo</a>
    </div>
    <% } %>
</asp:Content>
