using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MovimientoParabólico
{
    public partial class ProcessForm : Form
    {
        public ProcessForm()
        {
            InitializeComponent();
        }

        BackForm backForm;
        Cálculos formCalculos;

        int startMouseX, startMouseY;
        int deltaX, deltaY;
        int initialTejoX, initialTejoY;

        double x0, y0;
        double v0x, v0y;
        double gravity = 9.8;
        double t = 0;
        double tAcumulado = 0;

        bool enMovimiento = false;
        bool simulacionTerminada = false;

        // Rebotes
        int numRebotes = 0;
        const int MAX_REBOTES = 2;
        const double FACTOR_REBOTE = 0.6;

        // Cooldowns independientes por superficie.
        int cooldownPlataforma = 0;
        int cooldownSuelo = 0;
        const int COOLDOWN_PLATAFORMA_TICKS = 8;
        const int COOLDOWN_SUELO_TICKS = 5;

        // Listas trayectoria
        List<double> listT = new List<double>();
        List<double> listX = new List<double>();
        List<double> listY = new List<double>();
        List<double> listVx = new List<double>();
        List<double> listVy = new List<double>();
        List<double> listV = new List<double>();
        List<double> listAng = new List<double>();

        // Listas rebotes
        List<double> reboteT = new List<double>();
        List<double> reboteX = new List<double>();
        List<double> reboteY = new List<double>();
        List<double> reboteVx = new List<double>();
        List<double> reboteVy = new List<double>();
        List<double> reboteV = new List<double>();
        List<double> reboteAng = new List<double>();

        // ── Posiciones relativas (fracción del ClientSize) ───────────────
        private PointF relTejo, relObstaculo, relSuelo;
        private PointF relObjetivo, relPlataforma;
        private SizeF relTejoSz, relObstaculoSz, relSueloSz;
        private SizeF relObjetivoSz, relPlataformaSz;

        // ── Helpers de conversión relativa ───────────────────────────────
        private PointF ToRelative(Point p) =>
            new PointF((float)p.X / ClientSize.Width,
                       (float)p.Y / ClientSize.Height);

        private SizeF ToRelativeSz(Size s) =>
            new SizeF((float)s.Width  / ClientSize.Width,
                      (float)s.Height / ClientSize.Height);

        private Point FromRelative(PointF r) =>
            new Point((int)(r.X * ClientSize.Width),
                      (int)(r.Y * ClientSize.Height));

        private Size FromRelativeSz(SizeF r) =>
            new Size(Math.Max(1, (int)(r.Width  * ClientSize.Width)),
                     Math.Max(1, (int)(r.Height * ClientSize.Height)));

        // ── Aplica posiciones relativas a todos los PictureBox ───────────
        private void AplicarPosicionesRelativas()
        {
            picTejoF.Location       = FromRelative(relTejo);
            picTejoF.Size           = FromRelativeSz(relTejoSz);

            picObstaculoF.Location  = FromRelative(relObstaculo);
            picObstaculoF.Size      = FromRelativeSz(relObstaculoSz);

            picSueloF.Location      = FromRelative(relSuelo);
            picSueloF.Size          = FromRelativeSz(relSueloSz);

            picObjetivoF.Location   = FromRelative(relObjetivo);
            picObjetivoF.Size       = FromRelativeSz(relObjetivoSz);

            picPlataformaF.Location = FromRelative(relPlataforma);
            picPlataformaF.Size     = FromRelativeSz(relPlataformaSz);

            // Sincronizar backForm
            if ( backForm != null )
            {
                backForm.TejoLocation       = picTejoF.Location;
                backForm.ObstaculoLocation  = picObstaculoF.Location;
                backForm.SueloLocation      = picSueloF.Location;
                backForm.ObjetivoLocation   = picObjetivoF.Location;
                backForm.PlatafromaLocation = picPlataformaF.Location;
            }
        }

        // ── Guarda las relativas de todos los PictureBox ─────────────────
        private void GuardarPosicionesRelativas()
        {
            relTejo        = ToRelative(picTejoF.Location);
            relTejoSz      = ToRelativeSz(picTejoF.Size);
            relObstaculo   = ToRelative(picObstaculoF.Location);
            relObstaculoSz = ToRelativeSz(picObstaculoF.Size);
            relSuelo       = ToRelative(picSueloF.Location);
            relSueloSz     = ToRelativeSz(picSueloF.Size);
            relObjetivo    = ToRelative(picObjetivoF.Location);
            relObjetivoSz  = ToRelativeSz(picObjetivoF.Size);
            relPlataforma  = ToRelative(picPlataformaF.Location);
            relPlataformaSz= ToRelativeSz(picPlataformaF.Size);
        }

        // ════════════════════════════════════════════════════════════════
        //  LOAD
        // ════════════════════════════════════════════════════════════════
        private void ProcessForm_Load(object sender, EventArgs e)
        {
            backForm = new BackForm()
            {
                StartPosition = FormStartPosition.Manual,
                Size          = this.Size,
                Location      = this.Location,
                ShowInTaskbar = false
            };
            backForm.Show();

            formCalculos = new Cálculos();
            formCalculos.StartPosition = FormStartPosition.Manual;

            this.BringToFront();

            picTejoF.Location       = backForm.TejoLocation;
            picTejoF.Size           = backForm.TejoSize;
            picObstaculoF.Location  = backForm.ObstaculoLocation;
            picObstaculoF.Size      = backForm.ObstaculoSize;
            picSueloF.Location      = backForm.SueloLocation;
            picSueloF.Size          = backForm.SueloSize;
            picObjetivoF.Location   = backForm.ObjetivoLocation;
            picObjetivoF.Size       = backForm.ObjetivoSize;
            picPlataformaF.Location = backForm.PlatafromaLocation;
            picPlataformaF.Size     = backForm.PlataformaSize;

            initialTejoX = picTejoF.Location.X;
            initialTejoY = picTejoF.Location.Y;

            PosicionarObjetivosAleatorio();

            // Guardar posiciones relativas DESPUÉS de posicionar todo
            GuardarPosicionesRelativas();
        }

        private void PosicionarObjetivosAleatorio()
        {
            Random rnd = new Random();

            int pxPlataforma = rnd.Next(360, 550);
            int pyPlataforma = rnd.Next(200, 380);
            Point posPlataforma = new Point(pxPlataforma, pyPlataforma);
            picPlataformaF.Location     = posPlataforma;
            backForm.PlatafromaLocation = posPlataforma;

            int pyCerdito = rnd.Next(100, 350);
            Point posCerdito = new Point(680, pyCerdito);
            picObjetivoF.Location     = posCerdito;
            backForm.ObjetivoLocation = posCerdito;

            // Actualizar relativas con las nuevas posiciones aleatorias
            if ( ClientSize.Width > 0 && ClientSize.Height > 0 )
            {
                relPlataforma    = ToRelative(picPlataformaF.Location);
                relPlataformaSz  = ToRelativeSz(picPlataformaF.Size);
                relObjetivo      = ToRelative(picObjetivoF.Location);
                relObjetivoSz    = ToRelativeSz(picObjetivoF.Size);
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  MOVE / RESIZE
        // ════════════════════════════════════════════════════════════════
        private void ProcessForm_Move(object sender, EventArgs e)
        {
            if ( backForm != null )
                backForm.Location = this.Location;
        }

        private void ProcessForm_Resize(object sender, EventArgs e)
        {
            if ( backForm != null )
            {
                backForm.Size     = this.Size;
                backForm.Location = this.Location;
            }

            // Evitar división por cero durante minimización
            if ( ClientSize.Width <= 0 || ClientSize.Height <= 0 ) return;

            // Reubicar todos los objetos proporcionalmente
            AplicarPosicionesRelativas();

            // Actualizar origen del tejo para la física
            // Solo si la simulación no está en curso
            if ( !enMovimiento )
            {
                initialTejoX = picTejoF.Location.X;
                initialTejoY = picTejoF.Location.Y;
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  BOTÓN REINICIAR
        // ════════════════════════════════════════════════════════════════
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        // ════════════════════════════════════════════════════════════════
        //  BOTÓN MOSTRAR / OCULTAR CÁLCULOS
        // ════════════════════════════════════════════════════════════════
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            if ( formCalculos == null || formCalculos.IsDisposed )
            {
                formCalculos = new Cálculos();
                formCalculos.StartPosition = FormStartPosition.Manual;
            }

            if ( formCalculos.Visible )
            {
                formCalculos.Hide();
                btnMostrar.Text = "Mostrar datos";
            }
            else
            {
                formCalculos.StartPosition = FormStartPosition.CenterScreen;
                formCalculos.Show();
                formCalculos.StartPosition = FormStartPosition.Manual;
                btnMostrar.Text = "Ocultar datos";
            }

            this.BringToFront();
        }

        // ════════════════════════════════════════════════════════════════
        //  ARRASTRE DEL ANGRY BIRD
        // ════════════════════════════════════════════════════════════════
        private void picTejoF_MouseDown(object sender, MouseEventArgs e)
        {
            if ( enMovimiento || simulacionTerminada ) return;
            if ( e.Button == MouseButtons.Left )
            {
                startMouseX = e.X;
                startMouseY = e.Y;
            }
        }

        private void picTejoF_MouseMove(object sender, MouseEventArgs e)
        {
            if ( enMovimiento || simulacionTerminada ) return;
            if ( e.Button == MouseButtons.Left )
            {
                int newX = picTejoF.Location.X + e.X - startMouseX;
                int newY = picTejoF.Location.Y + e.Y - startMouseY;
                picTejoF.Location = new Point(newX, newY);
                deltaX = initialTejoX - picTejoF.Location.X;
                deltaY = picTejoF.Location.Y - initialTejoY;
            }
        }

        private void picTejoF_MouseUp(object sender, MouseEventArgs e)
        {
            if ( enMovimiento || simulacionTerminada ) return;
            if ( e.Button == MouseButtons.Left )
            {
                LimpiarDatos();
                numRebotes = 0;

                formCalculos.LimpiarTabla();
                formCalculos.LimpiarGraficas();

                cooldownPlataforma = COOLDOWN_PLATAFORMA_TICKS;
                cooldownSuelo      = COOLDOWN_SUELO_TICKS;

                // Lógica resortera
                x0  = -Math.Abs(deltaX);
                y0  = 0;
                v0x = Math.Max(1.0, Math.Abs(deltaX) * 1.0);
                v0y = deltaY;

                t          = 0;
                tAcumulado = 0;

                formCalculos.EstablecerVelocidadInicial(v0x, v0y);

                enMovimiento   = true;
                timer1.Enabled = true;
            }
        }

        // ════════════════════════════════════════════════════════════════
        //  TIMER
        // ════════════════════════════════════════════════════════════════
        private void timer1_Tick(object sender, EventArgs e)
        {
            // 1. Física
            double xt = v0x * t + x0;
            double yt = -0.5 * gravity * t * t + v0y * t + y0;
            double vxt = v0x;
            double vyt = -gravity * t + v0y;
            double vt = Math.Sqrt(vxt * vxt + vyt * vyt);
            double ang = Math.Atan2(vyt, vxt) * 180.0 / Math.PI;

            // 2. Mover tejo
            picTejoF.Location = new Point(
                initialTejoX + (int)xt,
                initialTejoY - (int)yt
            );

            // 3. Guardar datos
            listT.Add(Math.Round(tAcumulado, 2));
            listX.Add(Math.Round(xt, 2));
            listY.Add(Math.Round(yt, 2));
            listVx.Add(Math.Round(vxt, 2));
            listVy.Add(Math.Round(vyt, 2));
            listV.Add(Math.Round(vt, 2));
            listAng.Add(Math.Round(ang, 2));

            // 4. Reducir cooldowns
            if ( cooldownPlataforma > 0 ) cooldownPlataforma--;
            if ( cooldownSuelo      > 0 ) cooldownSuelo--;

            // 5. Actualizar cálculos
            formCalculos.ActualizarEnTiempoReal(
                tAcumulado, xt, yt, vxt, vyt, vt, ang,
                listT, listX, listY,
                listVx, listVy, listV, listAng,
                reboteT, reboteX, reboteY,
                reboteVx, reboteVy, reboteV, reboteAng
            );

            // ── CERDITO → detener ────────────────────────────────────────
            if ( picTejoF.Bounds.IntersectsWith(picObjetivoF.Bounds) )
            {
                Detener($"¡Le diste a King Pig!\n\nPosición: ({xt:F1}, {yt:F1}) px\nVelocidad: {vt:F2} px/s",
                    "¡Impacto!", MessageBoxIcon.Information);
                return;
            }

            // ── OBSTÁCULO → detener ──────────────────────────────────────
            if ( picTejoF.Bounds.IntersectsWith(picObstaculoF.Bounds) )
            {
                Detener("Chocaste con el obstáculo", "Colisión", MessageBoxIcon.Warning);
                return;
            }

            // ── PLATAFORMA → rebote ──────────────────────────────────────
            if ( picTejoF.Bounds.IntersectsWith(picPlataformaF.Bounds)
                && cooldownPlataforma == 0
                && numRebotes < MAX_REBOTES
                && xt > 1.0 )
            {
                Rectangle tejo = picTejoF.Bounds;
                Rectangle plat = picPlataformaF.Bounds;

                double newVx = vxt;
                double newVy = vyt;

                int overlapTop = tejo.Bottom - plat.Top;
                int overlapBottom = plat.Bottom  - tejo.Top;
                int overlapLeft = tejo.Right   - plat.Left;
                int overlapRight = plat.Right   - tejo.Left;

                int minVertical = Math.Min(overlapTop, overlapBottom);
                int minHorizontal = Math.Min(overlapLeft, overlapRight);

                double yFisicoRebote;

                if ( minVertical <= minHorizontal )
                {
                    // Rebote vertical
                    if ( vyt <= 0 )
                    {
                        // Cae sobre la plataforma desde arriba
                        picTejoF.Top  = plat.Top - picTejoF.Height;
                        yFisicoRebote = initialTejoY - picTejoF.Top;
                        newVx =  vxt * FACTOR_REBOTE;
                        newVy =  Math.Abs(vyt) * FACTOR_REBOTE;
                        x0    = xt;
                        y0    = yFisicoRebote;
                    }
                    else
                    {
                        // Golpea la base desde abajo
                        picTejoF.Top  = plat.Bottom;
                        yFisicoRebote = initialTejoY - picTejoF.Top;
                        newVx =  vxt * FACTOR_REBOTE;
                        newVy = -Math.Abs(vyt) * FACTOR_REBOTE;
                        x0    = xt;
                        y0    = yFisicoRebote;
                    }
                }
                else
                {
                    // Rebote lateral
                    if ( vxt >= 0 )
                    {
                        picTejoF.Left = plat.Left - picTejoF.Width;
                        newVx = -Math.Abs(vxt) * FACTOR_REBOTE;
                        newVy =  vyt * FACTOR_REBOTE;
                    }
                    else
                    {
                        picTejoF.Left = plat.Right;
                        newVx =  Math.Abs(vxt) * FACTOR_REBOTE;
                        newVy =  vyt * FACTOR_REBOTE;
                    }
                    yFisicoRebote = yt;
                    x0 = xt;
                    y0 = yt;
                }

                numRebotes++;
                cooldownPlataforma = COOLDOWN_PLATAFORMA_TICKS;
                cooldownSuelo      = 0;

                double vRebote = Math.Sqrt(newVx * newVx + newVy * newVy);
                double angRebote = Math.Atan2(newVy, newVx) * 180.0 / Math.PI;

                GuardarRebote(tAcumulado, xt, yFisicoRebote, newVx, newVy, vRebote, angRebote);
                formCalculos.ImprimirRebote(numRebotes, tAcumulado, xt, yFisicoRebote,
                                             newVx, newVy, vRebote, angRebote);
                v0x = newVx;
                v0y = newVy;
                t   = 0;

                tAcumulado += 0.05;
                return;
            }

            // ── SUELO → rebotar o detener ────────────────────────────────
            if ( picTejoF.Bottom >= picSueloF.Top && cooldownSuelo == 0 )
            {
                if ( numRebotes < MAX_REBOTES )
                {
                    picTejoF.Top = picSueloF.Top - picTejoF.Height;
                    double yFisicoSuelo = initialTejoY - picTejoF.Top;

                    double newVx = Math.Abs(vxt) * FACTOR_REBOTE;
                    double newVy = Math.Abs(vyt) * FACTOR_REBOTE;
                    double vRebote = Math.Sqrt(newVx * newVx + newVy * newVy);
                    double angRebote = Math.Atan2(newVy, newVx) * 180.0 / Math.PI;

                    numRebotes++;
                    cooldownSuelo = COOLDOWN_SUELO_TICKS;

                    GuardarRebote(tAcumulado, xt, yFisicoSuelo, newVx, newVy, vRebote, angRebote);
                    formCalculos.ImprimirRebote(numRebotes, tAcumulado, xt, yFisicoSuelo,
                                                 newVx, newVy, vRebote, angRebote);
                    v0x = newVx;
                    v0y = newVy;
                    x0  = xt;
                    y0  = yFisicoSuelo;
                    t   = 0;
                }
                else
                {
                    Detener("Baby Rosi cayó al suelo :C", "Fin", MessageBoxIcon.Information);
                }

                tAcumulado += 0.05;
                return;
            }

            // ── Límite superior ──────────────────────────────────────────
            if ( picTejoF.Top <= 0 )
            {
                Detener("Baby Rosi salió por el techo", "Fin", MessageBoxIcon.Information);
                return;
            }

            // ── Límite izquierdo ─────────────────────────────────────────
            if ( picTejoF.Left <= 0 )
            {
                Detener("Baby Rosi salió por la izquierda", "Fin", MessageBoxIcon.Information);
                return;
            }

            // ── Límite derecho ───────────────────────────────────────────
            if ( picTejoF.Right >= this.ClientSize.Width )
            {
                Detener("Baby Rosi salió por la derecha", "Fin", MessageBoxIcon.Information);
                return;
            }

            t          += 0.05;
            tAcumulado += 0.05;
        }

        // ════════════════════════════════════════════════════════════════
        //  HELPERS
        // ════════════════════════════════════════════════════════════════
        private void Detener(string mensaje, string titulo, MessageBoxIcon icono)
        {
            timer1.Enabled      = false;
            enMovimiento        = false;
            simulacionTerminada = true;

            formCalculos.FinalizarTabla(numRebotes);

            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, icono);
        }

        private void GuardarRebote(double tAcum, double x, double y,
                                    double vx, double vy, double v, double ang)
        {
            reboteT.Add(Math.Round(tAcum, 2));
            reboteX.Add(Math.Round(x, 2));
            reboteY.Add(Math.Round(y, 2));
            reboteVx.Add(Math.Round(vx, 2));
            reboteVy.Add(Math.Round(vy, 2));
            reboteV.Add(Math.Round(v, 2));
            reboteAng.Add(Math.Round(ang, 2));
        }

        private void LimpiarDatos()
        {
            listT.Clear(); listX.Clear(); listY.Clear();
            listVx.Clear(); listVy.Clear(); listV.Clear();
            listAng.Clear();

            reboteT.Clear(); reboteX.Clear(); reboteY.Clear();
            reboteVx.Clear(); reboteVy.Clear(); reboteV.Clear();
            reboteAng.Clear();
        }
    }
}