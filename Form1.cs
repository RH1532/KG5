using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Windows.Forms;

namespace cube3d
{
    public partial class Form1 : Form
    {
        Bitmap bm = new Bitmap(500, 500);
        Cube cube1;
        Graphics g;
        bool created = false;
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }
        List<Polygon> polys = new List<Polygon>();
        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Gray);
            if (!created)
            {
                cube1 = new Cube(g, new Vector3(-.5f, -.5f, -.5f));
                created = true;
                Cube[] cubes = new Cube[1] { cube1 };
                for (int i = 0; i < cubes.Length; i++)
                {
                    for (int j = 0; j < cubes[i].cube.Length; j++)
                    {
                        polys.Add(cubes[i].cube[j]);
                    }
                }
            }
            if (created)
            {
                DepthCalcuate();
                for (int i = 0; i < polys.Count; i++)
                    polys[i].Print(g);
            }
            e.Graphics.DrawImage(bm, 0, 0);
        }
        public void DepthCalcuate()
        {
            for (int i = 0; i < polys.Count; i++)
                polys[i].distance = DistanceToDot(polys[i].midleP);
            bool swapped = true;
            while (swapped != false)
            {
                swapped = false;
                for (int i = 1; i < polys.Count; i++)
                    if (polys[i - 1].distance > polys[i].distance)
                    {
                        Polygon temp = polys[i - 1];
                        polys[i - 1] = polys[i];
                        polys[i] = temp;
                        swapped = true;
                    }
            }
        }
        public float DistanceToDot(Vector3 dot)
        {
            Vector3 t1 = new Vector3();
            t1.X = 0;
            t1.Y = 0;
            t1.Z = 1000;
            return (float)(Math.Sqrt(Math.Pow((t1.X - dot.X), 2) +
                Math.Pow((t1.Y - dot.Y), 2) +
                Math.Pow((t1.Z - dot.Z), 2)));
        }
        private void Form1_Load(object sender, EventArgs e) => g = Graphics.FromImage(bm);
        float roll = 0;
        float pitch = 0;
        float _roll = 0;
        float _pitch = 0;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            roll = (float)(trackBar1.Value * Math.PI / 180f)  + _roll;
            cube1.RotateCube(pitch, roll, g);
            pictureBox1.Invalidate();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            pitch = (float)(trackBar2.Value * Math.PI / 180f) * -1 + _pitch;
            cube1.RotateCube(pitch, roll, g);
            pictureBox1.Invalidate();
        }
    }
}