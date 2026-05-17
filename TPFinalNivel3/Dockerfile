# Usamos la base oficial de Microsoft con el servidor web IIS y .NET 4.8
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2022

# Nos paramos en la carpeta raíz del servidor web
WORKDIR /inetpub/wwwroot

# Copiamos todo el contenido compilado de la interfaz de la app
COPY ./TPFinalNivel3/ .