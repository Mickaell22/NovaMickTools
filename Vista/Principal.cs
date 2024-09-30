using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vista.VerLinks;

namespace Vista
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
            PersonalizarDisenio();
        }

        public void PersonalizarDisenio()
        {
            panelModulo1.Visible = false;
            panelModulo2.Visible = false;
            panelModulo3.Visible = false;
        }
        private Form activeForm = null;
        private void abirPanelHijo(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelHijo.Controls.Add(childForm);
            panelHijo.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void mostrarSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                ocultarSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
        private void ocultarSubMenu()
        {
            if (panelModulo1.Visible)
            {
                panelModulo1.Visible = false;
            }
            if (panelModulo2.Visible)
            {
                panelModulo2.Visible = false;
            }
            if (panelModulo3.Visible)
            {
                panelModulo3.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(panelModulo1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(panelModulo2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mostrarSubMenu(panelModulo3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            abirPanelHijo(new GenerarWord());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            abirPanelHijo(new VerLink());
        }
    }
}
