using System;
using System.Windows.Forms;
using Datos;

namespace Vista.Factutras
{
    public partial class FormAgregarArticulo : Form
    {
        public ItemPedido NuevoItem { get; private set; }

        public FormAgregarArticulo()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                CrearNuevoItem();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtArticulo.Text))
            {
                MessageBox.Show(
                    "Debe ingresar la descripción del artículo",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtArticulo.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show(
                    "Debe ingresar un precio válido",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtPrecio.Focus();
                return false;
            }

            if (precio <= 0)
            {
                MessageBox.Show(
                    "El precio debe ser mayor a 0",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                txtPrecio.Focus();
                return false;
            }

            return true;
        }

        private void CrearNuevoItem()
        {
            NuevoItem = new ItemPedido
            {
                Link = txtLink.Text.Trim(),
                Articulo = txtArticulo.Text.Trim(),
                Precio = decimal.Parse(txtPrecio.Text),
                Seleccionado = true
            };
        }
    }
}