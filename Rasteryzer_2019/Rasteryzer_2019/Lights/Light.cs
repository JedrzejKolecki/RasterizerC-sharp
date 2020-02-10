using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    public interface Light
    {
        Vector3 Calculate(VertexProcessor vert, Vertex v);
        Vector3 Calculate(VertexProcessor vert, Vector3 normal);
    }
}
