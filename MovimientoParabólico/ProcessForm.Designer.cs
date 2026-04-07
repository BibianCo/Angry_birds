namespace MovimientoParabólico
{
    partial class ProcessForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessForm));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.picSueloF = new System.Windows.Forms.PictureBox();
            this.picObjetivoF = new System.Windows.Forms.PictureBox();
            this.picObstaculoF = new System.Windows.Forms.PictureBox();
            this.picTejoF = new System.Windows.Forms.PictureBox();
            this.btnReiniciar = new System.Windows.Forms.Button();
            this.picPlataformaF = new System.Windows.Forms.PictureBox();
            this.btnMostrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picSueloF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picObjetivoF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picObstaculoF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTejoF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlataformaF)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // picSueloF
            // 
            this.picSueloF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picSueloF.Location = new System.Drawing.Point(215, 566);
            this.picSueloF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picSueloF.Name = "picSueloF";
            this.picSueloF.Size = new System.Drawing.Size(900, 49);
            this.picSueloF.TabIndex = 7;
            this.picSueloF.TabStop = false;
            // 
            // picObjetivoF
            // 
            this.picObjetivoF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picObjetivoF.Image = ((System.Drawing.Image)(resources.GetObject("picObjetivoF.Image")));
            this.picObjetivoF.Location = new System.Drawing.Point(1012, 354);
            this.picObjetivoF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picObjetivoF.Name = "picObjetivoF";
            this.picObjetivoF.Size = new System.Drawing.Size(103, 89);
            this.picObjetivoF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picObjetivoF.TabIndex = 6;
            this.picObjetivoF.TabStop = false;
            // 
            // picObstaculoF
            // 
            this.picObstaculoF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picObstaculoF.Image = ((System.Drawing.Image)(resources.GetObject("picObstaculoF.Image")));
            this.picObstaculoF.Location = new System.Drawing.Point(360, 428);
            this.picObstaculoF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picObstaculoF.Name = "picObstaculoF";
            this.picObstaculoF.Size = new System.Drawing.Size(120, 139);
            this.picObstaculoF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picObstaculoF.TabIndex = 5;
            this.picObstaculoF.TabStop = false;
            // 
            // picTejoF
            // 
            this.picTejoF.Image = ((System.Drawing.Image)(resources.GetObject("picTejoF.Image")));
            this.picTejoF.Location = new System.Drawing.Point(120, 443);
            this.picTejoF.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picTejoF.Name = "picTejoF";
            this.picTejoF.Size = new System.Drawing.Size(73, 63);
            this.picTejoF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTejoF.TabIndex = 4;
            this.picTejoF.TabStop = false;
            this.picTejoF.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picTejoF_MouseDown);
            this.picTejoF.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picTejoF_MouseMove);
            this.picTejoF.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picTejoF_MouseUp);
            // 
            // btnReiniciar
            // 
            this.btnReiniciar.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReiniciar.Location = new System.Drawing.Point(12, 24);
            this.btnReiniciar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new System.Drawing.Size(131, 42);
            this.btnReiniciar.TabIndex = 8;
            this.btnReiniciar.Text = "Reiniciar";
            this.btnReiniciar.UseVisualStyleBackColor = true;
            this.btnReiniciar.Click += new System.EventHandler(this.button1_Click);
            // 
            // picPlataformaF
            // 
            this.picPlataformaF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPlataformaF.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picPlataformaF.Image = ((System.Drawing.Image)(resources.GetObject("picPlataformaF.Image")));
            this.picPlataformaF.Location = new System.Drawing.Point(653, 155);
            this.picPlataformaF.Margin = new System.Windows.Forms.Padding(4);
            this.picPlataformaF.Name = "picPlataformaF";
            this.picPlataformaF.Size = new System.Drawing.Size(124, 30);
            this.picPlataformaF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPlataformaF.TabIndex = 9;
            this.picPlataformaF.TabStop = false;
            // 
            // btnMostrar
            // 
            this.btnMostrar.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMostrar.Location = new System.Drawing.Point(980, 24);
            this.btnMostrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMostrar.Name = "btnMostrar";
            this.btnMostrar.Size = new System.Drawing.Size(131, 76);
            this.btnMostrar.TabIndex = 10;
            this.btnMostrar.Text = "Mostrar datos";
            this.btnMostrar.UseVisualStyleBackColor = true;
            this.btnMostrar.Click += new System.EventHandler(this.btnMostrar_Click);
            // 
            // ProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1123, 634);
            this.Controls.Add(this.btnMostrar);
            this.Controls.Add(this.picPlataformaF);
            this.Controls.Add(this.picTejoF);
            this.Controls.Add(this.btnReiniciar);
            this.Controls.Add(this.picSueloF);
            this.Controls.Add(this.picObjetivoF);
            this.Controls.Add(this.picObstaculoF);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ProcessForm";
            this.Text = "ProcessForm";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Load += new System.EventHandler(this.ProcessForm_Load);
            this.Move += new System.EventHandler(this.ProcessForm_Move);
            this.Resize += new System.EventHandler(this.ProcessForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picSueloF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picObjetivoF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picObstaculoF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTejoF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPlataformaF)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picSueloF;
        private System.Windows.Forms.PictureBox picObjetivoF;
        private System.Windows.Forms.PictureBox picObstaculoF;
        private System.Windows.Forms.PictureBox picTejoF;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnReiniciar;
        private System.Windows.Forms.PictureBox picPlataformaF;
        private System.Windows.Forms.Button btnMostrar;
    }
}