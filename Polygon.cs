using System.Drawing;
using System.Drawing.Drawing2D;
using System.Numerics;

namespace cube3d
{
    class Polygon
    {
        Vector3[] vertex;
        public Brush br;
        public float scale = 200;
        public Vector3 vertex0;
        public Vector3 vertex1;
        public Vector3 vertex2;
        public Vector3 vertex3;
        public float distance;
        public Vector3 midleP;
        public float rotX = 0;
        public float rotY = 0;
        public float rotZ = 0;
        public Vector3 position = new Vector3(0, 0, 0);
        public bool canBefill = false;
        public Vector3[] CreatePolygon(float x, float y, float z, Vector3 pos,
            Vector3 vx1, Vector3 vx2, Vector3 vx3, Vector3 vx4, Brush br)
        {
            this.br = br;
            rotX = x;
            rotY = y;
            rotZ = z;
            position = pos;

            vertex0 = vx1;
            vertex1 = vx2;
            vertex2 = vx3;
            vertex3 = vx4;
            vertex = new Vector3[4] { vertex0, vertex1, vertex2, vertex3 };

            var scaleM = Matrix4x4.CreateScale(scale / 2);
            var rotateM = Matrix4x4.CreateFromYawPitchRoll(rotX, rotY, rotZ);
            var translateM = Matrix4x4.CreateTranslation(position);
            var m = translateM * rotateM * scaleM;
            for (int i = 0; i < vertex.Length; i++)
                vertex[i] = Vector3.Transform(vertex[i], m);
            midleP = (vertex[0] + vertex[1] + vertex[2] + vertex[3]) / 4;
            return vertex;
        }

        private void Draw(Graphics gr, PointF startPoint, Matrix4x4 projectionMatrix)
        {
            var p = new Vector3[vertex.Length];
            for (int i = 0; i < vertex.Length; i++)
                p[i] = Vector3.Transform(vertex[i], projectionMatrix);
            var path = new GraphicsPath();
            AddLine(path, p[0], p[1]);
            AddLine(path, p[1], p[2]);
            AddLine(path, p[2], p[3]);
            AddLine(path, p[3], p[0]);

            gr.ResetTransform();
            gr.TranslateTransform(startPoint.X, startPoint.Y);

            gr.FillPath(br, path);
            gr.DrawPath(new Pen(Color.Black,2), path); ;
        }
        public void Print(Graphics gr)
        {
            Matrix4x4 transferXY = new Matrix4x4();
            transferXY.M11 = 1f;
            transferXY.M22 = 1f;
            transferXY.M33 = 1f;
            transferXY.M44 = 1f;
            Draw(gr, new PointF(500 / 2, 500 / 2), transferXY);
        }
        void AddLine(GraphicsPath path, params Vector3[] points)
        {
            foreach (var p in points)
                path.AddLines(new PointF[] { new PointF(p.X, p.Y) });
        }
        public void PolyControl(float x, float y, float z, Vector3 pos, Graphics g, Brush b)
        {
            vertex = CreatePolygon(x, y, z, pos, vertex0, vertex1, vertex2, vertex3, b);
        }

    }
}
