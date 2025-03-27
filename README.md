# BackendAPI

BackendAPI es una API desarrollada en .NET Core que utiliza Entity Framework Core para la gestión de datos y PostgreSQL como base de datos. Este proyecto incluye funcionalidades como autenticación JWT, manejo de libros, autores, géneros y usuarios.

## Características

- **Batch File:** Archivo automatizado para instalar y correr el proyecto sin necesidad de instalar previamente todos los paquetes.
- **Swagger:** Documentación interactiva para probar la API.
- **Logs:** Integración con Serilog para registrar eventos.
- **Autenticación JWT:** Seguridad para proteger los endpoints que lo requieran.
- **Entity Framework Core:** Gestión de datos con migraciones y soporte para PostgreSQL.
- **CRUD:** Generado para probar diferentes HTTP status codes con ejemplo mock de Weather. Operaciones para libros, autores, géneros y usuarios pendientes.

---

## Requisitos previos

1. **.NET SDK**: [Descargar .NET SDK](https://dotnet.microsoft.com/download)
2. **PostgreSQL**: Asegúrate de tener PostgreSQL instalado y en ejecución.
3. **Herramientas de EF Core**: Instala las herramientas globales de EF Core si no las tienes:
   ```bash
   dotnet tool install --global dotnet-ef