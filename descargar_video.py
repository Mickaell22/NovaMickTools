#!/usr/bin/env python3
"""
Script para descargar videos usando yt-dlp
Uso: python descargar_video.py <url> <carpeta_destino> [opciones_calidad] [opciones_audio]
"""

import sys
import os
import subprocess
import json
from pathlib import Path

# Configurar encoding UTF-8 para evitar errores de codificacion
if sys.platform.startswith('win'):
    import codecs
    sys.stdout = codecs.getwriter('utf-8')(sys.stdout.detach())
    sys.stderr = codecs.getwriter('utf-8')(sys.stderr.detach())

def verificar_yt_dlp():
    """Verifica si yt-dlp est� instalado y lo actualiza"""
    try:
        subprocess.run(['yt-dlp', '--version'], 
                      capture_output=True, check=True)
        
        # Actualizar yt-dlp para evitar problemas con sitios
        print("Actualizando yt-dlp...")
        try:
            subprocess.run(['yt-dlp', '--update'], 
                          capture_output=True, check=False, timeout=30)
        except:
            pass  # No fallar si la actualización falla
        return True
    except (subprocess.CalledProcessError, FileNotFoundError):
        return False

def instalar_yt_dlp():
    """Instala yt-dlp usando pip"""
    print("yt-dlp no encontrado. Instalando...")
    try:
        subprocess.run([sys.executable, '-m', 'pip', 'install', 'yt-dlp'], 
                      check=True)
        print("yt-dlp instalado exitosamente")
        return True
    except subprocess.CalledProcessError as e:
        print(f"Error instalando yt-dlp: {e}")
        return False

def obtener_info_video(url):
    """Obtiene informaci�n del video sin descargarlo"""
    try:
        cmd = ['yt-dlp', '--dump-json', '--no-warnings', url]
        result = subprocess.run(cmd, capture_output=True, text=True, check=True)
        info = json.loads(result.stdout)
        return {
            'title': info.get('title', 'Video sin t�tulo'),
            'duration': info.get('duration', 0),
            'uploader': info.get('uploader', 'Desconocido'),
            'formats': len(info.get('formats', [])),
            'resolution': info.get('height', 'Desconocida')
        }
    except Exception as e:
        print(f"Error obteniendo informaci�n del video: {e}")
        return None

def listar_formatos_disponibles(url):
    """Lista los formatos disponibles para el video"""
    try:
        cmd = ['yt-dlp', '--list-formats', '--no-warnings', url]
        result = subprocess.run(cmd, capture_output=True, text=True, check=True)
        print("Formatos disponibles:")
        print(result.stdout)
        return True
    except Exception as e:
        print(f"Error listando formatos: {e}")
        return False

def detectar_plataforma(url):
    """Detecta la plataforma del video para optimizaciones específicas"""
    url_lower = url.lower()
    if 'facebook.com' in url_lower or 'fb.watch' in url_lower:
        return 'facebook'
    elif 'tiktok.com' in url_lower:
        return 'tiktok'
    elif 'youtube.com' in url_lower or 'youtu.be' in url_lower:
        return 'youtube'
    elif 'instagram.com' in url_lower:
        return 'instagram'
    elif 'twitter.com' in url_lower or 'x.com' in url_lower:
        return 'twitter'
    else:
        return 'other'

def aplicar_optimizaciones_plataforma(cmd, plataforma, opciones_calidad="", opciones_audio=""):
    """Aplica optimizaciones específicas por plataforma"""
    
    print(f"DEBUG - Aplicando optimizaciones:")
    print(f"  Plataforma: {plataforma}")
    print(f"  Opciones de calidad recibidas: '{opciones_calidad}'")
    
    # Aplicar formato de calidad con fallbacks para evitar errores 403
    if opciones_calidad and opciones_calidad.strip():
        cmd.extend(['--format', opciones_calidad])
        print(f"  Agregado formato: --format {opciones_calidad}")
    else:
        # Para máxima calidad, usar formato que prefiera HTTPS y tenga fallbacks
        if plataforma == 'youtube':
            formato_calidad = 'bestvideo[height<=2160][protocol^=https]+bestaudio[protocol^=https]/best[height<=2160]/bestvideo+bestaudio/best'
            cmd.extend(['--format', formato_calidad])
            print(f"  Agregado formato optimizado para YouTube: --format {formato_calidad}")
        else:
            # Para otras plataformas, sin formato específico
            print(f"  Sin formato específico - yt-dlp elegirá automáticamente la mejor calidad")
    
    # Forzar conversión de audio a AAC para mejor compatibilidad
    if not (opciones_audio and '--extract-audio' in opciones_audio):
        cmd.extend(['--postprocessor-args', 'ffmpeg:-c:a aac'])
        cmd.extend(['--recode-video', 'mp4'])  # Asegurar contenedor MP4
        print(f"  Forzando conversión de audio a AAC y contenedor MP4 para compatibilidad")
    
    # Luego aplicar optimizaciones mínimas solo si es necesario
    if plataforma == 'youtube':
        # Solo agregar si hay problemas con fragmentos
        print("  No se agregan optimizaciones para YouTube (por ahora)")
        pass  # Por ahora no agregar nada que pueda interferir
        
    elif plataforma == 'facebook':
        cmd.extend([
            '--extractor-args', 'facebook:logged_in_tab=false'
        ])
        print("  Agregadas optimizaciones para Facebook")
            
    return cmd

