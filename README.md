# Catálogo Web - TP Final Nivel 3

Aplicación web de catálogo de artículos con gestión de usuarios, favoritos y panel de administración. Proyecto final del curso de C# Nivel 3 (ASP.NET WebForms).

## Enlaces

- **Aplicación en línea:** https://tp3.robertomartinez.dev
- **Repositorio:** https://github.com/Rml2734/TPFinalNivel3_Martinez

## Funcionalidades

- Catálogo de artículos con búsqueda por nombre.
- Registro e inicio de sesión de usuarios.
- Artículos favoritos por usuario.
- Detalle de cada artículo.
- Perfil de usuario con imagen.
- Panel de administración (solo administradores): listar, filtrar, agregar y editar artículos.
- Modo claro y oscuro.
- Manejo de errores con página de error personalizada.

## Tecnologías

- C# / ASP.NET WebForms (.NET Framework 4.8)
- SQL Server 2022
- ADO.NET
- Bootstrap 5
- Arquitectura en capas: `dominio` (entidades), `negocio` (acceso a datos y reglas) y `TPFinalNivel3` (presentación)

## Estructura de la solución

```
SolucionCatalogo/
├── dominio/          Clases de entidad (Articulo, Marca, Categoria, Usuario)
├── negocio/          Acceso a datos y lógica de negocio
└── TPFinalNivel3/    Páginas web (.aspx), MasterPage, imágenes
```

## Despliegue

La aplicación está publicada en un servidor propio con Ubuntu:

- Contenedor Docker con Mono 6.8 y xsp4 que ejecuta el sitio.
- SQL Server 2022 en otro contenedor, en la misma red de Docker.
- Caddy y Cloudflare Tunnel para exponer el sitio con HTTPS en `tp3.robertomartinez.dev`.
- La cadena de conexión se entrega al contenedor mediante una variable de entorno (CATALOGO_CONNECTION); no se guarda ninguna credencial en el repositorio.

Nota técnica: como el sitio corre en Mono, en `ListaArticulos.aspx` se usa `Eval("Marca.Descripcion")` dentro de un `TemplateField`, ya que `BoundField` no resuelve propiedades anidadas en Mono.

## Cómo ejecutarlo en local

1. Abrir `SolucionCatalogo.sln` en Visual Studio.
2. Crear la base de datos `CATALOGO_WEB_DB` en SQL Server.
3. La conexión local usa SQL Server Express con autenticación de Windows (ver negocio/AccesoDatos.cs). Para otro servidor, definir la variable de entorno CATALOGO_CONNECTION con la cadena de conexión completa.
4. Ejecutar con IIS Express (F5).

## Usuarios de prueba

- **Administrador:** `admin@admin.com`
- **Usuario estándar:** `test@test.com`
- **Contraseñas:** *(se entregan al docente por separado)*

## Autor

Roberto Martínez