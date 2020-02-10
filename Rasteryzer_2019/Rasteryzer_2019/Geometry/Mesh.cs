using Rasteryzer_2019.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    public interface Mesh
    {
        void DrawMesh(Rasterization rasterizer, VertexProcessor processor);
        void DrawMesh(Rasterization rasterizer, VertexProcessor processor, Light l);
        void MakeNormals(int a, int b, int c);
        void MakeNormals();
        void Light(Light l, VertexProcessor v);
    }
}
