using ObjLoader.Loader.Data.Elements;
using ObjLoader.Loader.Loaders;
using Rasteryzer_2019.Geometry;
using Rasteryzer_2019.Lights;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Wstępne ładowanie
            Console.WriteLine("Rasteryzer - Jędrzej Kołecki - 228012\nStart...\n");
            Buffer buff = new Buffer(1500, 1500, Color.BlanchedAlmond);
            List<Mesh> meshList = new List<Mesh>();
            List<Light> lightList = new List<Light>();
            Rasterization render = new Rasterization(buff);
            VertexProcessor vertex = new VertexProcessor();
            var objLoaderFactory = new ObjLoaderFactory();
            var objLoader = objLoaderFactory.Create();
            #endregion

            FileStream sb = new FileStream("Faces.txt", FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(sb);

            #region METODA BEZPOŚREDNIA RYSOWANIA W BUFORZE
            Vector3 p1 = new Vector3(.8f, .5f, .3f);
            Vector3 p2 = new Vector3(.9f, .0f, .2f);
            Vector3 p3 = new Vector3(0.0f, -.5f, .1f);
            //
            Vector3 p4 = new Vector3(.6f, .5f, 0);
            Vector3 p5 = new Vector3(.1f, .0f, .4f);
            Vector3 p6 = new Vector3(-0.5f, .6f, .5f);

            Vector3 p0 = new Vector3(2f, 5f, 5f); //0f, .5f, 1f 0f, -0.5f, -5f 0f, 1f, 5f
            DirectionalLight light = new DirectionalLight(p0);
            //

            Vector3 p7 = new Vector3(-0.859375f, 0.382812f, -0.382812f);
            Vector3 p8 = new Vector3(-0.773438f, 0.265625f, -0.4375f);
            Vector3 p9 = new Vector3( 0.640625f, -0.00781f, -0.429688f);

            Vertex v1 = new Vertex(p1); Vertex v2 = new Vertex(p2); Vertex v3 = new Vertex(p3);
            Vertex v4 = new Vertex(p4); Vertex v5 = new Vertex(p5); Vertex v6 = new Vertex(p6);
            #endregion

            Vertex v7 = new Vertex(p7);
            Vertex v8 = new Vertex(p8);
            Vertex v9 = new Vertex(p9);

            #region Dodanie obiektów do sceny
            SimpleTriangle t1 = new SimpleTriangle(v1, v2, v3);
            SimpleTriangle t2 = new SimpleTriangle(v4, v5, v6);
            SimpleTriangle t3 = new SimpleTriangle(v7, v8, v9);
            ObjMesh obj = new ObjMesh();

            Console.WriteLine("Podaj nazwe pliku OBJ: ");
            string nameObj = Console.ReadLine();
            var fileStream = new FileStream(nameObj, FileMode.Open);//"finalTest.obj"dwie_kule.objspeheres4.objsphere.objnew FileStream("monkey.obj", FileMode.Open);
            var result = objLoader.Load(fileStream);

            List<Vector3> normals = new List<Vector3>();

            //ladowanie pozycji wierzcholkow
            foreach (ObjLoader.Loader.Data.VertexData.Vertex l in result.Vertices)
            {
                Vector3 pos = new Vector3(l.X, l.Y, l.Z);
                Vertex newVert = new Vertex(pos);
                obj.vertexes.Add(newVert);
            }

            foreach (ObjLoader.Loader.Data.VertexData.Normal l in result.Normals)
            {
                Vector3 norm = new Vector3(l.X, l.Y, l.Z);
                normals.Add(norm);
            }

            obj.norm = normals;

            Console.WriteLine("Ilosc werteksów: " + obj.vertexes.Count() + "\n");
            Console.WriteLine("Ilosc normalnych: " + obj.norm.Count() + "\n");
            List<int> indexes = new List<int>();
            
            foreach (ObjLoader.Loader.Data.Elements.Group n in result.Groups)
            {
                int a = 0;
                foreach (ObjLoader.Loader.Data.Elements.Face f in n.Faces)
                {
                    sw.WriteLine("Face: " + a);
                    for (int i=0; i<f._vertices.Count; i++)
                    {
                        indexes.Add(f._vertices[i].VertexIndex-1);
                        sw.Write((f._vertices[i].VertexIndex-1) + " ");
                    }
                    a++;
                    sw.WriteLine("\n");
                }
            }

            sw.Close();

            lightList.Add(light);
            obj.indexes = indexes;
            meshList.Add(obj);
            Scene s = new Scene(meshList, lightList, render, vertex);

            #endregion
            buff.SaveImage();

            Console.WriteLine("\nRasteryzacja zakończona sukcesem.");
            Console.ReadKey();
        }
    }
}
