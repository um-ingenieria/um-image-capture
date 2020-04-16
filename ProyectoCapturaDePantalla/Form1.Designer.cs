namespace ProyectoCapturaDePantalla
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxImg = new System.Windows.Forms.PictureBox();
            this.timerCaptura = new System.Windows.Forms.Timer(this.components);
            this.timerLapso = new System.Windows.Forms.Timer(this.components);
            this.buttonEmpezar = new System.Windows.Forms.Button();
            this.buttonTerminar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxWebCam = new System.Windows.Forms.PictureBox();
            this.comboBoxWebCam = new System.Windows.Forms.ComboBox();
            this.comboBoxPantallas = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelNombre = new System.Windows.Forms.Label();
            this.buttonRunSP = new System.Windows.Forms.Button();
            this.emotionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWebCam)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxImg
            // 
            this.pictureBoxImg.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxImg.Location = new System.Drawing.Point(16, 15);
            this.pictureBoxImg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxImg.Name = "pictureBoxImg";
            this.pictureBoxImg.Size = new System.Drawing.Size(432, 289);
            this.pictureBoxImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxImg.TabIndex = 0;
            this.pictureBoxImg.TabStop = false;
            // 
            // timerCaptura
            // 
            this.timerCaptura.Tick += new System.EventHandler(this.timerCaptura_Tick);
            // 
            // timerLapso
            // 
            this.timerLapso.Enabled = true;
            this.timerLapso.Tick += new System.EventHandler(this.timerLapso_Tick);
            // 
            // buttonEmpezar
            // 
            this.buttonEmpezar.Location = new System.Drawing.Point(13, 382);
            this.buttonEmpezar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonEmpezar.Name = "buttonEmpezar";
            this.buttonEmpezar.Size = new System.Drawing.Size(203, 81);
            this.buttonEmpezar.TabIndex = 5;
            this.buttonEmpezar.Text = "Empezar";
            this.buttonEmpezar.UseVisualStyleBackColor = true;
            this.buttonEmpezar.Click += new System.EventHandler(this.buttonEmpezar_Click);
            // 
            // buttonTerminar
            // 
            this.buttonTerminar.Location = new System.Drawing.Point(252, 382);
            this.buttonTerminar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonTerminar.Name = "buttonTerminar";
            this.buttonTerminar.Size = new System.Drawing.Size(196, 82);
            this.buttonTerminar.TabIndex = 6;
            this.buttonTerminar.Text = "Terminar";
            this.buttonTerminar.UseVisualStyleBackColor = true;
            this.buttonTerminar.Click += new System.EventHandler(this.buttonTerminar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.pictureBoxWebCam);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 325);
            this.panel1.TabIndex = 9;
            // 
            // pictureBoxWebCam
            // 
            this.pictureBoxWebCam.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBoxWebCam.Location = new System.Drawing.Point(497, 16);
            this.pictureBoxWebCam.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxWebCam.Name = "pictureBoxWebCam";
            this.pictureBoxWebCam.Size = new System.Drawing.Size(432, 289);
            this.pictureBoxWebCam.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxWebCam.TabIndex = 10;
            this.pictureBoxWebCam.TabStop = false;
            // 
            // comboBoxWebCam
            // 
            this.comboBoxWebCam.FormattingEnabled = true;
            this.comboBoxWebCam.Location = new System.Drawing.Point(740, 340);
            this.comboBoxWebCam.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxWebCam.Name = "comboBoxWebCam";
            this.comboBoxWebCam.Size = new System.Drawing.Size(181, 24);
            this.comboBoxWebCam.TabIndex = 10;
            this.comboBoxWebCam.SelectedIndexChanged += new System.EventHandler(this.comboBoxWebCam_SelectedIndexChanged);
            // 
            // comboBoxPantallas
            // 
            this.comboBoxPantallas.FormattingEnabled = true;
            this.comboBoxPantallas.Location = new System.Drawing.Point(509, 340);
            this.comboBoxPantallas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxPantallas.Name = "comboBoxPantallas";
            this.comboBoxPantallas.Size = new System.Drawing.Size(181, 24);
            this.comboBoxPantallas.TabIndex = 11;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(224, 340);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(224, 22);
            this.textBoxName.TabIndex = 12;
            // 
            // labelNombre
            // 
            this.labelNombre.AutoSize = true;
            this.labelNombre.Location = new System.Drawing.Point(16, 343);
            this.labelNombre.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelNombre.Name = "labelNombre";
            this.labelNombre.Size = new System.Drawing.Size(202, 17);
            this.labelNombre.TabIndex = 13;
            this.labelNombre.Text = "Inserte el nombre de la prueba";
            // 
            // buttonRunSP
            // 
            this.buttonRunSP.Location = new System.Drawing.Point(783, 382);
            this.buttonRunSP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonRunSP.Name = "buttonRunSP";
            this.buttonRunSP.Size = new System.Drawing.Size(139, 81);
            this.buttonRunSP.TabIndex = 14;
            this.buttonRunSP.Text = "Correr proceso de carga";
            this.buttonRunSP.UseVisualStyleBackColor = true;
            this.buttonRunSP.Click += new System.EventHandler(this.buttonRunSP_Click);
            // 
            // emotionButton
            // 
            this.emotionButton.Location = new System.Drawing.Point(531, 382);
            this.emotionButton.Margin = new System.Windows.Forms.Padding(4);
            this.emotionButton.Name = "emotionButton";
            this.emotionButton.Size = new System.Drawing.Size(139, 81);
            this.emotionButton.TabIndex = 15;
            this.emotionButton.Text = "Correr proceso de reconocimiento emocional";
            this.emotionButton.UseVisualStyleBackColor = true;
            //this.emotionButton.Click += new System.EventHandler(this.emotionButton_ClickAsync);
            this.emotionButton.Click += async (s, e) => await this.emotionButton_ClickAsync(s, e);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 479);
            this.Controls.Add(this.emotionButton);
            this.Controls.Add(this.buttonRunSP);
            this.Controls.Add(this.labelNombre);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.comboBoxPantallas);
            this.Controls.Add(this.comboBoxWebCam);
            this.Controls.Add(this.buttonTerminar);
            this.Controls.Add(this.buttonEmpezar);
            this.Controls.Add(this.pictureBoxImg);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWebCam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxImg;
        private System.Windows.Forms.Timer timerCaptura;
        private System.Windows.Forms.Timer timerLapso;
        private System.Windows.Forms.Button buttonEmpezar;
        private System.Windows.Forms.Button buttonTerminar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxWebCam;
        private System.Windows.Forms.ComboBox comboBoxWebCam;
        private System.Windows.Forms.ComboBox comboBoxPantallas;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelNombre;
        private System.Windows.Forms.Button buttonRunSP;
        private System.Windows.Forms.Button emotionButton;
    }
}

