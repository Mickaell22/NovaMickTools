using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista.PythonTools
{
    public class VideoDescargado
    {
        public string Titulo { get; set; }
        public string Url { get; set; }
        public string Duracion { get; set; }
        public string Calidad { get; set; }
        public string Tamaño { get; set; }
        public DateTime FechaDescarga { get; set; }
        public bool Exitoso { get; set; }
    }

    public partial class FormDescargaVideos : Form
    {
        private bool descargando = false;
        private Process procesoDescarga;
        
        // Variables para descarga por lotes
        private List<string> listaUrls = new List<string>();
        private int indiceUrlActual = 0;
        private List<VideoDescargado> videosDescargados = new List<VideoDescargado>();
        private bool esModoLote = false;

        public FormDescargaVideos()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configurar txtUrl como TextArea multilinea
            txtUrl.Multiline = true;
            txtUrl.ScrollBars = ScrollBars.Vertical;
            txtUrl.AcceptsReturn = true;
            txtUrl.AcceptsTab = false;
            txtUrl.WordWrap = true;
            
            // Si el TextBox es muy pequeño, aumentar la altura y ajustar elementos
            if (txtUrl.Height < 60)
            {
                int alturaAnterior = txtUrl.Height;
                txtUrl.Height = 80;
                int diferencia = txtUrl.Height - alturaAnterior;
                
                // Reposicionar elementos que están debajo del txtUrl
                AjustarPosicionesElementos(diferencia);
            }
            
            // Configurar placeholder para URL
            if (string.IsNullOrEmpty(txtUrl.Text) || txtUrl.Text.StartsWith("Pega aquí"))
            {
                txtUrl.ForeColor = Color.Gray;
                txtUrl.Text = "Pega aquí uno o varios enlaces de videos (uno por línea)...";
            }

            // Configurar placeholder para carpeta
            if (string.IsNullOrEmpty(txtCarpeta.Text) || txtCarpeta.Text == "Selecciona carpeta de descarga...")
            {
                txtCarpeta.ForeColor = Color.Gray;
                txtCarpeta.Text = "Selecciona carpeta de descarga...";
            }

            // Configurar carpeta por defecto
            string carpetaDescargas = @"C:\Users\ASUS\Downloads\MisVideos";
            
            // Crear la carpeta si no existe
            try
            {
                if (!Directory.Exists(carpetaDescargas))
                {
                    Directory.CreateDirectory(carpetaDescargas);
                }
                txtCarpeta.Text = carpetaDescargas;
                txtCarpeta.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                // Si hay error creando la carpeta, usar Downloads genérico
                string carpetaAlternativa = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                txtCarpeta.Text = carpetaAlternativa;
                txtCarpeta.ForeColor = Color.Black;
            }
        }

        private void AjustarPosicionesElementos(int diferencia)
        {
            // Usar el Bottom del txtUrl como referencia para mover elementos debajo
            int posicionLimite = txtUrl.Bottom + 5; // 5px de margen
            
            // Crear lista de controles a mover (excluyendo txtUrl)
            var controlesAMover = new List<System.Windows.Forms.Control>();
            
            // Revisar todos los controles del formulario
            foreach (System.Windows.Forms.Control control in this.Controls)
            {
                if (control != txtUrl && control.Top >= posicionLimite - diferencia)
                {
                    controlesAMover.Add(control);
                }
            }
            
            // Mover los controles identificados
            foreach (var control in controlesAMover)
            {
                control.Top += diferencia;
                
                // Si es un contenedor, también ajustar sus controles hijos si es necesario
                if ((control is Panel || control is GroupBox) && control.Controls.Count > 0)
                {
                    foreach (System.Windows.Forms.Control subControl in control.Controls)
                    {
                        // Los controles hijos se mueven automáticamente con el contenedor padre
                        // pero podríamos hacer ajustes adicionales aquí si fuera necesario
                    }
                }
            }
            
            // Ajustar altura del formulario para acomodar todos los controles
            try
            {
                int maxBottom = this.Controls.Cast<System.Windows.Forms.Control>().Max(c => c.Bottom);
                if (this.Height < maxBottom + 50)
                {
                    this.Height = maxBottom + 50;
                }
            }
            catch
            {
                // Si hay error calculando, usar un incremento seguro
                this.Height += diferencia + 20;
            }
        }

        private void txtUrl_Enter(object sender, EventArgs e)
        {
            if (txtUrl.Text.StartsWith("Pega aquí"))
            {
                txtUrl.Text = "";
                txtUrl.ForeColor = Color.Black;
            }
        }

        private void txtUrl_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                txtUrl.Text = "Pega aquí uno o varios enlaces de videos (uno por línea)...";
                txtUrl.ForeColor = Color.Gray;
            }
        }

        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtCarpeta.Text = folderBrowserDialog.SelectedPath;
                txtCarpeta.ForeColor = Color.Black;
            }
        }

        private async void btnDescargar_Click(object sender, EventArgs e)
        {
            if (descargando)
            {
                // Cancelar descarga
                if (procesoDescarga != null && !procesoDescarga.HasExited)
                {
                    procesoDescarga.Kill();
                }
                ResetearFormulario();
                return;
            }

            // Validaciones
            if (!ValidarFormulario())
                return;

            try
            {
                descargando = true;
                btnDescargar.Text = "Cancelar";
                btnDescargar.BackColor = Color.FromArgb(217, 50, 50);

                await IniciarDescarga();
            }
            catch (Exception ex)
            {
                AgregarLog($"Error: {ex.Message}");
                MessageBox.Show($"Error durante la descarga: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ResetearFormulario();
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtUrl.Text) || txtUrl.Text == "Pega aquí el enlace del video...")
            {
                MessageBox.Show("Por favor, ingresa una o más URLs válidas.", "URL requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUrl.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCarpeta.Text) || txtCarpeta.Text == "Selecciona carpeta de descarga...")
            {
                MessageBox.Show("Por favor, selecciona una carpeta de descarga.", "Carpeta requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Directory.Exists(txtCarpeta.Text))
            {
                MessageBox.Show("La carpeta seleccionada no existe.", "Carpeta inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async Task IniciarDescarga()
        {
            // Procesar múltiples URLs
            string[] urls = txtUrl.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            urls = urls.Select(url => url.Trim())
                      .Where(url => !string.IsNullOrEmpty(url) && 
                                   !url.StartsWith("Pega aquí") && 
                                   (url.StartsWith("http://") || url.StartsWith("https://")))
                      .ToArray();
            
            listaUrls = urls.ToList();
            indiceUrlActual = 0;
            esModoLote = urls.Length > 1;
            
            if (urls.Length == 0)
            {
                MessageBox.Show("No se encontraron URLs válidas.\n\nAsegúrate de que:\n- Cada URL esté en una línea separada\n- Las URLs comiencen con http:// o https://",
                    "URLs no válidas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (esModoLote)
            {
                string urlsDetectadas = string.Join("\n", urls.Take(3)) + (urls.Length > 3 ? "\n..." : "");
                MessageBox.Show($"Se detectaron {urls.Length} URLs para descargar:\n\n{urlsDetectadas}\n\nSe procesarán una por una.",
                    "Descarga por Lotes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            lblEstado.Text = esModoLote ? $"Iniciando descarga por lotes... (1 de {listaUrls.Count})" : "Iniciando descarga...";
            progressBar.Style = ProgressBarStyle.Marquee;
            AgregarLog(esModoLote ? $"Iniciando descarga por lotes de {listaUrls.Count} videos..." : "Iniciando descarga...");

            await ProcesarSiguienteUrl();
        }

        private async Task ProcesarSiguienteUrl()
        {
            if (indiceUrlActual >= listaUrls.Count)
            {
                FinalizarDescargaLote();
                return;
            }
            
            string urlActual = listaUrls[indiceUrlActual];
            AgregarLog($"Procesando video {indiceUrlActual + 1} de {listaUrls.Count}: {urlActual}");
            
            // Buscar el script en la raíz del proyecto
            string baseDir = Application.StartupPath;
            string pythonScript = Path.Combine(baseDir, "..", "..", "..", "descargar_video.py");
            pythonScript = Path.GetFullPath(pythonScript); // Normalizar la ruta

            if (!File.Exists(pythonScript))
            {
                MessageBox.Show($"Script de Python no encontrado en:\n{pythonScript}\n\n" +
                    "Por favor, asegúrate de que el archivo descargar_video.py esté en la raíz del proyecto.",
                    "Script no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // CORREGIR - Obtener solo el formato sin --format
            string formatoCalidad = ObtenerFormatoCalidad(); // Nuevo método
            string audioOptions = chkSoloAudio.Checked ? "--extract-audio --audio-format mp3" : "";

            // Construir argumentos correctamente
            string argumentos;
            if (chkSoloAudio.Checked)
            {
                argumentos = $"\"{pythonScript}\" \"{urlActual}\" \"{txtCarpeta.Text}\" \"{formatoCalidad}\" \"{audioOptions}\"";
            }
            else
            {
                argumentos = $"\"{pythonScript}\" \"{urlActual}\" \"{txtCarpeta.Text}\" \"{formatoCalidad}\"";
            }

            AgregarLog($"DEBUG - Video {indiceUrlActual + 1}/{listaUrls.Count} - Argumentos: {argumentos}");
            AgregarLog($"DEBUG - Formato de calidad: {formatoCalidad}");


            // Configurar proceso
            // Configurar proceso
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = argumentos,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                WorkingDirectory = Application.StartupPath
            };

            procesoDescarga = new Process { StartInfo = startInfo };

            // Configurar eventos para capturar salida
            procesoDescarga.OutputDataReceived += (s, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    this.Invoke(new Action(() => {
                        AgregarLog(e.Data);
                        ActualizarProgreso(e.Data);
                    }));
                }
            };

            procesoDescarga.ErrorDataReceived += (s, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    this.Invoke(new Action(() => AgregarLog($"ERROR: {e.Data}")));
                }
            };

            // Iniciar proceso
            procesoDescarga.Start();
            procesoDescarga.BeginOutputReadLine();
            procesoDescarga.BeginErrorReadLine();

            // Esperar a que termine
            await Task.Run(() => procesoDescarga.WaitForExit());
            
            // Verificar resultado y registrar estadísticas
            bool exitoso = procesoDescarga.ExitCode == 0;
            
            // Registrar estadísticas del video descargado
            var videoDescargado = new VideoDescargado
            {
                Url = urlActual,
                Titulo = ExtraerTituloDelLog(),
                Duracion = ExtraerDuracionDelLog(),
                Calidad = formatoCalidad,
                Tamaño = ExtraerTamañoDelLog(),
                FechaDescarga = DateTime.Now,
                Exitoso = exitoso
            };
            videosDescargados.Add(videoDescargado);
            
            if (exitoso)
            {
                AgregarLog($"¡Video {indiceUrlActual + 1} completado exitosamente!");
                lblEstado.Text = esModoLote ? 
                    $"Video {indiceUrlActual + 1} de {listaUrls.Count} completado" : 
                    "Descarga completada exitosamente";
            }
            else
            {
                AgregarLog($"Error en video {indiceUrlActual + 1}. Código: {procesoDescarga.ExitCode}");
                lblEstado.Text = esModoLote ? 
                    $"Error en video {indiceUrlActual + 1} de {listaUrls.Count}" : 
                    "Error en la descarga";
            }
            
            // Continuar con el siguiente video o finalizar
            indiceUrlActual++;
            if (esModoLote && indiceUrlActual < listaUrls.Count)
            {
                AgregarLog($"--- Continuando con video {indiceUrlActual + 1} de {listaUrls.Count} ---");
                await ProcesarSiguienteUrl();
            }
            else if (!esModoLote && exitoso)
            {
                MessageBox.Show("¡Descarga completada exitosamente!", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                progressBar.Value = 100;
            }
        }

        private void FinalizarDescargaLote()
        {
            int exitosos = videosDescargados.Count(v => v.Exitoso);
            int fallidos = videosDescargados.Count - exitosos;
            
            lblEstado.Text = "Descarga por lotes completada";
            progressBar.Value = 100;
            
            AgregarLog("=== DESCARGA POR LOTES COMPLETADA ===");
            AgregarLog($"Total videos: {videosDescargados.Count}");
            AgregarLog($"Exitosos: {exitosos}");
            AgregarLog($"Fallidos: {fallidos}");
            AgregarLog("=== ESTADÍSTICAS ===");
            
            MostrarEstadisticas();
            
            MessageBox.Show($"Descarga por lotes completada!\n\n" +
                          $"Videos procesados: {videosDescargados.Count}\n" +
                          $"Exitosos: {exitosos}\n" +
                          $"Fallidos: {fallidos}",
                          "Lote Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string ExtraerTituloDelLog()
        {
            // Buscar en las últimas líneas del log el título del video
            try
            {
                string[] lineas = txtLog.Lines;
                for (int i = lineas.Length - 1; i >= 0; i--)
                {
                    if (lineas[i].Contains("Titulo:"))
                    {
                        return lineas[i].Substring(lineas[i].IndexOf("Titulo:") + 7).Trim();
                    }
                }
            }
            catch { }
            return "Título no disponible";
        }

        private string ExtraerDuracionDelLog()
        {
            try
            {
                string[] lineas = txtLog.Lines;
                for (int i = lineas.Length - 1; i >= 0; i--)
                {
                    if (lineas[i].Contains("Duracion:"))
                    {
                        return lineas[i].Substring(lineas[i].IndexOf("Duracion:") + 9).Trim();
                    }
                }
            }
            catch { }
            return "N/A";
        }

        private string ExtraerTamañoDelLog()
        {
            try
            {
                string[] lineas = txtLog.Lines;
                for (int i = lineas.Length - 1; i >= 0; i--)
                {
                    if (lineas[i].Contains("MB") || lineas[i].Contains("GB") || lineas[i].Contains("KB"))
                    {
                        // Buscar patrones como "Downloaded 123.45MB" o similar
                        var palabras = lineas[i].Split(' ');
                        foreach (var palabra in palabras)
                        {
                            if (palabra.Contains("MB") || palabra.Contains("GB") || palabra.Contains("KB"))
                                return palabra;
                        }
                    }
                }
            }
            catch { }
            return "N/A";
        }

        private void MostrarEstadisticas()
        {
            AgregarLog("--- ESTADÍSTICAS DETALLADAS ---");
            foreach (var video in videosDescargados)
            {
                string estado = video.Exitoso ? "✓ ÉXITO" : "✗ ERROR";
                AgregarLog($"{estado} | {video.Titulo} | {video.Duracion} | {video.Tamaño}");
            }
            
            // Calcular estadísticas generales
            var exitosos = videosDescargados.Where(v => v.Exitoso).ToList();
            if (exitosos.Any())
            {
                AgregarLog("--- RESUMEN ---");
                AgregarLog($"Videos exitosos: {exitosos.Count}");
                AgregarLog($"Tiempo total estimado: {CalcularTiempoTotal(exitosos)}");
            }
        }

        private string CalcularTiempoTotal(List<VideoDescargado> videos)
        {
            try
            {
                int totalSegundos = 0;
                foreach (var video in videos)
                {
                    if (video.Duracion.Contains(":"))
                    {
                        var partes = video.Duracion.Split(':');
                        if (partes.Length >= 2)
                        {
                            int mins = int.Parse(partes[0]);
                            int segs = int.Parse(partes[1]);
                            totalSegundos += mins * 60 + segs;
                        }
                    }
                }
                
                int horas = totalSegundos / 3600;
                int minutosFinales = (totalSegundos % 3600) / 60;
                int segundosFinales = totalSegundos % 60;
                
                return $"{horas:00}:{minutosFinales:00}:{segundosFinales:00}";
            }
            catch
            {
                return "N/A";
            }
        }

        private string ObtenerFormatoCalidad()
        {
            switch (cmbCalidad.SelectedIndex)
            {
                case 0: return ""; // Máxima calidad (automático con optimizaciones por plataforma)
                case 1: return "bestvideo[height<=1080][protocol^=https]+bestaudio[protocol^=https]/bestvideo[height<=1080]+bestaudio/best[height<=1080]"; // 1080p con fallbacks
                case 2: return "bestvideo[height<=720][protocol^=https]+bestaudio[protocol^=https]/bestvideo[height<=720]+bestaudio/best[height<=720]"; // 720p con fallbacks
                case 3: return "bestvideo[height<=480][protocol^=https]+bestaudio[protocol^=https]/bestvideo[height<=480]+bestaudio/best[height<=480]"; // 480p con fallbacks
                case 4: return "bestvideo[height<=360][protocol^=https]+bestaudio[protocol^=https]/bestvideo[height<=360]+bestaudio/best[height<=360]"; // 360p con fallbacks
                default: return ""; // Sin formato específico
            }
        }

        private void ActualizarProgreso(string salida)
        {
            // Buscar patrones de progreso en la salida de yt-dlp
            if (salida.Contains("%"))
            {
                try
                {
                    var partes = salida.Split(' ');
                    foreach (var parte in partes)
                    {
                        if (parte.Contains("%"))
                        {
                            string porcentajeStr = parte.Replace("%", "").Trim();
                            if (double.TryParse(porcentajeStr, out double porcentaje))
                            {
                                progressBar.Style = ProgressBarStyle.Continuous;
                                progressBar.Value = Math.Min(100, Math.Max(0, (int)porcentaje));
                                lblEstado.Text = $"Descargando... {porcentaje:F1}%";
                                break;
                            }
                        }
                    }
                }
                catch { /* Ignorar errores de parsing */ }
            }
        }

        private void AgregarLog(string mensaje)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action<string>(AgregarLog), mensaje);
                return;
            }

            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {mensaje}{Environment.NewLine}");
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        private void ResetearFormulario()
        {
            descargando = false;
            btnDescargar.Text = "Descargar";
            btnDescargar.BackColor = Color.FromArgb(50, 217, 55);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = 0;
            lblEstado.Text = "Listo";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtUrl.Text = "Pega aquí uno o varios enlaces de videos (uno por línea)...";
            txtUrl.ForeColor = Color.Gray;
            txtLog.Clear();
            progressBar.Value = 0;
            lblEstado.Text = "Listo";
            cmbCalidad.SelectedIndex = 0;
            chkSoloAudio.Checked = false;
            
            // Limpiar variables de lotes
            listaUrls.Clear();
            videosDescargados.Clear();
            indiceUrlActual = 0;
            esModoLote = false;
        }
    }
}