# Facial Authentication Seek Service 🖼️🔍

![License](https://img.shields.io/badge/License-MIT-blue.svg)
![.NET](https://img.shields.io/badge/.NET-6.0-purple.svg)
![ImageSharp](https://img.shields.io/badge/ImageSharp-2.1.3-green.svg)

Este proyecto implementa un servicio de autenticación biométrica utilizando la API de DeepSeek. El servicio compara dos imágenes faciales, genera un patrón biométrico para cada una, y determina si coinciden. Además, proporciona un porcentaje de confianza y un mensaje explicativo.

---

## Características Principales ✨

- **Comparación de Imágenes**: Analiza dos imágenes faciales y determina si coinciden.
- **Redimensionamiento de Imágenes**: Las imágenes se redimensionan a 100x100 píxeles antes de ser procesadas.
- **Integración con DeepSeek**: Utiliza la API de DeepSeek para realizar la comparación biométrica.
- **Respuesta en JSON**: Devuelve los resultados en un formato JSON estructurado.

---

## Requisitos 📋

- [.NET 6.0 o superior](https://dotnet.microsoft.com/download)
- [SixLabors.ImageSharp](https://www.nuget.org/packages/SixLabors.ImageSharp/) (para el procesamiento de imágenes)
- Clave API de DeepSeek

---

## Configuración ⚙️

1. **Clave API de DeepSeek**:
   - Obtén una clave API de DeepSeek y configúrala en el archivo de configuración de tu aplicación.

2. **Instalación de Dependencias**:
   - Asegúrate de tener instalado el paquete `SixLabors.ImageSharp`:
     ```bash
     dotnet add package SixLabors.ImageSharp
     ```

3. **Configuración del Proyecto**:
   - Asegúrate de que el archivo de configuración (`appsettings.json` o similar) contenga la URL de la API de DeepSeek y la clave API:
     ```json
     {
       "ExternalProviders": {
         "DeepSeekSettings": {
           "Url": "https://api.deepseek.com/chat/completions",
           "Key": "tu_clave_api_aqui"
         }
       }
     }
     ```

--- 

## Uso 🚀

### Método `PostAsync`

Este método toma dos imágenes faciales (en formato `IFormFile`), las redimensiona, las convierte a base64, y las envía a la API de DeepSeek para su comparación.

#### Parámetros

- `data`: Un objeto de tipo `FacialAuthenticationSeekRequest` que contiene las dos imágenes a comparar.

#### Ejemplo de Uso

```csharp
var service = new FacialAuthenticationSeekService(serviceProvider);
var request = new FacialAuthenticationSeekRequest
{
    ImageBase = imagenBase, // IFormFile
    ImageReference = imagenReferencia // IFormFile
};

var response = await service.PostAsync(request);