namespace Vista.Factutras
{
    partial class FormPedidosTemu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTotales = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblComision = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lblFecha = new System.Windows.Forms.Label();
            this.btnVolver = new System.Windows.Forms.Button();
            this.btnNuevoPedido = new System.Windows.Forms.Button();
            this.btnAgregarArticulo = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panelTotales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelTotales);
            this.panel1.Controls.Add(this.dgvPedidos);
            this.panel1.Controls.Add(this.dtpFecha);
            this.panel1.Controls.Add(this.lblFecha);
            this.panel1.Controls.Add(this.btnVolver);
            this.panel1.Controls.Add(this.btnNuevoPedido);
            this.panel1.Controls.Add(this.btnAgregarArticulo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1145, 430);
            this.panel1.TabIndex = 0;
            // 
            // panelTotales
            // 
            this.panelTotales.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTotales.Controls.Add(this.lblTotal);
            this.panelTotales.Controls.Add(this.lblComision);
            this.panelTotales.Controls.Add(this.lblSubtotal);
            this.panelTotales.Location = new System.Drawing.Point(1662, 1311);
            this.panelTotales.Margin = new System.Windows.Forms.Padding(4);
            this.panelTotales.Name = "panelTotales";
            this.panelTotales.Size = new System.Drawing.Size(267, 64);
            this.panelTotales.TabIndex = 6;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(4, 39);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(75, 16);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "Total: $0.00";
            // 
            // lblComision
            // 
            this.lblComision.AutoSize = true;
            this.lblComision.Location = new System.Drawing.Point(4, 23);
            this.lblComision.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComision.Name = "lblComision";
            this.lblComision.Size = new System.Drawing.Size(100, 16);
            this.lblComision.TabIndex = 1;
            this.lblComision.Text = "Comisión: $0.50";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(4, 7);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(93, 16);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "Subtotal: $0.00";
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.AllowUserToAddRows = false;
            this.dgvPedidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Location = new System.Drawing.Point(19, 85);
            this.dgvPedidos.Margin = new System.Windows.Forms.Padding(4);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.RowHeadersWidth = 51;
            this.dgvPedidos.Size = new System.Drawing.Size(1061, 291);
            this.dgvPedidos.TabIndex = 0;
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(80, 55);
            this.dtpFecha.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(132, 22);
            this.dtpFecha.TabIndex = 1;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Location = new System.Drawing.Point(16, 59);
            this.lblFecha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(48, 16);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha:";
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(16, 15);
            this.btnVolver.Margin = new System.Windows.Forms.Padding(4);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(160, 28);
            this.btnVolver.TabIndex = 3;
            this.btnVolver.Text = "Volver al Menú";
            this.btnVolver.UseVisualStyleBackColor = true;
            // 
            // btnNuevoPedido
            // 
            this.btnNuevoPedido.Location = new System.Drawing.Point(184, 15);
            this.btnNuevoPedido.Margin = new System.Windows.Forms.Padding(4);
            this.btnNuevoPedido.Name = "btnNuevoPedido";
            this.btnNuevoPedido.Size = new System.Drawing.Size(160, 28);
            this.btnNuevoPedido.TabIndex = 4;
            this.btnNuevoPedido.Text = "Nuevo Pedido";
            this.btnNuevoPedido.UseVisualStyleBackColor = true;
            // 
            // btnAgregarArticulo
            // 
            this.btnAgregarArticulo.Location = new System.Drawing.Point(352, 15);
            this.btnAgregarArticulo.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregarArticulo.Name = "btnAgregarArticulo";
            this.btnAgregarArticulo.Size = new System.Drawing.Size(160, 28);
            this.btnAgregarArticulo.TabIndex = 5;
            this.btnAgregarArticulo.Text = "Agregar Artículo";
            this.btnAgregarArticulo.UseVisualStyleBackColor = true;
            // 
            // FormPedidosTemu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1145, 430);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormPedidosTemu";
            this.Text = "Gestión de Pedidos Temu";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelTotales.ResumeLayout(false);
            this.panelTotales.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.Button btnNuevoPedido;
        private System.Windows.Forms.Button btnAgregarArticulo;
        private System.Windows.Forms.Panel panelTotales;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblComision;
        private System.Windows.Forms.Label lblSubtotal;
    }
}