<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="TPFinalNivel3.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-md-6">
            <h3>Mi Perfil</h3>

            <div class="mb-3">
                <label class="form-label">Email (Usuario)</label>
                <!-- ReadOnly=true para que no se pueda cambiar -->
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" ReadOnly="true" />
            </div>

            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                <%-- Validador de Nombre --%>
                <asp:RequiredFieldValidator ErrorMessage="El nombre es obligatorio" ControlToValidate="txtNombre" runat="server" CssClass="text-danger" />
            </div>

            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
                <%-- Validador de Apellido --%>
                <asp:RequiredFieldValidator ErrorMessage="El apellido es obligatorio" ControlToValidate="txtApellido" runat="server" CssClass="text-danger" />
            </div>

            <!-- Puedes agregar Fecha de Nacimiento aquí si tu tabla SQL tiene esa columna. 
                 Si no la tiene, mejor no lo pongas por ahora para no complicar la DB. -->

            <div class="mb-3">
                <label class="form-label">URL Imagen de Perfil</label>
                <asp:TextBox runat="server" ID="txtUrlImagen" CssClass="form-control" onchange="actualizarFoto()" />
            </div>

            <asp:Button Text="Guardar Cambios" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
            <a href="Default.aspx" class="btn btn-secondary text-decoration-none">Cancelar</a>
            <asp:Button Text="Eliminar Cuenta" ID="btnEliminar" OnClick="btnEliminar_Click"
                CssClass="btn btn-danger" runat="server"
                OnClientClick="return confirm('¿Estás totalmente seguro? Esta acción no se puede deshacer y perderás tus favoritos.');" />
        </div>

        <div class="col-md-6 d-flex flex-column align-items-center mt-4">
            <!-- Imagen de perfil grande para previsualizar -->
            <asp:Image runat="server" ID="imgNuevoPerfil"
                ImageUrl="https://www.pngkit.com/png/full/301-3012694_account-user-profile-avatar-comments-fa-user-circle.png"
                CssClass="rounded-circle border border-primary shadow"
                Style="height: 200px; width: 200px; object-fit: cover;" />
            <p class="text-muted mt-2">Previsualización de tu foto</p>
        </div>
    </div>

    <script>
        function actualizarFoto() {
            // Obtenemos el valor del cuadro de texto
            const url = document.getElementById('<%= txtUrlImagen.ClientID %>').value;
            // Se lo pasamos a la imagen de previsualización
            document.getElementById('<%= imgNuevoPerfil.ClientID %>').src = url;
        }
    </script>



</asp:Content>
