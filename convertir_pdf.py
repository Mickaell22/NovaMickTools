#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
Script para convertir documentos entre diferentes formatos usando Python
Uso: python convertir_pdf.py <archivo_origen> <carpeta_destino> <tipo_conversion>
Tipos de conversi�n soportados:
- word_to_pdf: Word (.docx, .doc) a PDF
- excel_to_pdf: Excel (.xlsx, .xls) a PDF
- image_to_pdf: Imagen a PDF
- pdf_to_word: PDF a Word (.docx)
"""

import sys
import os
import subprocess
from pathlib import Path

# Configurar encoding UTF-8 para evitar errores de codificacion
if sys.platform.startswith('win'):
    import codecs
    sys.stdout = codecs.getwriter('utf-8')(sys.stdout.detach())
    sys.stderr = codecs.getwriter('utf-8')(sys.stderr.detach())

def verificar_dependencias():
    """Verifica e instala las dependencias necesarias"""
    dependencias = {
        'pdf2docx': 'pdf2docx',
        'python-docx': 'python-docx',
        'openpyxl': 'openpyxl',
        'xlsxwriter': 'xlsxwriter',
        'Pillow': 'Pillow',
        'reportlab': 'reportlab',
        'pywin32': 'pywin32'
    }
    
    dependencias_faltantes = []
    
    for modulo, paquete in dependencias.items():
        try:
            __import__(modulo.replace('-', '_'))
        except ImportError:
            dependencias_faltantes.append(paquete)
    
    if dependencias_faltantes:
        print("Instalando dependencias faltantes...")
        for paquete in dependencias_faltantes:
            try:
                print(f"Instalando {paquete}...")
                subprocess.run([sys.executable, '-m', 'pip', 'install', paquete], 
                              check=True, capture_output=True, text=True)
                print(f"{paquete} instalado exitosamente")
            except subprocess.CalledProcessError as e:
                print(f"Error instalando {paquete}: {e}")
                return False
    
    return True

def word_to_pdf(archivo_origen, archivo_destino):
    """Convierte Word a PDF usando COM (Windows)"""
    try:
        import win32com.client as win32
        
        print("Iniciando conversion de Word a PDF...")
        
        # Crear aplicaci�n Word
        word = win32.Dispatch('Word.Application')
        word.Visible = False
        word.DisplayAlerts = False  # Desactivar alertas para mayor velocidad
        
        # Abrir documento
        print(f"Abriendo documento: {archivo_origen}")
        doc = word.Documents.Open(archivo_origen)
        
        # Guardar como PDF (formato 17)
        print(f"Guardando como PDF: {archivo_destino}")
        doc.SaveAs(archivo_destino, FileFormat=17)
        
        # Cerrar documento y aplicaci�n
        doc.Close()
        word.Quit()
        
        print("Conversion completada exitosamente")
        return True
        
    except Exception as e:
        print(f"Error en conversion Word a PDF: {e}")
        return False

def excel_to_pdf(archivo_origen, archivo_destino):
    """Convierte Excel a PDF usando COM (Windows)"""
    try:
        import win32com.client as win32
        
        print("Iniciando conversion de Excel a PDF...")
        
        # Crear aplicaci�n Excel
        excel = win32.Dispatch('Excel.Application')
        excel.Visible = False
        excel.DisplayAlerts = False  # Desactivar alertas para mayor velocidad
        
        # Abrir libro
        print(f"Abriendo libro: {archivo_origen}")
        workbook = excel.Workbooks.Open(archivo_origen)
        
        # Exportar como PDF
        print(f"Guardando como PDF: {archivo_destino}")
        workbook.ExportAsFixedFormat(0, archivo_destino)
        
        # Cerrar libro y aplicaci�n
        workbook.Close()
        excel.Quit()
        
        print("Conversion completada exitosamente")
        return True
        
    except Exception as e:
        print(f"Error en conversion Excel a PDF: {e}")
        return False

def image_to_pdf(archivo_origen, archivo_destino):
    """Convierte imagen a PDF usando Pillow"""
    try:
        from PIL import Image
        
        print("Iniciando conversion de imagen a PDF...")
        
        # Abrir imagen
        print(f"Abriendo imagen: {archivo_origen}")
        imagen = Image.open(archivo_origen)
        
        # Convertir a RGB si es necesario
        if imagen.mode != 'RGB':
            imagen = imagen.convert('RGB')
        
        # Guardar como PDF
        print(f"Guardando como PDF: {archivo_destino}")
        imagen.save(archivo_destino, "PDF")
        
        print("Conversion completada exitosamente")
        return True
        
    except Exception as e:
        print(f"Error en conversion imagen a PDF: {e}")
        return False

def pdf_to_word(archivo_origen, archivo_destino):
    """Convierte PDF a Word usando pdf2docx"""
    try:
        from pdf2docx import Converter
        
        print("Iniciando conversion de PDF a Word...")
        
        # Crear convertidor
        print(f"Procesando archivo: {archivo_origen}")
        cv = Converter(archivo_origen)
        
        # Convertir
        print(f"Guardando como Word: {archivo_destino}")
        cv.convert(archivo_destino, start=0, end=None)
        cv.close()
        
        print("Conversion completada exitosamente")
        return True
        
    except Exception as e:
        print(f"Error en conversion PDF a Word: {e}")
        return False

def main():
    if len(sys.argv) != 4:
        print("Uso: python convertir_pdf.py <archivo_origen> <carpeta_destino> <tipo_conversion>")
        print("Tipos de conversion: word_to_pdf, excel_to_pdf, image_to_pdf, pdf_to_word")
        sys.exit(1)
    
    archivo_origen = sys.argv[1]
    carpeta_destino = sys.argv[2]
    tipo_conversion = sys.argv[3]
    
    print(f"Archivo origen: {archivo_origen}")
    print(f"Carpeta destino: {carpeta_destino}")
    print(f"Tipo de conversion: {tipo_conversion}")
    print("-" * 50)
    
    # Verificar archivo origen
    if not os.path.exists(archivo_origen):
        print(f"Error: El archivo origen no existe: {archivo_origen}")
        sys.exit(1)
    
    # Crear carpeta destino si no existe
    Path(carpeta_destino).mkdir(parents=True, exist_ok=True)
    
    # Verificar dependencias
    if not verificar_dependencias():
        print("Error: No se pudieron instalar todas las dependencias necesarias")
        sys.exit(1)
    
    # Determinar nombre del archivo destino
    nombre_base = Path(archivo_origen).stem
    
    if tipo_conversion == "word_to_pdf":
        archivo_destino = os.path.join(carpeta_destino, f"{nombre_base}.pdf")
        exito = word_to_pdf(archivo_origen, archivo_destino)
    elif tipo_conversion == "excel_to_pdf":
        archivo_destino = os.path.join(carpeta_destino, f"{nombre_base}.pdf")
        exito = excel_to_pdf(archivo_origen, archivo_destino)
    elif tipo_conversion == "image_to_pdf":
        archivo_destino = os.path.join(carpeta_destino, f"{nombre_base}.pdf")
        exito = image_to_pdf(archivo_origen, archivo_destino)
    elif tipo_conversion == "pdf_to_word":
        archivo_destino = os.path.join(carpeta_destino, f"{nombre_base}.docx")
        exito = pdf_to_word(archivo_origen, archivo_destino)
    else:
        print(f"Error: Tipo de conversi�n no soportado: {tipo_conversion}")
        sys.exit(1)
    
    if exito:
        print(f"�Archivo convertido exitosamente en: {archivo_destino}")
        sys.exit(0)
    else:
        print("Error: La conversion fallo")
        sys.exit(1)

if __name__ == "__main__":
    main()