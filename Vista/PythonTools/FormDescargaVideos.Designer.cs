namespace Vista.PythonTools
{
    partial class FormDescargaVideos
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
            this.lblUrl = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.lblCarpeta = new System.Windows.Forms.Label();
            this.txtCarpeta = new System.Windows.Forms.TextBox();
            this.btnSeleccionarCarpeta = new System.Windows.Forms.Button();
            this.lblCalidad = new System.Windows.Forms.Label();
            this.cmbCalidad = new System.Windows.Forms.ComboBox();
            this.chkSoloAudio = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnDescargar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblEstado = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.lblTitulo.Text = "Descargar Videos";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkSoloAudio);
            this.panel2.Controls.Add(this.cmbCalidad);
            this.panel2.Controls.Add(this.lblCalidad);
            this.panel2.Controls.Add(this.btnSeleccionarCarpeta);
            this.panel2.Controls.Add(this.txtCarpeta);
            this.panel2.Controls.Add(this.lblCarpeta);
            this.panel2.Controls.Add(this.txtUrl);
            this.panel2.Controls.Add(this.lblUrl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(20);
            this.panel2.Size = new System.Drawing.Size(800, 180);
            this.panel2.TabIndex = 1;
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblUrl.Location = new System.Drawing.Point(23, 23);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(91, 20);
            this.lblUrl.TabIndex = 0;
            this.lblUrl.Text = "URL Video:";
            // 
            // txtUrl
            // 
            this.txtUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtUrl.Location = new System.Drawing.Point(120, 21);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(657, 23);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "Pega aquí el enlace del video...";
            this.txtUrl.ForeColor = System.Drawing.Color.Gray;
            this.txtUrl.Enter += new System.EventHandler(this.txtUrl_Enter);
            this.txtUrl.Leave += new System.EventHandler(this.txtUrl_Leave);
            // 
            // lblCarpeta
            // 
            this.lblCarpeta.AutoSize = true;
            this.lblCarpeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblCarpeta.Location = new System.Drawing.Point(23, 60);
            this.lblCarpeta.Name = "lblCarpeta";
            this.lblCarpeta.Size = new System.Drawing.Size(70, 20);
            this.lblCarpeta.TabIndex = 2;
            this.lblCarpeta.Text = "Carpeta:";
            // 
            // txtCarpeta
            // 
            this.txtCarpeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtCarpeta.Location = new System.Drawing.Point(120, 58);
            this.txtCarpeta.Name = "txtCarpeta";
            this.txtCarpeta.ReadOnly = true;
            this.txtCarpeta.Size = new System.Drawing.Size(500, 23);
            this.txtCarpeta.TabIndex = 3;
            this.txtCarpeta.Text = "Selecciona carpeta de descarga...";
            this.txtCarpeta.ForeColor = System.Drawing.Color.Gray;
            // 
            // btnSeleccionarCarpeta
            // 
            this.btnSeleccionarCarpeta.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnSeleccionarCarpeta.Location = new System.Drawing.Point(635, 56);
            this.btnSeleccionarCarpeta.Name = "btnSeleccionarCarpeta";
            this.btnSeleccionarCarpeta.Size = new System.Drawing.Size(142, 27);
            this.btnSeleccionarCarpeta.TabIndex = 4;
            this.btnSeleccionarCarpeta.Text = "Seleccionar";
            this.btnSeleccionarCarpeta.UseVisualStyleBackColor = true;
            this.btnSeleccionarCarpeta.Click += new System.EventHandler(this.btnSeleccionarCarpeta_Click);
            // 
            // lblCalidad
            // 
            this.lblCalidad.AutoSize = true;
            this.lblCalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblCalidad.Location = new System.Drawing.Point(23, 97);
            this.lblCalidad.Name = "lblCalidad";
            this.lblCalidad.Size = new System.Drawing.Size(67, 20);
            this.lblCalidad.TabIndex = 5;
            this.lblCalidad.Text = "Calidad:";
            // 
            // cmbCalidad
            // 
            this.cmbCalidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCalidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbCalidad.FormattingEnabled = true;
            this.cmbCalidad.Items.AddRange(new object[] {
            "Mejor calidad disponible",
            "1080p",
            "720p",
            "480p",
            "360p"});
            this.cmbCalidad.Location = new System.Drawing.Point(120, 95);
            this.cmbCalidad.Name = "cmbCalidad";
            this.cmbCalidad.Size = new System.Drawing.Size(200, 24);
            this.cmbCalidad.TabIndex = 6;
            this.cmbCalidad.SelectedIndex = 0;
            // 
            // chkSoloAudio
            // 
            this.chkSoloAudio.AutoSize = true;
            this.chkSoloAudio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.chkSoloAudio.Location = new System.Drawing.Point(120, 135);
            this.chkSoloAudio.Name = "chkSoloAudio";
            this.chkSoloAudio.Size = new System.Drawing.Size(169, 24);
            this.chkSoloAudio.TabIndex = 7;
            this.chkSoloAudio.Text = "Solo audio (MP3)";
            this.chkSoloAudio.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnLimpiar);
            this.panel3.Controls.Add(this.btnDescargar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 240);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(20);
            this.panel3.Size = new System.Drawing.Size(800, 60);
            this.panel3.TabIndex = 2;
            // 
            // btnDescargar
            // 
            this.btnDescargar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(217)))), ((int)(((byte)(55)))));
            this.btnDescargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDescargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnDescargar.ForeColor = System.Drawing.Color.White;
            this.btnDescargar.Location = new System.Drawing.Point(300, 15);
            this.btnDescargar.Name = "btnDescargar";
            this.btnDescargar.Size = new System.Drawing.Size(120, 35);
            this.btnDescargar.TabIndex = 0;
            this.btnDescargar.Text = "Descargar";
            this.btnDescargar.UseVisualStyleBackColor = false;
            this.btnDescargar.Click += new System.EventHandler(this.btnDescargar_Click);
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
            this.panel4.Location = new System.Drawing.Point(0, 300);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(20);
            this.panel4.Size = new System.Drawing.Size(800, 250);
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
            this.txtLog.Size = new System.Drawing.Size(754, 147);
            this.txtLog.TabIndex = 2;
            // 
            // FormDescargaVideos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 550);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDescargaVideos";
            this.Text = "Descarga de Videos";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label lblCarpeta;
        private System.Windows.Forms.TextBox txtCarpeta;
        private System.Windows.Forms.Button btnSeleccionarCarpeta;
        private System.Windows.Forms.Label lblCalidad;
        private System.Windows.Forms.ComboBox cmbCalidad;
        private System.Windows.Forms.CheckBox chkSoloAudio;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnDescargar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}