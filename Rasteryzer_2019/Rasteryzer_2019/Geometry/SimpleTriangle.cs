using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019.Geometry
{
    public class SimpleTriangle : Mesh
    {
        int vSize;
        int tSize;
        Vertex[] vertices;
        Vector3[] indices; //indeksy


        public SimpleTriangle(Vertex point1, Vertex point2, Vertex point3)
        {
            vSize = 3;
            tSize = 1;
            vertices = new Vertex[3];
            indices = new Vector3[1];

            vertices[0] = point1; //czy minus przejdzie?
            vertices[1] = point2;
            vertices[2] = point3;
            indices[0] = new Vector3(0, 1, 2);
            //MakeNormals();
        }

        public void DrawMesh(Rasterization rasterizer, VertexProcessor processor)
        {
            //rasterizer.Triangle(processor.tr(vertices[0].position), processor.tr(vertices[1].position), processor.tr(vertices[2].position));
            //rasterizer.Triangle(vertices[0].position, vertices[1].position, vertices[2].position);
        }

        public void DrawMesh(Rasterization rasterizer, VertexProcessor processor, Light l)
        {
            throw new NotImplementedException();
        }

        public void Light(Light l, VertexProcessor v)
        {
            throw new NotImplementedException();
        }

        public void MakeNormals(int a, int b, int c) //czy to zadziała?
        {
            Vector3 U = vertices[1].position - vertices[0].position;
            Vector3 V = vertices[2].position - vertices[0].position;

            Vector3 normal = U.Cross(V);
            normal = normal.Normalize();
            
            //ustawienie normalnych w wierzcholkach
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].normal = normal;
            }
        }

        public void MakeNormals()
        {
            throw new NotImplementedException();
        }
    }
}

