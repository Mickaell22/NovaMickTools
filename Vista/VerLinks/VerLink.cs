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
        public VerLink()
        {
            InitializeComponent();
        }
        
        private void VerLinks_Load(object sender, EventArgs e)
        {
            // Vincular el evento CheckedChanged para asegurar que solo un checkbox esté seleccionado
            checkBox1.CheckedChanged += OnlyOneCheckBox_CheckedChanged;
            checkBox2.CheckedChanged += OnlyOneCheckBox_CheckedChanged;
            checkBox3.CheckedChanged += OnlyOneCheckBox_CheckedChanged;
            checkBox4.CheckedChanged += OnlyOneCheckBox_CheckedChanged;
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

            // Determinar qué navegador está seleccionado
            string browserPath = "";
            string argsBase = "";

            if (checkBox1.Checked)
            {
                browserPath = @"C:\Users\ASUS\AppData\Local\Programs\Opera GX\launcher.exe";
                argsBase = "--new-window"; // Opera nueva ventana
            }
            else if (checkBox2.Checked)
            {
                browserPath = @"C:\Users\ASUS\AppData\Local\Programs\Opera GX\launcher.exe";
                argsBase = "--private --new-window"; // Opera incognito y nueva ventana
            }
            else if (checkBox3.Checked)
            {
                browserPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
                argsBase = "--new-window"; // Brave nueva ventana
            }
            else if (checkBox4.Checked)
            {
                browserPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
                argsBase = "--incognito --new-window"; // Brave incognito y nueva ventana
            }

            try
            {
                // Construir un solo argumento con todos los links
                string allLinks = string.Join(" ", links);

                // Abrir el navegador con todos los links en una nueva ventana/incógnito
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = browserPath,
                    Arguments = $"{argsBase} {allLinks}",
                    UseShellExecute = true  // Asegura que el proceso se ejecute correctamente
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir los links: " + ex.Message);
            }
        }

        // Verifica que solo un checkbox esté seleccionado
        private bool VerifySingleCheckboxSelected()
        {
            int count = 0;
            if (checkBox1.Checked) count++;
            if (checkBox2.Checked) count++;
            if (checkBox3.Checked) count++;
            if (checkBox4.Checked) count++;

            return count == 1;
        }
        // Método para abrir los links en el navegador adecuado
        private void OpenLinkInBrowser(string link)
        {
            string browserPath = string.Empty;
            string arguments = string.Empty;

            // Verificar qué checkbox está seleccionado y definir el navegador y los argumentos
            if (checkBox1.Checked)
            {
                // Opera normal
                browserPath = @"C:\Program Files\Opera\launcher.exe";
                arguments = link;
            }
            else if (checkBox2.Checked)
            {
                // Opera en modo incognito
                browserPath = @"C:\Program Files\Opera\launcher.exe";
                arguments = $"--private {link}";
            }
            else if (checkBox3.Checked)
            {
                // Brave normal
                browserPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
                arguments = link;
            }
            else if (checkBox4.Checked)
            {
                // Brave en modo incognito
                browserPath = @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe";
                arguments = $"--incognito {link}";
            }

            // Ejecutar el navegador con el link
            if (!string.IsNullOrEmpty(browserPath))
            {
                Process.Start(browserPath, arguments);
            }
            else
            {
                MessageBox.Show("Navegador no encontrado.");
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
            // Guardar el cursor actual
            int selectionStart = textBox1.SelectionStart;

            // Obtener todas las líneas del TextBox
            string[] lines = textBox1.Lines;

            // Asegurarse de que cada línea termine con un espacio
            for (int i = 0; i < lines.Length; i++)
            {
                if (!lines[i].EndsWith(" "))
                {
                    lines[i] += " "; // Añadir un espacio al final de la línea si no lo tiene
                }
            }

            // Asignar las líneas de nuevo al TextBox
            textBox1.Lines = lines;

            // Restaurar la posición del cursor
            textBox1.SelectionStart = selectionStart;
            textBox1.SelectionLength = 0; // Desmarcar cualquier selección
        }
    }
}
