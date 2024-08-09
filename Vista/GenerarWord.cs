using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class GenerarWord : Form
    {
        private string[] asignaturas = new string[10];
        private string[] paralelos = new string[10];
        private string[] docentes = new string[10];
        public GenerarWord()
        {
                InitializeComponent();

                // Vincular el evento Load del formulario
                this.Load += new EventHandler(GenerarWord_Load);

                // Vincular el evento CheckedChanged del checkbox
                this.checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);

                

        }

        private void GenerarWord_Load(object sender, EventArgs e)
        {
            // Ocultar panel2 al iniciar
            panel2.Visible = false;

            // Centrar panel3 al iniciar
            CenterPanel(panel3);
        }
        private void CenterPanel(Panel panel)
        {
            panel.Left = (panel.Parent.ClientSize.Width - panel.Width) / 2;
            panel.Top = (panel.Parent.ClientSize.Height - panel.Height) / 2;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                panel2.Visible = true;
                panel2.Location = new Point(panel3.Right + 10, panel3.Top); // Ajusta la posición del panel2 al lado derecho de panel3
            }
            else
            {
                panel2.Visible = false;
            }
        }
    }
}
