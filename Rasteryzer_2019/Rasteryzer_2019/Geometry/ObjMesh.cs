using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019.Geometry
{
    public class ObjMesh : Mesh
    {
        public List<Vertex> vertexes;
        public List<int> indexes;
        public List<Vector3> norm;

        FileStream file;
        StreamWriter stream;

        public ObjMesh()
        {
            file = new FileStream("Vertexes.txt", FileMode.OpenOrCreate);
            stream = new StreamWriter(file);
            vertexes = new List<Vertex>();
            indexes = new List<int>();
            norm = new List<Vector3>();
        }

        public void Light(Light l, VertexProcessor processor)
        {
            foreach (Vertex v in vertexes)
            {
                v.light = l.Calculate(processor, v);
            }
        }

        public void DrawMesh(Rasterization rasterizer, VertexProcessor processor, Light l)
        {
            System.Console.WriteLine("Ilosc indeksow: " + indexes.Count);

            for (int i = 0; i < indexes.Count; i += 3)
            {
                rasterizer.Triangle(processor.tr(vertexes[indexes[i]].position), processor.tr(vertexes[indexes[i + 1]].position), processor.tr(vertexes[indexes[i + 2]].position), vertexes[indexes[i]], vertexes[indexes[i + 1]], vertexes[indexes[i+2]], l, processor);
                stream.WriteLine("Indeks: " + indexes[i] + " " + indexes[i + 1] + " " + indexes[i + 2]);
                stream.WriteLine("Wierzchołki: " + vertexes[indexes[i]].position.ToString() + " | " + vertexes[indexes[i+1]].position.ToString() + " | " + vertexes[indexes[i+2]].position.ToString());
            }
            stream.Close();
        }

        //dla liczenia na biezaco
        public void MakeNormals(int first, int second, int third)
        {
                Vector3 U = vertexes[indexes[second]].position - vertexes[indexes[first]].position;
                Vector3 V = vertexes[indexes[third]].position - vertexes[indexes[first]].position;

                Vector3 normal = U.Cross(V);
                normal = normal.Normalize(); 
                normal.showVector();
                vertexes[indexes[first]].normal = normal;
                vertexes[indexes[second]].normal = normal;
                vertexes[indexes[third]].normal = normal;

                stream.WriteLine("Normalne dla ścian: " + indexes[first] + " ; " + indexes[second] + " ; " + indexes[third] + "\t" + vertexes[indexes[first]].normal.ToString() + " | " + vertexes[indexes[second]].normal.ToString() + " | " + vertexes[indexes[third]].normal.ToString());
        }

        public void DrawMesh(Rasterization rasterizer, VertexProcessor processor)
        {
            throw new NotImplementedException();
        }

        //policzenie wszystkich od razu w Scenie - nadpisywanie prawdopodobnie
        public void MakeNormals()
        {
            Vector3 v = new Vector3(0, 0, 0);
            for (int i = 0; i < vertexes.Count; i++)
            {
                vertexes[i].normal = v;
            }

            for (int i = 0; i < indexes.Count; i += 3)
            {
                Vector3 U = vertexes[indexes[i + 1]].position - vertexes[indexes[i]].position;
                Vector3 V = vertexes[indexes[i + 2]].position - vertexes[indexes[i]].position;
                Vector3 normal = U.Cross(V);
                normal = normal.Normalize();
                vertexes[indexes[i]].normal += normal;
                vertexes[indexes[i+1]].normal += normal;
                vertexes[indexes[i+2]].normal += normal;
            }

            for (int i = 0; i < vertexes.Count; i++)
            {
                vertexes[i].normal.Normalize();
                stream.WriteLine("Normalne dla ścian: " + vertexes[i].normal.ToString());
            }
        }
    }
}
