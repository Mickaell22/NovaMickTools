using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Vista.VerLinks
{
    public partial class VerLink : Form
    {
        private List<string> todosLosLinks = new List<string>();
        private int indiceLinkActual = 0;
        private const int LINKS_POR_LOTE = 10;
        
        public VerLink()
        {
            InitializeComponent();
        }
        
        private void VerLinks_Load(object sender, EventArgs e)
        {
            // Vincular el evento CheckedChanged para asegurar que solo un checkbox esté seleccionado
            checkBox3.CheckedChanged += OnlyOneCheckBox_CheckedChanged;
            checkBox4.CheckedChanged += OnlyOneCheckBox_CheckedChanged;
            
            // Ocultar los checkboxes de Chrome (1 y 2)
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            
            // Configurar textBox1 para múltiples líneas si no lo está ya
            ConfigurarTextBox();
        }
        
        private void ConfigurarTextBox()
        {
            // Configurar textBox1 como multilinea si no lo está
            textBox1.Multiline = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.AcceptsReturn = true;
            textBox1.WordWrap = true;
            
            // Ajustar altura si es necesario
            if (textBox1.Height < 100)
            {
                textBox1.Height = 120;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Verificar que solo un checkbox esté seleccionado
            if (!VerifySingleCheckboxSelected())
            {
                MessageBox.Show("Por favor, selecciona solo un navegador.");
                return;
            }

            // Obtener los links del textBox1, separados por saltos de línea
            string[] links = textBox1.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Limpiar espacios en blanco alrededor de cada link
            links = links.Select(link => link.Trim()).Where(link => !string.IsNullOrEmpty(link)).ToArray();

            if (links.Length == 0)
            {
                MessageBox.Show("No hay links para abrir.");
                return;
            }

            // Guardar todos los links y resetear el índice
            todosLosLinks = links.ToList();
            indiceLinkActual = 0;

            // Mostrar información sobre los lotes
            int totalLotes = (int)Math.Ceiling((double)todosLosLinks.Count / LINKS_POR_LOTE);
            MessageBox.Show($"Se encontraron {todosLosLinks.Count} links.\nSe abrirán en {totalLotes} lotes de {LINKS_POR_LOTE} links cada uno.\n\nPresiona 'Abrir Siguiente Lote' para continuar.", 
                "Apertura por Lotes", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Abrir el primer lote
            AbrirSiguienteLote();
        }

        // Verifica que solo un checkbox esté seleccionado
        private bool VerifySingleCheckboxSelected()
        {
            int count = 0;
            if (checkBox3.Checked) count++;
            if (checkBox4.Checked) count++;

            return count == 1;
        }

        private void AbrirSiguienteLote()
        {
            if (indiceLinkActual >= todosLosLinks.Count)
            {
                MessageBox.Show("Todos los links han sido abiertos.", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtener el lote actual (máximo 10 links)
            int linksRestantes = todosLosLinks.Count - indiceLinkActual;
            int linksEnEsteLote = Math.Min(LINKS_POR_LOTE, linksRestantes);
            
            List<string> loteActual = todosLosLinks.GetRange(indiceLinkActual, linksEnEsteLote);
            
            // Determinar navegador y argumentos base (solo Brave)
            string browserPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
            string modoNavegador = "";
            
            if (checkBox3.Checked)
            {
                modoNavegador = "normal";
            }
            else if (checkBox4.Checked)
            {
                modoNavegador = "incognito";
            }

            try
            {
                // Construir argumentos para abrir todas las URLs en pestañas de una sola ventana
                string argumentos = "";
                
                if (modoNavegador == "incognito")
                {
                    argumentos = "--incognito --new-window";
                }
                else
                {
                    argumentos = "--new-window";
                }
                
                // Agregar todas las URLs del lote como argumentos separados
                argumentos += " " + string.Join(" ", loteActual);

                // Abrir el navegador con todas las URLs del lote en una sola ventana
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = browserPath,
                    Arguments = argumentos,
                    UseShellExecute = true
                });

                // Actualizar el índice
                indiceLinkActual += linksEnEsteLote;

                // Mostrar información del progreso
                int loteNumero = (indiceLinkActual / LINKS_POR_LOTE);
                int totalLotes = (int)Math.Ceiling((double)todosLosLinks.Count / LINKS_POR_LOTE);
                
                if (indiceLinkActual < todosLosLinks.Count)
                {
                    int linksRestantesTotal = todosLosLinks.Count - indiceLinkActual;
                    DialogResult resultado = MessageBox.Show(
                        $"Lote {loteNumero} completado. Se abrieron {linksEnEsteLote} links en pestañas.\n\n" +
                        $"Links restantes: {linksRestantesTotal}\n" +
                        $"¿Deseas abrir el siguiente lote?",
                        "Continuar con siguiente lote",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        AbrirSiguienteLote();
                    }
                }
                else
                {
                    MessageBox.Show($"¡Completado! Se abrieron todos los {todosLosLinks.Count} links en {totalLotes} lotes con pestañas.", 
                        "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir los links: " + ex.Message);
            }
        }

        // Método para abrir los links en el navegador adecuado (solo Brave)
        private void OpenLinkInBrowser(string link)
        {
            string browserPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
            string arguments = string.Empty;

            // Verificar qué checkbox está seleccionado y definir los argumentos
            if (checkBox3.Checked)
            {
                // Brave normal
                arguments = link;
            }
            else if (checkBox4.Checked)
            {
                // Brave en modo incognito
                arguments = $"--incognito {link}";
            }

            // Ejecutar el navegador con el link
            if (!string.IsNullOrEmpty(arguments))
            {
                Process.Start(browserPath, arguments);
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un modo de Brave.");
            }
        }
        // Función para desmarcar otros checkboxes cuando uno es seleccionado
        private void OnlyOneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                foreach (var control in panel7.Controls)
                {
                    if (control is CheckBox checkBox && checkBox != sender)
                    {
                        checkBox.Checked = false;
                    }
                }
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Funcionalidad simplificada para Ctrl+V automático
            // Ya no agregamos espacios automáticamente para evitar interferencias
        }
    }
}
