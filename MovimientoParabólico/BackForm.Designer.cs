namespace MovimientoParabólico
{
    partial class BackForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackForm));
            this.picTejo = new System.Windows.Forms.PictureBox();
            this.picObstaculo = new System.Windows.Forms.PictureBox();
            this.picObjetivo = new System.Windows.Forms.PictureBox();
            this.picSuelo = new System.Windows.Forms.PictureBox();
            this.picPlataforma = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picTejo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picObstaculo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picObjetivo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSuelo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlataforma)).BeginInit();
            this.SuspendLayout();
            // 
            // picTejo
            // 
            this.picTejo.Image = ((System.Drawing.Image)(resources.GetObject("picTejo.Image")));
            this.picTejo.Location = new System.Drawing.Point(120, 443);
            this.picTejo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picTejo.Name = "picTejo";
            this.picTejo.Size = new System.Drawing.Size(73, 63);
            this.picTejo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTejo.TabIndex = 0;
            this.picTejo.TabStop = false;
            this.picTejo.Visible = false;
            // 
            // picObstaculo
            // 
            this.picObstaculo.Image = ((System.Drawing.Image)(resources.GetObject("picObstaculo.Image")));
            this.picObstaculo.Location = new System.Drawing.Point(360, 428);
            this.picObstaculo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picObstaculo.Name = "picObstaculo";
            this.picObstaculo.Size = new System.Drawing.Size(120, 139);
            this.picObstaculo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picObstaculo.TabIndex = 1;
            this.picObstaculo.TabStop = false;
            this.picObstaculo.Visible = false;
            // 
            // picObjetivo
            // 
            this.picObjetivo.BackColor = System.Drawing.SystemColors.Control;
            this.picObjetivo.Image = ((System.Drawing.Image)(resources.GetObject("picObjetivo.Image")));
            this.picObjetivo.Location = new System.Drawing.Point(973, 355);
            this.picObjetivo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picObjetivo.Name = "picObjetivo";
            this.picObjetivo.Size = new System.Drawing.Size(126, 110);
            this.picObjetivo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picObjetivo.TabIndex = 2;
            this.picObjetivo.TabStop = false;
            this.picObjetivo.Visible = false;
            // 
            // picSuelo
            // 
            this.picSuelo.BackColor = System.Drawing.Color.Green;
            this.picSuelo.Location = new System.Drawing.Point(215, 566);
            this.picSuelo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picSuelo.Name = "picSuelo";
            this.picSuelo.Size = new System.Drawing.Size(893, 42);
            this.picSuelo.TabIndex = 3;
            this.picSuelo.TabStop = false;
            this.picSuelo.Visible = false;
            // 
            // picPlataforma
            // 
            this.picPlataforma.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPlataforma.Image = ((System.Drawing.Image)(resources.GetObject("picPlataforma.Image")));
            this.picPlataforma.Location = new System.Drawing.Point(653, 155);
            this.picPlataforma.Margin = new System.Windows.Forms.Padding(4);
            this.picPlataforma.Name = "picPlataforma";
            this.picPlataforma.Size = new System.Drawing.Size(124, 30);
            this.picPlataforma.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPlataforma.TabIndex = 4;
            this.picPlataforma.TabStop = false;
            // 
            // BackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1123, 634);
            this.Controls.Add(this.picPlataforma);
            this.Controls.Add(this.picSuelo);
            this.Controls.Add(this.picObjetivo);
            this.Controls.Add(this.picObstaculo);
            this.Controls.Add(this.picTejo);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "BackForm";
            this.Text = "BackForm";
            this.Load += new System.EventHandler(this.BackForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BackForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.picTejo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picObstaculo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picObjetivo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSuelo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlataforma)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picTejo;
        private System.Windows.Forms.PictureBox picObstaculo;
        private System.Windows.Forms.PictureBox picObjetivo;
        private System.Windows.Forms.PictureBox picSuelo;
        private System.Windows.Forms.PictureBox picPlataforma;
    }
}

