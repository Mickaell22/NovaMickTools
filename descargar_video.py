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

def verificar_yt_dlp():
    """Verifica si yt-dlp est� instalado"""
    try:
        subprocess.run(['yt-dlp', '--version'], 
                      capture_output=True, check=True)
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
            'formats': len(info.get('formats', []))
        }
    except Exception as e:
        print(f"Error obteniendo informaci�n del video: {e}")
        return None

def descargar_video(url, carpeta_destino, opciones_calidad="", opciones_audio=""):
    """Descarga el video con las opciones especificadas"""
    
    # Crear carpeta si no existe
    Path(carpeta_destino).mkdir(parents=True, exist_ok=True)
    
    # Construir comando
    cmd = ['yt-dlp']
    
    # Agregar opciones de calidad
    if opciones_calidad:
        if opciones_calidad.startswith('--format'):
            cmd.extend(opciones_calidad.split())
        else:
            cmd.extend(['--format', opciones_calidad])
    
    # Agregar opciones de audio
    if opciones_audio:
        if '--extract-audio' in opciones_audio:
            cmd.append('--extract-audio')
        if '--audio-format' in opciones_audio:
            parts = opciones_audio.split()
            for i, part in enumerate(parts):
                if part == '--audio-format' and i + 1 < len(parts):
                    cmd.extend(['--audio-format', parts[i + 1]])
                    break
    
    # Configurar nombre de archivo y progreso
    if '--extract-audio' in cmd:
        cmd.extend([
            '-o', os.path.join(carpeta_destino, '%(title)s.%(ext)s'),
            '--progress'
        ])
    else:
        cmd.extend([
            '-o', os.path.join(carpeta_destino, '%(title)s.%(ext)s'),
            '--progress'
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
            print("�Descarga completada exitosamente!")
            return True
        else:
            print(f"Error en la descarga. C�digo de salida: {process.returncode}")
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
    
    print(f"URL: {url}")
    print(f"Carpeta destino: {carpeta_destino}")
    print(f"Opciones de calidad: {opciones_calidad}")
    print(f"Opciones de audio: {opciones_audio}")
    print("-" * 50)
    
    # Verificar yt-dlp
    if not verificar_yt_dlp():
        if not instalar_yt_dlp():
            print("No se pudo instalar yt-dlp. Instalalo manualmente con: pip install yt-dlp")
            sys.exit(1)
    
    # Obtener informaci�n del video
    print("Obteniendo informaci�n del video...")
    info = obtener_info_video(url)
    if info:
        print(f"T�tulo: {info['title']}")
        print(f"Autor: {info['uploader']}")
        if info['duration']:
            minutos = info['duration'] // 60
            segundos = info['duration'] % 60
            print(f"Duraci�n: {minutos}:{segundos:02d}")
        print(f"Formatos disponibles: {info['formats']}")
        print("-" * 50)
    
    # Iniciar descarga
    print("Iniciando descarga...")
    success = descargar_video(url, carpeta_destino, opciones_calidad, opciones_audio)
    
    sys.exit(0 if success else 1)

if __name__ == "__main__":
    main()