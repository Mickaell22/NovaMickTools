using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Vista
{
    public partial class GenerarWord : Form
    {
        private string[] asignaturas = new string[10];
        private string[] paralelos = new string[10];
        private string[] docentes = new string[10];
        private string[] ruta = new string[10];
        private string baseFolderPath = "";
        public GenerarWord()
        {
            InitializeComponent();
            cargarInformacion();
            // Vincular el evento Load del formulario
            this.Load += new EventHandler(GenerarWord_Load);

            // Vincular el evento CheckedChanged del checkbox
            this.checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
            informacionIzquierda();
            setInformacion();
        }
        public void setInformacion()
        {
            //Set combox asignatur
            CargarComboxAsignatura();

            //Set ruta base
            CargarRutaBase();
            CargarRuta();

            #region Asignatura
            txtBoxA_1.Text = asignaturas[0];
            txtBoxA_2.Text = asignaturas[1];
            txtBoxA_3.Text = asignaturas[2];
            txtBoxA_4.Text = asignaturas[3];
            txtBoxA_5.Text = asignaturas[4];
            txtBoxA_6.Text = asignaturas[5];
            txtBoxA_7.Text = asignaturas[6];
            #endregion

            #region Docente
            txtBoxD_1.Text = docentes[0];
            txtBoxD_2.Text = docentes[1];
            txtBoxD_3.Text = docentes[2];
            txtBoxD_4.Text = docentes[3];
            txtBoxD_5.Text = docentes[4];
            txtBoxD_6.Text = docentes[5];
            txtBoxD_7.Text = docentes[6];
            #endregion

            #region Paralelo
            txtBoxP_1.Text = paralelos[0];
            txtBoxP_2.Text = paralelos[1];
            txtBoxP_3.Text = paralelos[2];
            txtBoxP_4.Text = paralelos[3];
            txtBoxP_5.Text = paralelos[4];
            txtBoxP_6.Text = paralelos[5];
            txtBoxP_7.Text = paralelos[6];
            #endregion
        }
        public void CargarRutaBase()
        {
            if (File.Exists("rutaBase.txt"))
                ruta = File.ReadAllLines("rutaBase.txt");
        }
        public void cargarInformacion()
        {
            if (File.Exists("asignaturas.txt"))
                asignaturas = File.ReadAllLines("asignaturas.txt");

            if (File.Exists("paralelos.txt"))
                paralelos = File.ReadAllLines("paralelos.txt");

            if (File.Exists("docentes.txt"))
                docentes = File.ReadAllLines("docentes.txt");
        }
        private void GuardarDatos()
        {
            // Guardar valores desde los TextBox a los arreglos
            #region Asignatura
            asignaturas[0] = txtBoxA_1.Text;
            asignaturas[1] = txtBoxA_2.Text;
            asignaturas[2] = txtBoxA_3.Text;
            asignaturas[3] = txtBoxA_4.Text;
            asignaturas[4] = txtBoxA_5.Text;
            asignaturas[5] = txtBoxA_6.Text;
            asignaturas[6] = txtBoxA_7.Text;
            #endregion

            #region Docente
            docentes[0] = txtBoxD_1.Text;
            docentes[1] = txtBoxD_2.Text;
            docentes[2] = txtBoxD_3.Text;
            docentes[3] = txtBoxD_4.Text;
            docentes[4] = txtBoxD_5.Text;
            docentes[5] = txtBoxD_6.Text;
            docentes[6] = txtBoxD_7.Text;
            #endregion

            #region Paralelo
            paralelos[0] = txtBoxP_1.Text;
            paralelos[1] = txtBoxP_2.Text;
            paralelos[2] = txtBoxP_3.Text;
            paralelos[3] = txtBoxP_4.Text;
            paralelos[4] = txtBoxP_5.Text;
            paralelos[5] = txtBoxP_6.Text;
            paralelos[6] = txtBoxP_7.Text;
            #endregion

            // Escribir los arreglos en archivos de texto
            File.WriteAllLines("asignaturas.txt", asignaturas);
            File.WriteAllLines("paralelos.txt", paralelos);
            File.WriteAllLines("docentes.txt", docentes);
        }
        public void informacionIzquierda()
        {
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel8.Dock = System.Windows.Forms.DockStyle.Left;

            // Establece el ancho de los paneles para que cada uno ocupe un 33% del panel4
            this.panel6.Width = this.panel4.Width / 3;
            this.panel7.Width = this.panel4.Width / 3;
            this.panel8.Width = this.panel4.Width / 3;

            // Opcionalmente, puedes agregar eventos para manejar cambios en el tamaño de la ventana
            this.panel4.SizeChanged += new System.EventHandler(this.Panel4_SizeChanged);
        }
        private void Panel4_SizeChanged(object sender, EventArgs e)
        {
            // Reajusta el ancho de cada panel cuando panel4 cambie de tamaño
            this.panel6.Width = this.panel4.Width / 3;
            this.panel7.Width = this.panel4.Width / 3;
            this.panel8.Width = this.panel4.Width / 3;
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

        private void button2_Click(object sender, EventArgs e)
        {
            //Editar informacion
            if (txtBoxP_1.Enabled)
            {
                //Guardamos


                btnEditarGuardar.Text = "Editar informacion";

                GuardarDatos();
                MessageBox.Show("Informacion guardada");
                HabilitarTodos(false);
            }
            else
            {
                //Habilitamos la opcion de guardar informacion

                btnEditarGuardar.Text = "Guardar cambios";
                MessageBox.Show("Vas a editar la informacion");
                HabilitarTodos(true);
            }
            //Guardar
        }
        public void HabilitarTodos(bool x)
        {
            #region Asignatura
            txtBoxA_1.Enabled = x;
            txtBoxA_2.Enabled = x;
            txtBoxA_3.Enabled = x;
            txtBoxA_4.Enabled = x;
            txtBoxA_5.Enabled = x;
            txtBoxA_6.Enabled = x;
            txtBoxA_7.Enabled = x;
            #endregion

            #region Docente
            txtBoxD_1.Enabled = x;
            txtBoxD_2.Enabled = x;
            txtBoxD_3.Enabled = x;
            txtBoxD_4.Enabled = x;
            txtBoxD_5.Enabled = x;
            txtBoxD_6.Enabled = x;
            txtBoxD_7.Enabled = x;
            #endregion

            #region Paralelo
            txtBoxP_1.Enabled = x;
            txtBoxP_2.Enabled = x;
            txtBoxP_3.Enabled = x;
            txtBoxP_4.Enabled = x;
            txtBoxP_5.Enabled = x;
            txtBoxP_6.Enabled = x;
            txtBoxP_7.Enabled = x;
            #endregion

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = cmbAsignatura.SelectedIndex;

            // Verificar si el índice seleccionado es válido
            if (selectedIndex >= 0 && selectedIndex < docentes.Length && selectedIndex < paralelos.Length)
            {
                txtDocente.Text = docentes[selectedIndex];
                txtParalelo.Text = paralelos[selectedIndex];
            }
        }
        public void CargarComboxAsignatura()
        {
            for (int i = 0;i<asignaturas.Length;i++)
            {
                if (asignaturas[i] != "")
                {
                    cmbAsignatura.Items.Add(asignaturas[i]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    baseFolderPath = folderDialog.SelectedPath;
                    ruta[0] = baseFolderPath;
                }
                File.WriteAllLines("rutaBase.txt", ruta) ;
                CargarRuta();
            }
        }
        public void CargarRuta()
        {
            txtRuta.Text = ruta[0];
        }

    }
}
