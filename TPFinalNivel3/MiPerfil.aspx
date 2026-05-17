<%@ Page Title="Mi Perfil - Mi Catálogo" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="TPFinalNivel3.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <div class="row">
            
            <div class="col-md-6">
                <h3 class="fw-bold mb-3">Mi Perfil</h3>
                <hr class="mb-4" />

                <div class="mb-3">
                    <label for="<%= txtEmail.ClientID %>" class="form-label fw-bold text-secondary">Email (Usuario)</label>
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control bg-light" ReadOnly="true" />
                </div>

                <div class="mb-3">
                    <label for="<%= txtNombre.ClientID %>" class="form-label fw-bold text-secondary">Nombre</label>
                    <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" placeholder="Ingresa tu nombre" />
                    <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio" ControlToValidate="txtNombre" runat="server" CssClass="text-danger small fw-bold" Display="Dynamic" />
                </div>

                <div class="mb-3">
                    <label for="<%= txtApellido.ClientID %>" class="form-label fw-bold text-secondary">Apellido</label>
                    <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" placeholder="Ingresa tu apellido" />
                    <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio" ControlToValidate="txtApellido" runat="server" CssClass="text-danger small fw-bold" Display="Dynamic" />
                </div>

                <div class="mb-4">
                    <label for="<%= txtUrlImagen.ClientID %>" class="form-label fw-bold text-secondary">URL Imagen de Perfil</label>
                    <asp:TextBox runat="server" ID="txtUrlImagen" CssClass="form-control" onchange="actualizarFoto()" placeholder="https://enlace-de-tu-foto.png" />
                </div>

                <div class="mb-3 mt-4">
                    <asp:Button Text="Guardar Cambios" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary px-4 me-2 rounded-pill shadow-sm" />
                    <a href="Default.aspx" class="btn btn-secondary me-2 rounded-pill px-4">Cancelar</a>
                    <asp:Button Text="Eliminar Cuenta" ID="btnEliminar" OnClick="btnEliminar_Click"
                        CssClass="btn btn-outline-danger btn-sm rounded-pill px-3" runat="server"
                        OnClientClick="return confirm('¿Estás totalmente seguro de proceder? Esta acción es destructiva, no se puede deshacer y perderás toda tu lista de favoritos.');" />
                </div>
            </div>

            <div class="col-md-6 d-flex flex-column align-items-center justify-content-center mt-4">
                <div class="p-3 bg-white rounded-circle shadow border border-light">
                    <asp:Image runat="server" ID="imgNuevoPerfil"
                        ImageUrl="https://www.pngkit.com/png/full/301-3012694_account-user-profile-avatar-comments-fa-user-circle.png"
                        CssClass="rounded-circle"
                        Style="height: 200px; width: 200px; object-fit: cover; display: block;" AlternateText="Previsualización de avatar de usuario" />
                </div>
                <p class="text-muted mt-3 small fw-bold">Previsualización de tu foto de perfil</p>
            </div>

        </div>
    </div>

    <script>
        function actualizarFoto() {
            // Capturamos el valor dinámico del cuadro de texto usando el ClientID inyectado por el framework
            const url = document.getElementById('<%= txtUrlImagen.ClientID %>').value;
            
            // Si el campo tiene datos, rehidratamos el atributo src de la imagen en tiempo real
            if (url.trim() !== "") {
                document.getElementById('<%= imgNuevoPerfil.ClientID %>').src = url;
            } else {
                // Fallback preventivo si el usuario borra la URL del cuadro de texto
                document.getElementById('<%= imgNuevoPerfil.ClientID %>').src = "https://www.pngkit.com/png/full/301-3012694_account-user-profile-avatar-comments-fa-user-circle.png";
            }
        }
    </script>
</asp:Content>
