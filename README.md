# TestChat
Test Chat Windows Native, desarrollado en WPF con .NET 9.

## Características
*   **UI/UX Moderna**:
    *   **Modo Oscuro y Claro**: Cambio de tema fluido con preferencias persistentes.
    *   **Diseño Responsivo**: Barra lateral de navegación, pantalla de bienvenida  y globos de mensaje.
    *   **Estilo Personalizado**: Bordes redondeados, efectos hover y una interfaz limpia.
    *   **Colores Dinámicos**: Los globos de chat y bordes se ajustan dinámicamente al tema.
*   **Funcionalidades**:
    *   **Respuestas de IA Simuladas**: Simula una respuesta de IA con indicador de "Generando...".
    *   **Historial de Chat**: Lista organizada de conversaciones, filtrable mediante búsqueda.
    *   **Localización**: Soporte completo para **Español (ES)** e Inglés (EN), cambiable en tiempo real.
    *   **Panel de Configuración**: Panel superpuesto para cambiar tema e idioma.
*   **Manejo de Archivos**:
    *   **Adjuntos**: Pega imágenes o archivos usando el botón dedicado o **Ctrl+V**.
    *   **Drag & Drop**: Copia archivos del explorador y pégalos directamente en el chat.
    *   **Previsualizaciones**: Las imágenes muestran una miniatura; los archivos muestran iconos dinámicos según su tipo (📊, 📕, 📦, etc.).
    *   **Interacción**: Haz clic en cualquier adjunto para abrirlo con el visor predeterminado de tu sistema.
## Requisitos Previos
*   **.NET 9.0 SDK**: Necesario para compilar y ejecutar la aplicación.
*   **Windows OS**: Al ser una aplicación WPF, funciona en Windows.
## Instalación y Ejecución
1.  **Clonar/Descargar** el repositorio.
2.  Abrir una terminal en el directorio del proyecto:
    ```powershell
    cd WpfChatApp
    ```
3.  **Ejecutar** la aplicación:
    ```powershell
    dotnet run
    ```
4.  **Compilar** para release (opcional):
    ```powershell
    dotnet build -c Release
    ```
  
## Estructura del Proyecto
*   **ViewModels/**: Lógica MVVM (MainViewModel).
*   **Models/**: Objetos de datos (ChatMessage, ChatSession, ChatAttachment).
*   **Services/**: Lógica para la gestión de Temas e Idiomas.
*   **Resources/**: Diccionarios de recursos XAML para Temas, Cadenas y Estilos.
*   **Core/**: Clases de ayuda (RelayCommand, ObservableObject).
