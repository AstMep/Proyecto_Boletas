using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Boletas
{
    public class Panelito : Panel
    {
        public int BorderRadius { get; set; } = 20; // Radio del borde redondeado

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            GraphicsPath path = GetRoundedPath(rect, BorderRadius);
            this.Region = new Region(path);

            using (Pen pen = new Pen(Color.Transparent, 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(rect.Location, size);

            // Esquinas redondeadas
            path.AddArc(arc, 180, 90); // superior izquierda
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90); // superior derecha
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);   // inferior derecha
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);  // inferior izquierda
            path.CloseFigure();

            return path;
        }

    }
}
