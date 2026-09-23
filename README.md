# Sistema de Gestión para Kiosco (C# WinForms)

Un sistema de Punto de Venta (POS) ligero y rápido, diseñado específicamente para kioscos y minimercados, desarrollado en **C# (.NET 8)** con **Windows Forms** y **SQL Server**.

## Características Principales
- **Arquitectura de 3 Capas (N-Tier)** aplicada al módulo de Usuarios (Views, Business, Data).
- Estructura de carpetas limpia y profesional (Views, Models, Data, Business, Utils, Scripts).
- Arquitectura basada en Roles (Administrador, Supervisor, Vendedor).
- Autenticación segura usando cifrado **BCrypt**.
- Interfaz de módulos por pestañas ("Single Page Application" usando UserControls).
- Modo Oscuro nativo.

## Requisitos del Sistema
- **.NET 8.0 SDK** o superior.
- **SQL Server** (Express Edition es suficiente) con autenticación de Windows habilitada (`localhost\SQLEXPRESS`).
- Visual Studio 2022.

## Instalación y Despliegue

1. **Clonar el repositorio:**
   ```bash
   git clone <url-del-repo>
   cd kiosco-csharp
   ```

2. **Configuración de la Base de Datos (Seeder):**
   El archivo `KioscoApp/KioscoDB.sql` contiene tanto el esquema de las tablas (`CREATE TABLE`) como los datos iniciales de prueba (usuarios base, categorías, productos).
   
   Abrir **powershell** en la ubicacion del archivo *Kiosco.sql* que se encuentra dentro de la carpeta *scripts* y ejecutar el siguiente comando:
   ```powershell
   Invoke-Sqlcmd -ServerInstance ".\SQLEXPRESS" -InputFile "KioscoDB.sql"
   ```

3. **Compilar y Ejecutar:**
   Abre la solución en Visual Studio y presiona `F5`, o ejecuta desde la línea de comandos:
   ```bash
   dotnet build
   dotnet run --project KioscoApp
   ```

---

## ⚠️ Advertencia Técnica Crítica: BCrypt en PowerShell

Si necesitas insertar o actualizar contraseñas encriptadas con BCrypt directamente desde la línea de comandos (ej. `sqlcmd -Q "INSERT..."`), **JAMÁS utilices comillas dobles (`" "`) en PowerShell para envolver el hash.**

**❌ Incorrecto:**
```powershell
sqlcmd -Q "INSERT INTO Usuarios (Contrasena) VALUES ('`$2a`$11`$phr8...')"
```
**Por qué falla:** PowerShell interpola todas las variables que empiezan con `$`. Los fragmentos del hash como `$2a` o `$11` son interpretados como variables vacías y **eliminados** silenciosamente del texto, corrompiendo la contraseña insertada en la base de datos (luego el login te dirá que la contraseña es inválida aunque sea correcta).

**✅ Correcto:**
Usa SIEMPRE **comillas simples (`' '`)** externas en PowerShell para evitar cualquier evaluación de variables si estás manipulando hashes BCrypt crudos:
```powershell
sqlcmd -Q 'INSERT INTO Usuarios (Contrasena) VALUES (''$2a$11$phr8.2Iyh4hDs6YiluSs7OmMTzIcLd5qLzCc0jPlxYFbaphoWYKii'')'
```
*(Nota: Dentro de comillas simples en PS, para escapar una comilla simple en SQL, usas dos comillas simples `''`).*

## Usuarios de Prueba (Por Defecto)
Todos los usuarios creados por el seeder inicial tienen la contraseña: `admin`

- **Admin IT:** `admin`
- **Supervisor:** `super`
- **Vendedores:** `antonio`, `ramiro`

