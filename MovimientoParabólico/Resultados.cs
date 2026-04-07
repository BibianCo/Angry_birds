using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MovimientoParabólico
{
    public partial class Cálculos : Form
    {
        private double v0x, v0y;

        // ── Datos acumulados (siempre se guardan, independiente de visibilidad) ──
        private List<double[]> puntosTabla = new List<double[]>();

        private List<double> _listT = new List<double>();
        private List<double> _listX = new List<double>();
        private List<double> _listY = new List<double>();
        private List<double> _listVx = new List<double>();
        private List<double> _listVy = new List<double>();
        private List<double> _listV = new List<double>();
        private List<double> _listAng = new List<double>();

        private List<double> _reboteT = new List<double>();
        private List<double> _reboteX = new List<double>();
        private List<double> _reboteY = new List<double>();
        private List<double> _reboteVx = new List<double>();
        private List<double> _reboteVy = new List<double>();
        private List<double> _reboteV = new List<double>();
        private List<double> _reboteAng = new List<double>();

        // Último estado recibido (para refrescar al abrir el form)
        private double _ultT, _ultXt, _ultYt, _ultVxt, _ultVyt, _ultVt, _ultAng;
        private bool _hayDatos = false;

        public Cálculos()
        {
            InitializeComponent();
        }

        private void Cálculos_Load(object sender, EventArgs e)
        {
            ResetearLabels();
        }

        // Al hacerse visible, refrescar todo con los datos ya acumulados
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if ( Visible && _hayDatos )
                RefrescarTodo();
        }

        protected override void WndProc(ref Message m)
        {
            if ( m.Msg == 0x0112 && (m.WParam.ToInt32() & 0xFFF0) == 0xF010 )
                return;
            base.WndProc(ref m);
        }

        // ════════════════════════════════════════════════════════════════
        //  VELOCIDAD INICIAL
        // ════════════════════════════════════════════════════════════════
        public void EstablecerVelocidadInicial(double vx0, double vy0)
        {
            v0x = vx0;
            v0y = vy0;
            puntosTabla.Clear();

            if ( Visible )
            {
                double mag = Math.Sqrt(v0x * v0x + v0y * v0y);
                double ang = Math.Atan2(v0y, v0x) * 180.0 / Math.PI;
                lblVelX0.Text    = $"{v0x:F2} px/s";
                lblVelY0.Text    = $"{v0y:F2} px/s";
                lblMagVel0.Text  = $"{mag:F2} px/s";
                lblAngVelx0.Text = $"{ang:F2} °";
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  ACTUALIZACIÓN EN TIEMPO REAL
        // ════════════════════════════════════════════════════════════════
        public void ActualizarEnTiempoReal(
            double t, double xt, double yt,
            double vxt, double vyt, double vt, double ang,
            List<double> listT, List<double> listX, List<double> listY,
            List<double> listVx, List<double> listVy, List<double> listV,
            List<double> listAng,
            List<double> reboteT, List<double> reboteX, List<double> reboteY,
            List<double> reboteVx, List<double> reboteVy, List<double> reboteV,
            List<double> reboteAng)
        {
            // ── Guardar último estado siempre, independiente de visibilidad ──
            _ultT = t; _ultXt = xt; _ultYt = yt;
            _ultVxt = vxt; _ultVyt = vyt; _ultVt = vt; _ultAng = ang;
            _hayDatos = true;

            // Copiar snapshot de todas las listas (incluye rebotes)
            CopiarListas(listT, listX, listY, listVx, listVy, listV, listAng,
                         reboteT, reboteX, reboteY, reboteVx, reboteVy, reboteV, reboteAng);

            // Acumular punto para tabla
            puntosTabla.Add(new double[] { t, xt, yt, vxt, vyt, vt, ang });

            // Solo actualizar UI si está visible
            if ( !Visible ) return;

            RefrescarLabels();
            ActualizarGraficas();
        }

        // ════════════════════════════════════════════════════════════════
        //  REFRESCAR TODO (se llama al abrir el form con datos ya cargados)
        // ════════════════════════════════════════════════════════════════
        private void RefrescarTodo()
        {
            RefrescarLabels();
            ActualizarGraficas();
        }

        // ════════════════════════════════════════════════════════════════
        //  REFRESCAR LABELS
        // ════════════════════════════════════════════════════════════════
        private void RefrescarLabels()
        {
            // ── Velocidad inicial (fija, usa campos guardados) ──
            double mag0 = Math.Sqrt(v0x * v0x + v0y * v0y);
            double ang0 = Math.Atan2(v0y, v0x) * 180.0 / Math.PI;
            lblVelX0.Text    = $"{v0x:F2} px/s";
            lblVelY0.Text    = $"{v0y:F2} px/s";
            lblMagVel0.Text  = $"{mag0:F2} px/s";
            lblAngVelx0.Text = $"{ang0:F2} °";

            // ── Datos generales ──
            lblTiempoVuelo.Text = $"{_ultT:F2} s";
            lblAlcance.Text     = $"{_ultXt:F2} px";

            double altMax = 0;
            int idxAltMax = 0;
            for ( int i = 0 ; i < _listY.Count ; i++ )
            {
                if ( _listY[i] > altMax ) { altMax = _listY[i]; idxAltMax = i; }
            }
            lblAltMax.Text = $"{altMax:F2} px";

            if ( idxAltMax < _listVx.Count )
            {
                lblVelX.Text    = $"{_listVx[idxAltMax]:F2} px/s";
                lblVelY.Text    = $"{_listVy[idxAltMax]:F2} px/s";
                lblMagVel.Text  = $"{_listV[idxAltMax]:F2} px/s";
                lblAngVelX.Text = $"{_listAng[idxAltMax]:F2} °";
            }

            // ── Velocidad final (instante actual) ──
            lblVelXf.Text   = $"{_ultVxt:F2} px/s";
            lblVelYf.Text   = $"{_ultVyt:F2} px/s";
            lblMagVelF.Text = $"{_ultVt:F2} px/s";
            lblAngVelF.Text = $"{_ultAng:F2} °";
        }

        // ════════════════════════════════════════════════════════════════
        //  COPIAR LISTAS (snapshot para uso interno)
        // ════════════════════════════════════════════════════════════════
        private void CopiarListas(
            List<double> listT, List<double> listX, List<double> listY,
            List<double> listVx, List<double> listVy, List<double> listV,
            List<double> listAng,
            List<double> reboteT, List<double> reboteX, List<double> reboteY,
            List<double> reboteVx, List<double> reboteVy, List<double> reboteV,
            List<double> reboteAng)
        {
            _listT   = new List<double>(listT);
            _listX   = new List<double>(listX);
            _listY   = new List<double>(listY);
            _listVx  = new List<double>(listVx);
            _listVy  = new List<double>(listVy);
            _listV   = new List<double>(listV);
            _listAng = new List<double>(listAng);

            _reboteT   = new List<double>(reboteT);
            _reboteX   = new List<double>(reboteX);
            _reboteY   = new List<double>(reboteY);
            _reboteVx  = new List<double>(reboteVx);
            _reboteVy  = new List<double>(reboteVy);
            _reboteV   = new List<double>(reboteV);
            _reboteAng = new List<double>(reboteAng);
        }

        // ════════════════════════════════════════════════════════════════
        //  LIMPIAR LISTAS INTERNAS
        // ════════════════════════════════════════════════════════════════
        private void LimpiarListasInternas()
        {
            _listT.Clear(); _listX.Clear(); _listY.Clear();
            _listVx.Clear(); _listVy.Clear(); _listV.Clear();
            _listAng.Clear();

            // FIX: limpiar también las listas de rebotes internas para que
            // no queden marcadores fantasma de ejecuciones anteriores
            _reboteT.Clear(); _reboteX.Clear(); _reboteY.Clear();
            _reboteVx.Clear(); _reboteVy.Clear(); _reboteV.Clear();
            _reboteAng.Clear();
        }

        private void dataGridDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // ════════════════════════════════════════════════════════════════
        //  FINALIZAR TABLA
        // ════════════════════════════════════════════════════════════════
        public void FinalizarTabla(int numRebotes)
        {
            if ( puntosTabla.Count == 0 ) return;

            int maxTray = Math.Max(1, 10 - numRebotes);
            int total = puntosTabla.Count;

            List<int> indices = new List<int>();
            if ( total <= maxTray )
            {
                for ( int i = 0 ; i < total ; i++ ) indices.Add(i);
            }
            else
            {
                for ( int i = 0 ; i < maxTray ; i++ )
                    indices.Add((int)Math.Round((double)i * (total - 1) / (maxTray - 1)));
            }

            foreach ( int idx in indices )
            {
                double[] p = puntosTabla[idx];
                dataGridDatos.Rows.Add(
                    "Trayectoria", "",
                    Math.Round(p[0], 2), Math.Round(p[1], 2), Math.Round(p[2], 2),
                    Math.Round(p[3], 2), Math.Round(p[4], 2),
                    Math.Round(p[5], 2), Math.Round(p[6], 2)
                );
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  REBOTES EN TABLA
        // ════════════════════════════════════════════════════════════════
        public void ImprimirRebote(int rebote, double t, double x, double y,
                                    double vx, double vy, double v, double ang)
        {
            dataGridDatos.Rows.Add(
                "Rebote", rebote,
                Math.Round(t, 2), Math.Round(x, 2), Math.Round(y, 2),
                Math.Round(vx, 2), Math.Round(vy, 2), Math.Round(v, 2),
                Math.Round(ang, 2)
            );
        }

        // ════════════════════════════════════════════════════════════════
        //  GRÁFICAS
        // ════════════════════════════════════════════════════════════════
        private void ActualizarGraficas()
        {
            LlenarChart(chartYT, _listT, _listY, _reboteT, _reboteY);
            LlenarChart(chartXT, _listT, _listX, _reboteT, _reboteX);
            LlenarChart(chartYX, _listX, _listY, _reboteX, _reboteY);
            LlenarChart(chartVxT, _listT, _listVx, _reboteT, _reboteVx);
            LlenarChart(chartVyT, _listT, _listVy, _reboteT, _reboteVy);
            LlenarChart(chartVT, _listT, _listV, _reboteT, _reboteV);
            LlenarChart(chartAngT, _listT, _listAng, _reboteT, _reboteAng);
        }

        private void LlenarChart(Chart chart,
                                  List<double> ejeX, List<double> ejeY,
                                  List<double> rebX, List<double> rebY)
        {
            chart.Series["Trayectoria"].Points.Clear();
            chart.Series["Rebotes"].Points.Clear();

            int nTray = Math.Min(ejeX.Count, ejeY.Count);
            for ( int i = 0 ; i < nTray ; i++ )
                chart.Series["Trayectoria"].Points.AddXY(ejeX[i], ejeY[i]);

            int nReb = Math.Min(rebX.Count, rebY.Count);
            if ( nReb == 0 ) return;

            for ( int i = 0 ; i < nReb ; i++ )
                chart.Series["Rebotes"].Points.AddXY(rebX[i], rebY[i]);
        }

        // ════════════════════════════════════════════════════════════════
        //  LIMPIAR (llamado desde ProcessForm al reiniciar)
        // ════════════════════════════════════════════════════════════════
        public void LimpiarGraficas()
        {
            // FIX: limpiar listas internas ANTES de limpiar los charts
            // para que no queden datos de rebotes de ejecuciones anteriores
            LimpiarListasInternas();
            _hayDatos = false;

            foreach ( var chart in new Chart[] { chartYT, chartXT, chartYX,
                                                 chartVxT, chartVyT, chartVT, chartAngT } )
            {
                chart.Series["Trayectoria"].Points.Clear();
                chart.Series["Rebotes"].Points.Clear();
            }
        }

        public void LimpiarTabla()
        {
            dataGridDatos.Rows.Clear();
            puntosTabla.Clear();
            _hayDatos = false;
            ResetearLabels();
        }

        // ════════════════════════════════════════════════════════════════
        //  RESET LABELS
        // ════════════════════════════════════════════════════════════════
        private void ResetearLabels()
        {
            lblTiempoVuelo.Text = "---"; lblAlcance.Text  = "---"; lblAltMax.Text   = "---";
            lblVelX.Text        = "---"; lblVelY.Text     = "---"; lblMagVel.Text   = "---";
            lblAngVelX.Text     = "---"; lblVelX0.Text    = "---"; lblVelY0.Text    = "---";
            lblMagVel0.Text     = "---"; lblAngVelx0.Text = "---"; lblVelXf.Text    = "---";
            lblVelYf.Text       = "---"; lblMagVelF.Text  = "---"; lblAngVelF.Text  = "---";
        }
    }
}