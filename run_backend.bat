@echo off
:: Nombre del archivo: run_backend.bat

echo ==========================================
echo Iniciando el proyecto BackendAPI
echo ==========================================

:: Verificar si dotnet está instalado
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo .NET SDK no está instalado. Por favor, instálalo desde https://dotnet.microsoft.com/download
    exit /b 1
)

:: Restaurar dependencias
echo Restaurando dependencias...
dotnet restore
if %errorlevel% neq 0 (
    echo Error al restaurar dependencias.
    exit /b 1
)

:: Compilar el proyecto
echo Compilando el proyecto...
dotnet build
if %errorlevel% neq 0 (
    echo Error al compilar el proyecto.
    exit /b 1
)

:: Ejecutar el proyecto
echo Ejecutando el proyecto...
dotnet run
if %errorlevel% neq 0 (
    echo Error al ejecutar el proyecto.
    exit /b 1
)

pause