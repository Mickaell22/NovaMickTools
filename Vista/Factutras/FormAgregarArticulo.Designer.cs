namespace Vista.Factutras
{
    partial class FormAgregarArticulo
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
            this.lblLink = new System.Windows.Forms.Label();
            this.txtLink = new System.Windows.Forms.TextBox();
            this.lblArticulo = new System.Windows.Forms.Label();
            this.txtArticulo = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLink
            // 
            this.lblLink.AutoSize = true;
            this.lblLink.Location = new System.Drawing.Point(16, 20);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(35, 16);
            this.lblLink.TabIndex = 0;
            this.lblLink.Text = "Link:";
            // 
            // txtLink
            // 
            this.txtLink.Location = new System.Drawing.Point(89, 17);
            this.txtLink.Name = "txtLink";
            this.txtLink.Size = new System.Drawing.Size(350, 22);
            this.txtLink.TabIndex = 1;
            // 
            // lblArticulo
            // 
            this.lblArticulo.AutoSize = true;
            this.lblArticulo.Location = new System.Drawing.Point(16, 50);
            this.lblArticulo.Name = "lblArticulo";
            this.lblArticulo.Size = new System.Drawing.Size(57, 16);
            this.lblArticulo.TabIndex = 2;
            this.lblArticulo.Text = "Artículo:";
            // 
            // txtArticulo
            // 
            this.txtArticulo.Location = new System.Drawing.Point(89, 47);
            this.txtArticulo.Name = "txtArticulo";
            this.txtArticulo.Size = new System.Drawing.Size(350, 22);
            this.txtArticulo.TabIndex = 3;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Location = new System.Drawing.Point(16, 80);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(49, 16);
            this.lblPrecio.TabIndex = 4;
            this.lblPrecio.Text = "Precio:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Location = new System.Drawing.Point(89, 77);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(100, 22);
            this.txtPrecio.TabIndex = 5;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(89, 115);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(95, 28);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(194, 115);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 28);
            this.btnCancelar.TabIndex = 7;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // FormAgregarArticulo
            // 
            this.AcceptButton = this.btnAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(462, 163);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.lblPrecio);
            this.Controls.Add(this.txtArticulo);
            this.Controls.Add(this.lblArticulo);
            this.Controls.Add(this.txtLink);
            this.Controls.Add(this.lblLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAgregarArticulo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Agregar Artículo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLink;
        private System.Windows.Forms.TextBox txtLink;
        private System.Windows.Forms.Label lblArticulo;
        private System.Windows.Forms.TextBox txtArticulo;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
    }
}