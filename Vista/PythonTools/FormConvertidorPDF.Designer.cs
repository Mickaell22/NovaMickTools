namespace Vista.PythonTools
{
    partial class FormConvertidorPDF
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpTipoConversion = new System.Windows.Forms.GroupBox();
            this.rbPDFaWord = new System.Windows.Forms.RadioButton();
            this.rbWordaPDF = new System.Windows.Forms.RadioButton();
            this.rbExcelaPDF = new System.Windows.Forms.RadioButton();
            this.rbImagenaPDF = new System.Windows.Forms.RadioButton();
            this.lblArchivoOrigen = new System.Windows.Forms.Label();
            this.txtArchivoOrigen = new System.Windows.Forms.TextBox();
            this.btnSeleccionarArchivo = new System.Windows.Forms.Button();
            this.lblCarpetaDestino = new System.Windows.Forms.Label();
            this.txtCarpetaDestino = new System.Windows.Forms.TextBox();
            this.btnSeleccionarCarpeta = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnConvertir = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpTipoConversion.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(217)))), ((int)(((byte)(55)))));
            this.panel1.Controls.Add(this.lblTitulo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 60);
            this.panel1.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(290, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(220, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Convertidor PDF";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSeleccionarCarpeta);
            this.panel2.Controls.Add(this.txtCarpetaDestino);
            this.panel2.Controls.Add(this.lblCarpetaDestino);
            this.panel2.Controls.Add(this.btnSeleccionarArchivo);
            this.panel2.Controls.Add(this.txtArchivoOrigen);
            this.panel2.Controls.Add(this.lblArchivoOrigen);
            this.panel2.Controls.Add(this.grpTipoConversion);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(20);
            this.panel2.Size = new System.Drawing.Size(800, 220);
            this.panel2.TabIndex = 1;
            // 
            // grpTipoConversion
            // 
            this.grpTipoConversion.Controls.Add(this.rbImagenaPDF);
            this.grpTipoConversion.Controls.Add(this.rbExcelaPDF);
            this.grpTipoConversion.Controls.Add(this.rbWordaPDF);
            this.grpTipoConversion.Controls.Add(this.rbPDFaWord);
            this.grpTipoConversion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.grpTipoConversion.Location = new System.Drawing.Point(23, 23);
            this.grpTipoConversion.Name = "grpTipoConversion";
            this.grpTipoConversion.Size = new System.Drawing.Size(754, 80);
            this.grpTipoConversion.TabIndex = 0;
            this.grpTipoConversion.TabStop = false;
            this.grpTipoConversion.Text = "Tipo de Conversión";
            // 
            // rbPDFaWord
            // 
            this.rbPDFaWord.AutoSize = true;
            this.rbPDFaWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rbPDFaWord.Location = new System.Drawing.Point(20, 30);
            this.rbPDFaWord.Name = "rbPDFaWord";
            this.rbPDFaWord.Size = new System.Drawing.Size(126, 22);
            this.rbPDFaWord.TabIndex = 0;
            this.rbPDFaWord.Text = "PDF → Word";
            this.rbPDFaWord.UseVisualStyleBackColor = true;
            this.rbPDFaWord.CheckedChanged += new System.EventHandler(this.TipoConversion_CheckedChanged);
            // 
            // rbWordaPDF
            // 
            this.rbWordaPDF.AutoSize = true;
            this.rbWordaPDF.Checked = true;
            this.rbWordaPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rbWordaPDF.Location = new System.Drawing.Point(170, 30);
            this.rbWordaPDF.Name = "rbWordaPDF";
            this.rbWordaPDF.Size = new System.Drawing.Size(126, 22);
            this.rbWordaPDF.TabIndex = 1;
            this.rbWordaPDF.TabStop = true;
            this.rbWordaPDF.Text = "Word → PDF";
            this.rbWordaPDF.UseVisualStyleBackColor = true;
            this.rbWordaPDF.CheckedChanged += new System.EventHandler(this.TipoConversion_CheckedChanged);
            // 
            // rbExcelaPDF
            // 
            this.rbExcelaPDF.AutoSize = true;
            this.rbExcelaPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rbExcelaPDF.Location = new System.Drawing.Point(320, 30);
            this.rbExcelaPDF.Name = "rbExcelaPDF";
            this.rbExcelaPDF.Size = new System.Drawing.Size(125, 22);
            this.rbExcelaPDF.TabIndex = 2;
            this.rbExcelaPDF.Text = "Excel → PDF";
            this.rbExcelaPDF.UseVisualStyleBackColor = true;
            this.rbExcelaPDF.CheckedChanged += new System.EventHandler(this.TipoConversion_CheckedChanged);
            // 
            // rbImagenaPDF
            // 
            this.rbImagenaPDF.AutoSize = true;
            this.rbImagenaPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rbImagenaPDF.Location = new System.Drawing.Point(470, 30);
            this.rbImagenaPDF.Name = "rbImagenaPDF";
            this.rbImagenaPDF.Size = new System.Drawing.Size(144, 22);
            this.rbImagenaPDF.TabIndex = 3;
            this.rbImagenaPDF.Text = "Imagen → PDF";
            this.rbImagenaPDF.UseVisualStyleBackColor = true;
            this.rbImagenaPDF.CheckedChanged += new System.EventHandler(this.TipoConversion_CheckedChanged);
            // 
            // lblArchivoOrigen
            // 
            this.lblArchivoOrigen.AutoSize = true;
            this.lblArchivoOrigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblArchivoOrigen.Location = new System.Drawing.Point(23, 120);
            this.lblArchivoOrigen.Name = "lblArchivoOrigen";
            this.lblArchivoOrigen.Size = new System.Drawing.Size(126, 20);
            this.lblArchivoOrigen.TabIndex = 1;
            this.lblArchivoOrigen.Text = "Archivo Origen:";
            // 
            // txtArchivoOrigen
            // 
            this.txtArchivoOrigen.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtArchivoOrigen.Location = new System.Drawing.Point(160, 118);
            this.txtArchivoOrigen.Name = "txtArchivoOrigen";
            this.txtArchivoOrigen.ReadOnly = true;
            this.txtArchivoOrigen.Size = new System.Drawing.Size(460, 23);
            this.txtArchivoOrigen.TabIndex = 2;
            this.txtArchivoOrigen.Text = "Selecciona el archivo a convertir...";
            this.txtArchivoOrigen.ForeColor = System.Drawing.Color.Gray;
            // 
            // btnSeleccionarArchivo
            // 
            this.btnSeleccionarArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSeleccionarArchivo.Location = new System.Drawing.Point(635, 116);
            this.btnSeleccionarArchivo.Name = "btnSeleccionarArchivo";
            this.btnSeleccionarArchivo.Size = new System.Drawing.Size(142, 27);
            this.btnSeleccionarArchivo.TabIndex = 3;
            this.btnSeleccionarArchivo.Text = "Seleccionar";
            this.btnSeleccionarArchivo.UseVisualStyleBackColor = true;
            this.btnSeleccionarArchivo.Click += new System.EventHandler(this.btnSeleccionarArchivo_Click);
            // 
            // lblCarpetaDestino
            // 
            this.lblCarpetaDestino.AutoSize = true;
            this.lblCarpetaDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblCarpetaDestino.Location = new System.Drawing.Point(23, 160);
            this.lblCarpetaDestino.Name = "lblCarpetaDestino";
            this.lblCarpetaDestino.Size = new System.Drawing.Size(131, 20);
            this.lblCarpetaDestino.TabIndex = 4;
            this.lblCarpetaDestino.Text = "Carpeta Destino:";
            // 
            // txtCarpetaDestino
            // 
            this.txtCarpetaDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCarpetaDestino.Location = new System.Drawing.Point(160, 158);
            this.txtCarpetaDestino.Name = "txtCarpetaDestino";
            this.txtCarpetaDestino.ReadOnly = true;
            this.txtCarpetaDestino.Size = new System.Drawing.Size(460, 23);
            this.txtCarpetaDestino.TabIndex = 5;
            this.txtCarpetaDestino.Text = "Selecciona carpeta de destino...";
            this.txtCarpetaDestino.ForeColor = System.Drawing.Color.Gray;
            // 
            // btnSeleccionarCarpeta
            // 
            this.btnSeleccionarCarpeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSeleccionarCarpeta.Location = new System.Drawing.Point(635, 156);
            this.btnSeleccionarCarpeta.Name = "btnSeleccionarCarpeta";
            this.btnSeleccionarCarpeta.Size = new System.Drawing.Size(142, 27);
            this.btnSeleccionarCarpeta.TabIndex = 6;
            this.btnSeleccionarCarpeta.Text = "Seleccionar";
            this.btnSeleccionarCarpeta.UseVisualStyleBackColor = true;
            this.btnSeleccionarCarpeta.Click += new System.EventHandler(this.btnSeleccionarCarpeta_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnLimpiar);
            this.panel3.Controls.Add(this.btnConvertir);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 280);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(20);
            this.panel3.Size = new System.Drawing.Size(800, 60);
            this.panel3.TabIndex = 2;
            // 
            // btnConvertir
            // 
            this.btnConvertir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(217)))), ((int)(((byte)(55)))));
            this.btnConvertir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConvertir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnConvertir.ForeColor = System.Drawing.Color.White;
            this.btnConvertir.Location = new System.Drawing.Point(300, 15);
            this.btnConvertir.Name = "btnConvertir";
            this.btnConvertir.Size = new System.Drawing.Size(120, 35);
            this.btnConvertir.TabIndex = 0;
            this.btnConvertir.Text = "Convertir";
            this.btnConvertir.UseVisualStyleBackColor = false;
            this.btnConvertir.Click += new System.EventHandler(this.btnConvertir_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.Gray;
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(440, 15);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(120, 35);
            this.btnLimpiar.TabIndex = 1;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtLog);
            this.panel4.Controls.Add(this.lblEstado);
            this.panel4.Controls.Add(this.progressBar);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 340);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(20);
            this.panel4.Size = new System.Drawing.Size(800, 210);
            this.panel4.TabIndex = 3;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(23, 23);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(754, 23);
            this.progressBar.TabIndex = 0;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lblEstado.Location = new System.Drawing.Point(23, 55);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(45, 17);
            this.lblEstado.TabIndex = 1;
            this.lblEstado.Text = "Listo";
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.Location = new System.Drawing.Point(23, 80);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(754, 107);
            this.txtLog.TabIndex = 2;
            // 
            // FormConvertidorPDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormConvertidorPDF";
            this.Text = "Convertidor PDF";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.grpTipoConversion.ResumeLayout(false);
            this.grpTipoConversion.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox grpTipoConversion;
        private System.Windows.Forms.RadioButton rbPDFaWord;
        private System.Windows.Forms.RadioButton rbWordaPDF;
        private System.Windows.Forms.RadioButton rbExcelaPDF;
        private System.Windows.Forms.RadioButton rbImagenaPDF;
        private System.Windows.Forms.Label lblArchivoOrigen;
        private System.Windows.Forms.TextBox txtArchivoOrigen;
        private System.Windows.Forms.Button btnSeleccionarArchivo;
        private System.Windows.Forms.Label lblCarpetaDestino;
        private System.Windows.Forms.TextBox txtCarpetaDestino;
        private System.Windows.Forms.Button btnSeleccionarCarpeta;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnConvertir;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}