# NovaMickTools

Suite de herramientas de escritorio en Windows Forms (.NET). Integra módulos de conversión de documentos, descarga de videos y gestión de pedidos en una sola aplicación.

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Python](https://img.shields.io/badge/Python-3776AB?style=for-the-badge&logo=python&logoColor=white)
![Windows](https://img.shields.io/badge/Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white)

---

## Módulos

| Módulo | Descripción |
|--------|-------------|
| **Convertidor PDF** | Word → PDF, Excel → PDF, Imagen → PDF, PDF → Word. Drag & drop y procesamiento en lote. |
| **Descargador de Videos** | Descarga desde YouTube, Facebook, Instagram y +1000 sitios. Selección de calidad (4K → 360p), opción solo-audio MP3. |
| **Pedidos Temu** | Gestión de órdenes de catálogo: lista de artículos con precio y link, generación de documentos Word. |

---

## Stack

- **UI:** Windows Forms (C# .NET)
- **Procesamiento:** Scripts Python (yt-dlp, Pillow, docx)
- **Generación de documentos:** python-docx via subprocess

---

## Requisitos

- Windows 10/11
- .NET 6+ (o el runtime configurado en el proyecto)
- Python 3.7+ con dependencias instaladas: `pip install yt-dlp Pillow python-docx`

---

## Correr localmente

```bash
git clone https://github.com/Mickaell22/NovaMickTools.git
cd NovaMickTools
```

Abrir `NovaMickTools.sln` en Visual Studio y compilar.
