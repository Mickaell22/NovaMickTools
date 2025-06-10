using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista.PythonTools
{
    public partial class FormDescargaVideos : Form
    {
        private bool descargando = false;
        private Process procesoDescarga;

        public FormDescargaVideos()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configurar placeholder para URL
            if (string.IsNullOrEmpty(txtUrl.Text) || txtUrl.Text == "Pega aquí el enlace del video...")
            {
                txtUrl.ForeColor = Color.Gray;
                txtUrl.Text = "Pega aquí el enlace del video...";
            }

            // Configurar placeholder para carpeta
            if (string.IsNullOrEmpty(txtCarpeta.Text) || txtCarpeta.Text == "Selecciona carpeta de descarga...")
            {
                txtCarpeta.ForeColor = Color.Gray;
                txtCarpeta.Text = "Selecciona carpeta de descarga...";
            }

            // Configurar carpeta por defecto (Descargas del usuario)
            string carpetaDescargas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "VideosDescargados");
            if (Directory.Exists(carpetaDescargas))
            {
                txtCarpeta.Text = carpetaDescargas;
                txtCarpeta.ForeColor = Color.Black;
            }
        }

        private void txtUrl_Enter(object sender, EventArgs e)
        {
            if (txtUrl.Text == "Pega aquí el enlace del video...")
            {
                txtUrl.Text = "";
                txtUrl.ForeColor = Color.Black;
            }
        }

        private void txtUrl_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                txtUrl.Text = "Pega aquí el enlace del video...";
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
                MessageBox.Show("Por favor, ingresa una URL válida.", "URL requerida",
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
            string pythonScript = Path.Combine(Application.StartupPath, "PythonScripts", "descargar_video.py");

            if (!File.Exists(pythonScript))
            {
                MessageBox.Show($"Script de Python no encontrado: {pythonScript}\n\n" +
                    "Por favor, asegúrate de que el archivo descargar_video.py esté en la carpeta PythonScripts.",
                    "Script no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblEstado.Text = "Iniciando descarga...";
            progressBar.Style = ProgressBarStyle.Marquee;
            AgregarLog("Iniciando descarga...");

            // CORREGIR - Obtener solo el formato sin --format
            string formatoCalidad = ObtenerFormatoCalidad(); // Nuevo método
            string audioOptions = chkSoloAudio.Checked ? "--extract-audio --audio-format mp3" : "";

            // Construir argumentos correctamente
            string argumentos;
            if (chkSoloAudio.Checked)
            {
                argumentos = $"\"{pythonScript}\" \"{txtUrl.Text}\" \"{txtCarpeta.Text}\" \"{formatoCalidad}\" \"{audioOptions}\"";
            }
            else
            {
                argumentos = $"\"{pythonScript}\" \"{txtUrl.Text}\" \"{txtCarpeta.Text}\" \"{formatoCalidad}\"";
            }

            AgregarLog($"DEBUG - Argumentos completos: {argumentos}");
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
            // Verificar resultado
            if (procesoDescarga.ExitCode == 0)
            {
                lblEstado.Text = "Descarga completada exitosamente";
                progressBar.Value = 100;
                AgregarLog("¡Descarga completada!");
                MessageBox.Show("¡Descarga completada exitosamente!", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblEstado.Text = "Error en la descarga";
                AgregarLog($"Proceso terminó con código de error: {procesoDescarga.ExitCode}");
            }
        }

        private string ObtenerFormatoCalidad()
        {
            switch (cmbCalidad.SelectedIndex)
            {
                case 0: return "bestvideo[ext=mp4]+bestaudio[ext=m4a]/best"; // Mejor calidad combinada
                case 1: return "bestvideo[height<=1080][ext=mp4]+bestaudio[ext=m4a]/best[height<=1080]"; // 1080p
                case 2: return "bestvideo[height<=720][ext=mp4]+bestaudio[ext=m4a]/best[height<=720]"; // 720p
                case 3: return "bestvideo[height<=480][ext=mp4]+bestaudio[ext=m4a]/best[height<=480]"; // 480p
                case 4: return "bestvideo[height<=360][ext=mp4]+bestaudio[ext=m4a]/best[height<=360]"; // 360p
                default: return "bestvideo[ext=mp4]+bestaudio[ext=m4a]/best";
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
            txtUrl.Text = "Pega aquí el enlace del video...";
            txtUrl.ForeColor = Color.Gray;
            txtLog.Clear();
            progressBar.Value = 0;
            lblEstado.Text = "Listo";
            cmbCalidad.SelectedIndex = 0;
            chkSoloAudio.Checked = false;
        }
    }
}