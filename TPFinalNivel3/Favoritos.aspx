<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="TPFinalNivel3.Favoritos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Mis Productos Favoritos</h2>
    <div class="row row-cols-1 row-cols-md-4 g-4">
        <asp:Repeater ID="repFavoritos" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 shadow-sm">
                        <img src="<%# Eval("ImagenUrl") %>" class="card-img-top" style="height: 150px; object-fit: contain;">
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <asp:LinkButton runat="server" ID="btnEliminarFav" 
                                OnClick="btnEliminarFav_Click" 
                                CommandArgument='<%# Eval("Id") %>' 
                                CssClass="btn btn-outline-danger btn-sm">Quitar de Favoritos</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
