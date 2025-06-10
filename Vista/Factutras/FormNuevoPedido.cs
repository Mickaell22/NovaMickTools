using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Datos;

namespace Vista.Factutras
{
    public partial class FormNuevoPedido : Form
    {
        public Pedido NuevoPedido { get; private set; }

        public FormNuevoPedido()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                CrearNuevoPedido();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                MessageBox.Show(
                    "Debe ingresar el nombre del cliente",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtCliente.Focus();
                return false;
            }
            return true;
        }

        private void CrearNuevoPedido()
        {
            NuevoPedido = new Pedido
            {
                NombreCliente = txtCliente.Text.Trim(),
                FechaPedido = DateTime.Now,
                Items = new List<ItemPedido>()
            };
        }
    }
}