using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista.PythonTools
{
    public partial class FormConvertidorPDF : Form
    {
        private bool convirtiendo = false;
        private Process procesoConversion;

        public FormConvertidorPDF()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            // Configurar carpeta por defecto (Documentos del usuario)
            string carpetaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
            if (Directory.Exists(carpetaDocumentos))
            {
                txtCarpetaDestino.Text = carpetaDocumentos;
                txtCarpetaDestino.ForeColor = Color.Black;
            }

            // Configurar filtros del OpenFileDialog
            ActualizarFiltroArchivos();
            
            // Habilitar drag & drop
            ConfigurarDragDrop();
        }

        private void ConfigurarDragDrop()
        {
            // Habilitar drag & drop en el formulario
            this.AllowDrop = true;
            this.DragEnter += FormConvertidorPDF_DragEnter;
            this.DragDrop += FormConvertidorPDF_DragDrop;
            
            // También habilitar en el TextBox del archivo origen
            txtArchivoOrigen.AllowDrop = true;
            txtArchivoOrigen.DragEnter += FormConvertidorPDF_DragEnter;
            txtArchivoOrigen.DragDrop += FormConvertidorPDF_DragDrop;
        }

        private void FormConvertidorPDF_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] archivos = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (archivos.Length == 1 && EsArchivoValido(archivos[0]))
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void FormConvertidorPDF_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] archivos = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (archivos.Length == 1 && EsArchivoValido(archivos[0]))
                {
                    string archivo = archivos[0];
                    txtArchivoOrigen.Text = archivo;
                    txtArchivoOrigen.ForeColor = Color.Black;
                    
                    // Auto-detectar tipo de conversión basado en la extensión
                    DetectarTipoConversion(archivo);
                    
                    // Auto-configurar carpeta destino si está vacía
                    if (txtCarpetaDestino.Text == "Selecciona carpeta de destino..." ||
                        txtCarpetaDestino.ForeColor == Color.Gray)
                    {
                        string directorioArchivo = Path.GetDirectoryName(archivo);
                        txtCarpetaDestino.Text = directorioArchivo;
                        txtCarpetaDestino.ForeColor = Color.Black;
                    }
                    
                    AgregarLog($"Archivo arrastrado: {Path.GetFileName(archivo)}");
                }
            }
        }

        private bool EsArchivoValido(string archivo)
        {
            if (!File.Exists(archivo))
                return false;
                
            string extension = Path.GetExtension(archivo).ToLower();
            string[] extensionesValidas = { ".docx", ".doc", ".xlsx", ".xls", ".pdf", ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tiff" };
            
            return extensionesValidas.Contains(extension);
        }

        private void DetectarTipoConversion(string archivo)
        {
            string extension = Path.GetExtension(archivo).ToLower();
            
            switch (extension)
            {
                case ".docx":
                case ".doc":
                    rbWordaPDF.Checked = true;
                    break;
                case ".xlsx":
                case ".xls":
                    rbExcelaPDF.Checked = true;
                    break;
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".bmp":
                case ".gif":
                case ".tiff":
                    rbImagenaPDF.Checked = true;
                    break;
                case ".pdf":
                    rbPDFaWord.Checked = true;
                    break;
            }
            
            ActualizarFiltroArchivos();
        }

        private void TipoConversion_CheckedChanged(object sender, EventArgs e)
        {
            ActualizarFiltroArchivos();
            LimpiarSeleccionArchivo();
        }

        private void ActualizarFiltroArchivos()
        {
            if (rbWordaPDF.Checked)
            {
                openFileDialog.Filter = "Documentos Word (*.docx;*.doc)|*.docx;*.doc|Todos los archivos (*.*)|*.*";
                openFileDialog.Title = "Seleccionar documento Word";
            }
            else if (rbExcelaPDF.Checked)
            {
                openFileDialog.Filter = "Documentos Excel (*.xlsx;*.xls)|*.xlsx;*.xls|Todos los archivos (*.*)|*.*";
                openFileDialog.Title = "Seleccionar documento Excel";
            }
            else if (rbImagenaPDF.Checked)
            {
                openFileDialog.Filter = "Imágenes (*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.gif;*.tiff|Todos los archivos (*.*)|*.*";
                openFileDialog.Title = "Seleccionar imagen";
            }
            else if (rbPDFaWord.Checked)
            {
                openFileDialog.Filter = "Documentos PDF (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";
                openFileDialog.Title = "Seleccionar documento PDF";
            }
        }

        private void LimpiarSeleccionArchivo()
        {
            txtArchivoOrigen.Text = "Selecciona el archivo a convertir...";
            txtArchivoOrigen.ForeColor = Color.Gray;
        }

        private void btnSeleccionarArchivo_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtArchivoOrigen.Text = openFileDialog.FileName;
                txtArchivoOrigen.ForeColor = Color.Black;

                // Auto-configurar carpeta destino si está vacía
                if (txtCarpetaDestino.Text == "Selecciona carpeta de destino..." ||
                    txtCarpetaDestino.ForeColor == Color.Gray)
                {
                    string directorioArchivo = Path.GetDirectoryName(openFileDialog.FileName);
                    txtCarpetaDestino.Text = directorioArchivo;
                    txtCarpetaDestino.ForeColor = Color.Black;
                }
            }
        }

        private void btnSeleccionarCarpeta_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtCarpetaDestino.Text = folderBrowserDialog.SelectedPath;
                txtCarpetaDestino.ForeColor = Color.Black;
            }
        }

        private async void btnConvertir_Click(object sender, EventArgs e)
        {
            if (convirtiendo)
            {
                // Cancelar conversión
                if (procesoConversion != null && !procesoConversion.HasExited)
                {
                    procesoConversion.Kill();
                }
                ResetearFormulario();
                return;
            }

            // Validaciones
            if (!ValidarFormulario())
                return;

            try
            {
                convirtiendo = true;
                btnConvertir.Text = "Cancelar";
                btnConvertir.BackColor = Color.FromArgb(217, 50, 50);

                await IniciarConversion();
            }
            catch (Exception ex)
            {
                AgregarLog($"Error: {ex.Message}");
                MessageBox.Show($"Error durante la conversión: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ResetearFormulario();
            }
        }

        private bool ValidarFormulario()
        {
            if (string.IsNullOrWhiteSpace(txtArchivoOrigen.Text) ||
                txtArchivoOrigen.Text == "Selecciona el archivo a convertir...")
            {
                MessageBox.Show("Por favor, selecciona un archivo para convertir.", "Archivo requerido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!File.Exists(txtArchivoOrigen.Text))
            {
                MessageBox.Show("El archivo seleccionado no existe.", "Archivo inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCarpetaDestino.Text) ||
                txtCarpetaDestino.Text == "Selecciona carpeta de destino...")
            {
                MessageBox.Show("Por favor, selecciona una carpeta de destino.", "Carpeta requerida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!Directory.Exists(txtCarpetaDestino.Text))
            {
                MessageBox.Show("La carpeta de destino no existe.", "Carpeta inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async Task IniciarConversion()
        {
            // Buscar el script en la raíz del proyecto (retroceder desde bin/Debug)
            string baseDir = Application.StartupPath;
            string pythonScript = Path.Combine(baseDir, "..", "..", "..", "convertir_pdf.py");
            pythonScript = Path.GetFullPath(pythonScript); // Normalizar la ruta

            // Verificar si existe el script de Python
            if (!File.Exists(pythonScript))
            {
                MessageBox.Show($"Script de Python no encontrado en:\n{pythonScript}\n\n" +
                    "Por favor, asegúrate de que el archivo convertir_pdf.py esté en la raíz del proyecto.",
                    "Script no encontrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblEstado.Text = "Iniciando conversión...";
            progressBar.Style = ProgressBarStyle.Marquee;
            AgregarLog("Iniciando conversión...");

            // Determinar tipo de conversión
            string tipoConversion = ObtenerTipoConversion();

            string argumentos = $"\"{pythonScript}\" \"{txtArchivoOrigen.Text}\" \"{txtCarpetaDestino.Text}\" {tipoConversion}";

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

            procesoConversion = new Process { StartInfo = startInfo };

            // Configurar eventos para capturar salida
            procesoConversion.OutputDataReceived += (s, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    this.Invoke(new Action(() => {
                        AgregarLog(e.Data);
                        ActualizarProgreso(e.Data);
                    }));
                }
            };

            procesoConversion.ErrorDataReceived += (s, e) => {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    this.Invoke(new Action(() => AgregarLog($"ERROR: {e.Data}")));
                }
            };

            // Iniciar proceso
            procesoConversion.Start();
            procesoConversion.BeginOutputReadLine();
            procesoConversion.BeginErrorReadLine();

            // Esperar a que termine
            await Task.Run(() => procesoConversion.WaitForExit());

            // Verificar resultado
            if (procesoConversion.ExitCode == 0)
            {
                lblEstado.Text = "Conversión completada exitosamente";
                progressBar.Value = 100;
                AgregarLog("¡Conversión completada!");

                // Preguntar si quiere abrir la carpeta de destino
                var result = MessageBox.Show("¡Conversión completada exitosamente!\n\n¿Deseas abrir la carpeta de destino?",
                    "Éxito", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    Process.Start("explorer.exe", txtCarpetaDestino.Text);
                }
            }
            else
            {
                lblEstado.Text = "Error en la conversión";
                AgregarLog($"Proceso terminó con código de error: {procesoConversion.ExitCode}");
            }
        }

        private string ObtenerTipoConversion()
        {
            if (rbWordaPDF.Checked) return "word_to_pdf";
            if (rbExcelaPDF.Checked) return "excel_to_pdf";
            if (rbImagenaPDF.Checked) return "image_to_pdf";
            if (rbPDFaWord.Checked) return "pdf_to_word";
            return "word_to_pdf"; // Por defecto
        }

        private void ActualizarProgreso(string salida)
        {
            // Buscar indicadores de progreso en la salida
            if (salida.Contains("Procesando") || salida.Contains("Convirtiendo"))
            {
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Value = 50; // Progreso intermedio
                lblEstado.Text = salida;
            }
            else if (salida.Contains("Completado") || salida.Contains("Guardado"))
            {
                progressBar.Value = 100;
                lblEstado.Text = "Conversión completada";
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
            convirtiendo = false;
            btnConvertir.Text = "Convertir";
            btnConvertir.BackColor = Color.FromArgb(50, 217, 55);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.Value = 0;
            lblEstado.Text = "Listo";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtArchivoOrigen.Text = "Selecciona el archivo a convertir...";
            txtArchivoOrigen.ForeColor = Color.Gray;

            txtCarpetaDestino.Text = "Selecciona carpeta de destino...";
            txtCarpetaDestino.ForeColor = Color.Gray;

            txtLog.Clear();
            progressBar.Value = 0;
            lblEstado.Text = "Listo";

            rbWordaPDF.Checked = true; // Volver a la opción por defecto
            ActualizarFiltroArchivos();
        }
    }
}