using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MockupDesign
{
    public partial class Form1 : Form
    {
        double radian = 0;

        float sx, sy;
        float ex, ey;

        bool isDragging = false;

        public Form1()
        {
            InitializeComponent();

            pictureBox1.Width = 200;
            pictureBox1.Height = 200;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Draw();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        void DrawCircle(Control ctrl, float x, float y, float r)
        {
            Graphics graphics = ctrl.CreateGraphics();

            int w = pictureBox1.Width;
            int h = pictureBox1.Height;

            Pen pen = new Pen(Color.Red, 2);

            float cx = w / 2;
            float cy = w / 2;

            graphics.DrawEllipse(pen, cx-r, cy-r, 2*r, 2*r);
        }

        void DrawLine(Control ctrl, float x1, float y1, float x2, float y2)
        {
            Graphics graphics = ctrl.CreateGraphics();

            int w = pictureBox1.Width;
            int h = pictureBox1.Height;

            Pen pen = new Pen(Color.Red, 2);

            float cx = w / 2;
            float cy = w / 2;

            x1 = cx + x1;
            y1 = cy - y1;

            x2 = cx + x2;
            y2 = cy - y2;

            graphics.DrawLine(pen, x1, y1, x2, y2);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Draw()
        {
            ClearSlate();           

            DrawCircle(pictureBox1, 0, 0, 25);
            DrawCircle(pictureBox1, 0, 0, 50);
            
            Point point = new Point(0, 0);

            Point newPoint = getPolarPoint(point, radian, 100);

            DrawLine(pictureBox1, point.X , point.Y, newPoint.X, newPoint.Y);
        }

        private void ClearSlate()
        {
            Graphics graphics = pictureBox1.CreateGraphics();
            graphics.FillRectangle(new SolidBrush(Color.Black), 0, 0, pictureBox1.Width , pictureBox1.Height);
        }

        private Point getPolarPoint(Point point, double radian, int r)
        {
            float x = (float)(point.X + r * Math.Cos(radian));
            float y = (float)(point.X + r * Math.Sin(radian));

            return new Point((int)x, (int)y);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Draw();
        }

        private void Form1_MouseHover(object sender, EventArgs e)
        {
            Draw();
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch
            {

            }            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;

            int w = pictureBox1.Width;
            int h = pictureBox1.Height;

            float cx = w / 2;
            float cy = w / 2;

            sx = 0;  
            sy = 0;  

            txtSX.Text = sx.ToString();
            txtSY.Text = sy.ToString();           
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if(isDragging == true)
            {

                int w = pictureBox1.Width;
                int h = pictureBox1.Height;

                float cx = w / 2;
                float cy = w / 2;

                ex =  e.X - cx;
                ey =  -1 * (e.Y - cy);

                txtEX.Text = ex.ToString();
                txtEY.Text = ey.ToString();

                float x1, y1, x2, y2;

                x1 = sx;
                y1 = sy;

                x2 = ex;
                y2 = ey;

                float deltaX = x2 - x1;
                float deltaY = y2 - y1;

                radian = Math.Atan2(deltaY, deltaX);

                double degree = (180.0 / Math.PI) * radian;

                textBox1.Text = degree.ToString();

                Draw();
            }            
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }
    }
}
