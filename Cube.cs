using System;
using System.Drawing;
using System.Numerics;

namespace cube3d
{
    class Cube
    {
        public Polygon[] cube = new Polygon[6];
        public Vector3 Vertex0 = new Vector3(0, 0, 0);
        public Vector3 Vertex1 = new Vector3(0, 1, 0);
        public Vector3 Vertex2 = new Vector3(1, 1, 0);
        public Vector3 Vertex3 = new Vector3(1, 0, 0);
        public Vector3 Vertex4 = new Vector3(0, 0, 1);
        public Vector3 Vertex5 = new Vector3(0, 1, 1);
        public Vector3 Vertex6 = new Vector3(1, 1, 1);
        public Vector3 Vertex7 = new Vector3(1, 0, 1);
        public Vector3 pos;
        public Cube(Graphics g, Vector3 position)
        {
            pos = position;
            for (int i = 0; i < cube.Length; i++)
                cube[i] = new Polygon();
            cube[0].CreatePolygon(0, 0, 0, pos,
    Vertex0, Vertex1, Vertex2, Vertex3, Brushes.HotPink);

            cube[1].CreatePolygon(0, 0, 0, pos,
    Vertex4, Vertex5, Vertex6, Vertex7, Brushes.Orange);

            cube[2].CreatePolygon(0, 0, 0, pos,
    Vertex0, Vertex4, Vertex5, Vertex1, Brushes.Green);

            cube[3].CreatePolygon(0, 0, 0, pos,
    Vertex3, Vertex7, Vertex6, Vertex2, Brushes.Purple);

            cube[4].CreatePolygon(0, 0, 0, pos,
    Vertex0, Vertex4, Vertex7, Vertex3, Brushes.Yellow);

            cube[5].CreatePolygon(0, 0, 0, pos,
    Vertex1, Vertex5, Vertex6, Vertex2, Brushes.Blue);
        }
       
        public void RotateCube(float rotY, float rotZ, Graphics g)
        {
                for (int i = 0; i < cube.Length; i++)
                cube[i].PolyControl(0, rotY, rotZ, cube[i].position, g, cube[i].br);
        }
        public void TranslateCube(float posX, float PosY, Graphics g)
        {
            for (int i = 0; i < cube.Length; i++)
                cube[i].PolyControl(cube[i].rotX, cube[i].rotY, cube[i].rotZ,
                    new Vector3(posX, PosY, cube[i].position.Z), g, cube[i].br);
        }
    }
}
