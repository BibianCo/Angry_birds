using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovimientoParabólico
{
    public partial class BackForm : Form
    {
        public Point TejoLocation
        {
            get { return picTejo.Location; }
            set { picTejo.Location = value; }
        }
        public Size TejoSize
        {
            get { return picTejo.Size; }
            set { picTejo.Size = value; }
        }

        public Point ObstaculoLocation
        {
            get { return picObstaculo.Location; }
            set { picObstaculo.Location = value; }
        }
        public Size ObstaculoSize
        {
            get { return picObstaculo.Size; }
            set { picObstaculo.Size = value; }
        }
        public Point SueloLocation
        {
            get { return picSuelo.Location; }
            set { picSuelo.Location = value; }
        }
        public Size SueloSize
        {
            get { return picSuelo.Size; }
            set { picSuelo.Size = value; }
        }
        public Point ObjetivoLocation
        {
            get { return picObjetivo.Location; }
            set { picObjetivo.Location = value; }
        }
        public Size ObjetivoSize
        {
            get { return picObjetivo.Size; }
            set { picObjetivo.Size = value; }
        }

        public Point PlatafromaLocation
        {
            get { return picPlataforma.Location; }
            set { picPlataforma.Location = value; }
        }
        public Size PlataformaSize
        {
            get { return picPlataforma.Size; }
            set { picPlataforma.Size = value; }
        }


        public BackForm()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
        }

        private void BackForm_Load(object sender, EventArgs e)
        {

        }

        private void BackForm_MouseDown(object sender, MouseEventArgs e)
        {
            var processForm = Application.OpenForms["ProcessForm"];
            processForm?.BringToFront();
        }
    }
}
