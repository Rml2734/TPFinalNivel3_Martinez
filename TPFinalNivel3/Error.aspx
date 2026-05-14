<%@ Page Title="" Language="C#" MasterPageFile="~/MiMaster.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TPFinalNivel3.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        /* Definimos la animación de la alerta */
        @keyframes parpadeo-alerta {
            0% {
                color: #dc3545;
                opacity: 1;
            }
            /* Rojo de Bootstrap */
            50% {
                color: #ff0000;
                opacity: 0.5;
                transform: scale(1.01);
            }
            /* Rojo puro y un poco más grande */
            100% {
                color: #dc3545;
                opacity: 1;
            }
        }

        .mensaje-peligro {
            animation: parpadeo-alerta 1s infinite; /* Se repite cada segundo para siempre */
            display: inline-block;
            font-weight: bold;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container text-center mt-5">
        <%--  --%>
        <div class="shadow p-5 mb-5 rounded card-error" style="transition: all 0.3s ease;">
            <h1 class="display-4 text-danger fw-bold">¡Ups! Algo salió mal</h1>
            <p class="lead mt-3">Hubo un problema procesando tu solicitud.</p>
            <hr class="my-4">

            <div class="alert alert-secondary py-3 shadow-sm">
                <i class="bi bi-exclamation-triangle-fill me-2 text-danger"></i>
                <asp:Label ID="lblMensaje" runat="server" Text="Error desconocido."
                    CssClass="mensaje-peligro" />
            </div>

            <div class="mt-4">
                <a class="btn btn-primary btn-lg rounded-pill px-5 shadow" href="Default.aspx" role="button">Volver al Inicio
                </a>
            </div>
        </div>
    </div>
</asp:Content>
