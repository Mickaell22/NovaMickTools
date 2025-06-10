using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using Datos;

namespace Vista.Factutras
{
    public partial class FormPedidosTemu : Form
    {
        private List<Pedido> pedidos = new List<Pedido>();
        private bool mostrandoResumen = false;

        public FormPedidosTemu()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            ConfigurarDataGridViewPedidos();

            btnNuevoPedido.Click += BtnNuevoPedido_Click;
            btnAgregarArticulo.Click += BtnAgregarArticulo_Click;
            btnVolver.Click += BtnVolver_Click;

            // Agregar botón para ver resumen
            Button btnVerResumen = new Button
            {
                Text = "Ver Resumen del Día",
                Location = new System.Drawing.Point(520, 15),
                Size = new System.Drawing.Size(160, 28)
            };
            btnVerResumen.Click += BtnVerResumen_Click;
            panel1.Controls.Add(btnVerResumen);
        }

        private void ConfigurarDataGridViewPedidos()
        {
            dgvPedidos.AutoGenerateColumns = false;
            dgvPedidos.Columns.Clear();

            if (!mostrandoResumen)
            {
                dgvPedidos.Columns.AddRange(new DataGridViewColumn[]
                {
                    new DataGridViewTextBoxColumn
                    {
                        Name = "Numero",
                        HeaderText = "Num.",
                        DataPropertyName = "Numero",
                        Width = 50
                    },
                    new DataGridViewLinkColumn
                    {
                        Name = "Link",
                        HeaderText = "Link",
                        DataPropertyName = "Link",
                        Width = 200
                    },
                    new DataGridViewTextBoxColumn
                    {
                        Name = "Articulo",
                        HeaderText = "Artículo",
                        DataPropertyName = "Articulo",
                        Width = 200
                    },
                    new DataGridViewCheckBoxColumn
                    {
                        Name = "Seleccionado",
                        HeaderText = "Estado",
                        DataPropertyName = "Seleccionado",
                        Width = 60
                    },
                    new DataGridViewTextBoxColumn
                    {
                        Name = "Precio",
                        HeaderText = "Precio",
                        DataPropertyName = "Precio",
                        DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" },
                        Width = 80
                    }
                });
            }
            else
            {
                dgvPedidos.Columns.AddRange(new DataGridViewColumn[]
                {
                    new DataGridViewTextBoxColumn
                    {
                        Name = "Numero",
                        HeaderText = "Num.",
                        DataPropertyName = "Numero",
                        Width = 50
                    },
                    new DataGridViewTextBoxColumn
                    {
                        Name = "Cliente",
                        HeaderText = "Cliente",
                        DataPropertyName = "Cliente",
                        Width = 100
                    },
                    new DataGridViewTextBoxColumn
                    {
                        Name = "CantidadArticulos",
                        HeaderText = "Artículos",
                        DataPropertyName = "CantidadArticulos",
                        Width = 80
                    },
                    new DataGridViewCheckBoxColumn
                    {
                        Name = "Estado",
                        HeaderText = "Estado",
                        DataPropertyName = "Estado",
                        Width = 60
                    },
                    new DataGridViewTextBoxColumn
                    {
                        Name = "Precio",
                        HeaderText = "Precio",
                        DataPropertyName = "Precio",
                        DefaultCellStyle = new DataGridViewCellStyle { Format = "N2" },
                        Width = 80
                    }
                });
            }
        }

        private void BtnNuevoPedido_Click(object sender, EventArgs e)
        {
            using (var formNuevo = new FormNuevoPedido())
            {
                if (formNuevo.ShowDialog() == DialogResult.OK)
                {
                    pedidos.Add(formNuevo.NuevoPedido);
                    mostrandoResumen = false;
                    ConfigurarDataGridViewPedidos();
                    MostrarPedidoActual(formNuevo.NuevoPedido);
                }
            }
        }

        private void BtnAgregarArticulo_Click(object sender, EventArgs e)
        {
            if (pedidos.Count == 0)
            {
                MessageBox.Show("Primero cree un nuevo pedido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var formArticulo = new FormAgregarArticulo())
            {
                if (formArticulo.ShowDialog() == DialogResult.OK)
                {
                    var pedidoActual = pedidos.Last();
                    formArticulo.NuevoItem.Numero = pedidoActual.Items.Count + 1;
                    pedidoActual.Items.Add(formArticulo.NuevoItem);
                    MostrarPedidoActual(pedidoActual);
                }
            }
        }

        private void BtnVerResumen_Click(object sender, EventArgs e)
        {
            mostrandoResumen = true;
            ConfigurarDataGridViewPedidos();
            MostrarResumenPedidos();
        }

        private void MostrarPedidoActual(Pedido pedido)
        {
            this.Text = $"Pedido de {pedido.NombreCliente}";
            dgvPedidos.DataSource = null;
            dgvPedidos.DataSource = pedido.Items;

            ActualizarTotales(pedido);
        }

        private void MostrarResumenPedidos()
        {
            var resumen = pedidos.Select(p => new
            {
                Numero = p.Items.Count > 0 ? p.Items[0].Numero : 0,
                Cliente = p.NombreCliente,
                CantidadArticulos = p.Items.Count(i => i.Seleccionado),
                Estado = true,
                Precio = p.Subtotal
            }).ToList();

            dgvPedidos.DataSource = resumen;

            decimal subtotalGeneral = pedidos.Sum(p => p.Subtotal);
            decimal comisionGeneral = pedidos.Sum(p => p.Items.Count(i => i.Seleccionado) * 0.50M);

            lblSubtotal.Text = $"Sub total: {subtotalGeneral:N2}";
            lblComision.Text = $"Comisión: {comisionGeneral:N2}";
            lblTotal.Text = $"Total: {(subtotalGeneral + comisionGeneral):N2}";
        }

        private void ActualizarTotales(Pedido pedido)
        {
            decimal subtotal = pedido.Subtotal;
            decimal comision = pedido.Items.Count(i => i.Seleccionado) * 0.50M;
            decimal total = subtotal + comision;

            lblSubtotal.Text = $"Sub total: {subtotal:N2}";
            lblComision.Text = $"Comisión: {comision:N2}";
            lblTotal.Text = $"Total: {total:N2}";
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            if (mostrandoResumen)
            {
                mostrandoResumen = false;
                ConfigurarDataGridViewPedidos();
                if (pedidos.Any())
                    MostrarPedidoActual(pedidos.Last());
            }
            else
            {
                this.Close();
            }
        }
    }
}