# Registro de Productos

Aplicación .NET MAUI 10 para Android con XAML y SQLite local.

## Ejecutar en Visual Studio
1. Abre RegistroProductos.sln.
2. Comprueba en Visual Studio Installer la carga de trabajo .NET MAUI con SDK y JDK Android.
3. Restaura paquetes NuGet si se solicita.
4. Selecciona un emulador Android o un teléfono con depuración USB.
5. Ejecuta con F5.

En esta computadora el SDK Android está en `C:\Users\jeise\AppData\Local\Android\Sdk` y el JDK en `C:\Program Files\Android\Android Studio1\jbr`. Si Visual Studio no los detecta, configura esas rutas en las opciones de Android.

## Estructura
- Models/Producto.cs: modelo con ID autoincremental y los seis campos solicitados.
- Data/ProductoDatabase.cs: conexión, inicialización, GuardarProductoAsync y ObtenerProductosAsync.
- MainPage.xaml: formulario y botón Guardar Producto.
- MainPage.xaml.cs: captura, validación, guardado y mensajes.
- Validation/ProductoValidator.cs: reglas de validación.
- MauiProgram.cs: dependencias y ruta de productos.db3 en AppDataDirectory.

## Validación
Nombre y descripción obligatorios, máximo 100 y 500 caracteres. Precio positivo con hasta dos decimales; se admite coma o punto, sin separadores de miles. Cantidad entera desde cero. ID y fecha se asignan al guardar. El botón se bloquea durante la operación; si falla, se conservan los datos del formulario.

## Prueba manual
Guarda Cuaderno / Cuaderno escolar / 25,50 / 10. Comprueba el mensaje con ID y fecha y el formulario limpio. Guarda otro producto y comprueba un ID distinto. Prueba campos vacíos, precio negativo, tres decimales y cantidad fraccionaria: deben rechazarse.

La base persiste entre sesiones. ObtenerProductosAsync permite consultar los registros desde el código. Desinstalar la aplicación o borrar sus datos elimina la base local.

Referencia: https://learn.microsoft.com/dotnet/maui/data-cloud/database-sqlite?view=net-maui-10.0