def descargar_video(url, carpeta_destino, opciones_calidad="", opciones_audio=""):
    """Descarga el video con las opciones especificadas"""
    
    # Crear carpeta si no existe
    Path(carpeta_destino).mkdir(parents=True, exist_ok=True)
    
    # Detectar plataforma para optimizaciones
    plataforma = detectar_plataforma(url)
    print(f"Plataforma detectada: {plataforma}")
    
    # Construir comando
    cmd = ['yt-dlp']
    
    # Opciones básicas (como funcionaba antes)
    cmd.extend([
        '--merge-output-format', 'mp4'  # Solo forzar MP4
    ])
    
    # No agregar formato aquí, se maneja en las optimizaciones de plataforma
    
    # Agregar opciones de audio
    if opciones_audio:
        if '--extract-audio' in opciones_audio:
            cmd.append('--extract-audio')
            # Eliminar opciones de video si solo queremos audio
            cmd = [c for c in cmd if not c.startswith('bestvideo')]
        if '--audio-format' in opciones_audio:
            parts = opciones_audio.split()
            for i, part in enumerate(parts):
                if part == '--audio-format' and i + 1 < len(parts):
                    cmd.extend(['--audio-format', parts[i + 1]])
                    break
    
    # Aplicar optimizaciones específicas por plataforma
    cmd = aplicar_optimizaciones_plataforma(cmd, plataforma, opciones_calidad, opciones_audio)
    
    # Configurar nombre de archivo y progreso
    cmd.extend([
        '-o', os.path.join(carpeta_destino, '%(title)s [%(uploader)s].%(ext)s'),
        '--progress',
        '--newline'  # Facilita el parsing del progreso
    ])
    
    # Agregar URL
    cmd.append(url)
    
    print(f"Ejecutando: {' '.join(cmd)}")
    
    try:
        # Ejecutar descarga
        process = subprocess.Popen(
            cmd,
            stdout=subprocess.PIPE,
            stderr=subprocess.STDOUT,
            universal_newlines=True,
            bufsize=1
        )
        
        # Mostrar progreso en tiempo real
        for line in process.stdout:
            print(line.strip())
        
        # Esperar a que termine
        process.wait()
        
        if process.returncode == 0:
            print("Descarga completada exitosamente!")
            return True
        else:
            print(f"Error en la descarga. Codigo de salida: {process.returncode}")
            return False
            
    except Exception as e:
        print(f"Error durante la descarga: {e}")
        return False

def main():
    if len(sys.argv) < 3:
        print("Uso: python descargar_video.py <url> <carpeta_destino> [opciones_calidad] [opciones_audio]")
        print("Ejemplo: python descargar_video.py 'https://youtube.com/watch?v=...' 'C:/Downloads' '--format best' '--extract-audio --audio-format mp3'")
        sys.exit(1)
    
    url = sys.argv[1]
    carpeta_destino = sys.argv[2]
    opciones_calidad = sys.argv[3] if len(sys.argv) > 3 else ""
    opciones_audio = " ".join(sys.argv[4:]) if len(sys.argv) > 4 else ""
    
    # Detectar plataforma
    plataforma = detectar_plataforma(url)
    
    print(f"DEBUG - Argumentos recibidos:")
    print(f"  URL: {url}")
    print(f"  Plataforma detectada: {plataforma}")
    print(f"  Carpeta destino: {carpeta_destino}")
    print(f"  Opciones de calidad recibidas: '{opciones_calidad}'")
    print(f"  Opciones de audio: '{opciones_audio}'")
    print("-" * 50)
    
    # Mostrar formatos disponibles ANTES de descargar
    print("FORMATOS DISPONIBLES:")
    listar_formatos_disponibles(url)
    print("-" * 50)
    
    # Verificar yt-dlp
    if not verificar_yt_dlp():
        if not instalar_yt_dlp():
            print("No se pudo instalar yt-dlp. Instalalo manualmente con: pip install yt-dlp")
            sys.exit(1)
    
    # Obtener informacion del video
    print("Obteniendo informacion del video...")
    info = obtener_info_video(url)
    if info:
        print(f"Titulo: {info['title']}")
        print(f"Autor: {info['uploader']}")
        if info['duration']:
            minutos = int(info['duration']) // 60
            segundos = int(info['duration']) % 60
            print(f"Duracion: {minutos}:{segundos:02d}")
        print(f"Resolucion m�xima: {info['resolution']}p" if info['resolution'] != 'Desconocida' else "Resolucion: Desconocida")
        print(f"Formatos disponibles: {info['formats']}")
        print("-" * 50)
        
        # Listar formatos disponibles para debugging
        if opciones_calidad and opciones_calidad != "best":
            print("Listando formatos disponibles para verificar calidad...")
            listar_formatos_disponibles(url)
            print("-" * 50)
    
    # Iniciar descarga
    print("Iniciando descarga...")
    success = descargar_video(url, carpeta_destino, opciones_calidad, opciones_audio)
    
    sys.exit(0 if success else 1)

if __name__ == "__main__":
    main()