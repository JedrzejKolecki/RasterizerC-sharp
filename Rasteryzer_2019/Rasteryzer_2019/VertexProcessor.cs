using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    public class VertexProcessor
    {
        public Matrix4 view2proj; //
        public Matrix4 world2view; //
        public Matrix4 obj2world; //

        public Matrix4 obj2proj;
        public Matrix4 obj2view; 

        public VertexProcessor()
        {
            obj2world = new Matrix4();
            
            //skalowanie sceny
            Vector3 scale = new Vector3(1.8f, 1.8f, 1.8f);
            MultiplyByScale(scale);

            //obrot sceny
            Vector3 rotation = new Vector3(0, 0, 1);
            MultiplyByRotation(0, rotation);
            
            SetPerspective(100, 1.0f, 1.0f, 10000.0f); //SetPerspective(140, 1, 1, 5);

            //kamera
            Vector3 eye = new Vector3(.2f, 0f, 2f);  //(0, .6f, 1.0f) (0, -.2f, 2)
            Vector3 center = new Vector3(0, 0, 0); //(0, 0, 0)
            Vector3 up = new Vector3(0, 1, 0); //(0, 1, 0)
            SetLookAt(eye,center,up);

            obj2view = world2view.MultiplyMatrixByMatrix(obj2world);
            obj2proj = view2proj.MultiplyMatrixByMatrix(obj2view);
        }
        
        //tworzy macierz rzutowania view2proj
        public void SetPerspective(double fovy, float aspect, float near, float far) 
        {
            fovy *= Math.PI / 360;
            float f =  (float)(Math.Cos(fovy) / Math.Sin(fovy));

            Vector4 v1 = new Vector4(f / aspect, 0, 0, 0);
            Vector4 v2 = new Vector4(0, f, 0, 0);
            Vector4 v3 = new Vector4(0, 0, (far + near) / (near - far), -1);
            Vector4 v4 = new Vector4(0, 0, (2 * near * far) / (near * far), 0);

            view2proj = new Matrix4(v1,v2,v3,v4);
        }
        
        //tworzy macierz rzutowanie world2view
        public void SetLookAt(Vector3 eye, Vector3 center, Vector3 up) 
        {
            Vector3 f = center - eye;
            f = f.Normalize();
            up = up.Normalize();
            Vector3 s = f.Cross(up);
            Vector3 u = s.Cross(f);

            Vector4 v1 = new Vector4(s.X, u.X, -f.X, 0);
            Vector4 v2 = new Vector4(s.Y, u.Y, -f.Y, 0);
            Vector4 v3 = new Vector4(s.Z, u.Z, -f.Z, 0);
            Vector4 v4 = new Vector4(0,0,0,1);
            world2view = new Matrix4(v1,v2,v3,v4);
            MultiplyByTranslation2(eye);
        }

        //macierz translacji
        public void MultiplyByTranslation(Vector3 v)
        {
            Vector4 v1 = new Vector4(1, 0, 0, 0);
            Vector4 v2 = new Vector4(0, 1, 0, 0);
            Vector4 v3 = new Vector4(0, 0, 1, 0);
            Vector4 v4 = new Vector4(v.X, v.Y, v.Z, 1);

            Matrix4 m = new Matrix4(v1, v2, v3, v4);

            obj2world = obj2world.MultiplyMatrixByMatrix(m);
        }

        //macierz translacji
        public void MultiplyByTranslation2(Vector3 v)
        {
            Vector4 v1 = new Vector4(1, 0, 0, 0);
            Vector4 v2 = new Vector4(0, 1, 0, 0);
            Vector4 v3 = new Vector4(0, 0, 1, 0);
            Vector4 v4 = new Vector4(v.X, v.Y, v.Z, 1);

            Matrix4 m = new Matrix4(v1, v2, v3, v4);

            world2view = world2view.MultiplyMatrixByMatrix(m);
        }

        //macierz skalowania
        public void MultiplyByScale(Vector3 v)
        {
            Vector4 v1 = new Vector4(v.X, 0, 0, 0);
            Vector4 v2 = new Vector4(0, v.Y, 0, 0);
            Vector4 v3 = new Vector4(0, 0, v.Z, 0);
            Vector4 v4 = new Vector4(0, 0, 0, 1);

            Matrix4 m = new Matrix4(v1, v2, v3, v4);

            obj2world = obj2world.MultiplyMatrixByMatrix(m);
        }

        //macierz rotacji
        public void MultiplyByRotation(double a, Vector3 v)
        {
            float s = (float)Math.Sin((a * Math.PI) / 180);
            float c = (float)Math.Cos((a*Math.PI) / 180);
            v = v.Normalize();

            Vector4 v1 = new Vector4(v.X * v.X * (1-c) + c, v.Y * v.X * (1-c) + v.Z * s, v.X * v.Z * (1-c) - v.Y * s, 0);
            Vector4 v2 = new Vector4(v.X * v.Y * (1-c) - v.Z * s, v.Y * v.Y * (1-c) + c, v.Y * v.Z * (1-c) + v.X * s,0);
            Vector4 v3 = new Vector4(v.X * v.Z * (1-c) + v.Y * s, v.Y * v.Z * (1-c) - v.X * s, v.Z * v.Z * (1-c) + c,0);
            Vector4 v4 = new Vector4(0, 0, 0, 1);

            Matrix4 m = new Matrix4(v1,v2,v3,v4);
            obj2world = obj2world.MultiplyMatrixByMatrix(m);
        }

        //przemnozenie wierzcholka przez macierze
        public Vector3 tr(Vector3 vertexPosition)
        {
            Vector4 verPos = new Vector4(vertexPosition);
            //verPos = obj2view.MultiplyMatrixByVector(verPos);
            verPos = obj2proj.MultiplyMatrixByVector(verPos);
            Vector3 finalPos = new Vector3(verPos.X / verPos.W, verPos.Y / verPos.W, verPos.Z / verPos.W);
            return finalPos;
        }
    }
}