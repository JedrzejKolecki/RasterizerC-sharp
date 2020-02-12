using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    public class Vertex
    {
        public Vector3 position;
        public Vector3 normal;
        public int index;
        public Vector3 light;

        public Vertex(Vector3 pos)
        {
            normal = new Vector3(0,0,0);
            position = pos;
        }

    }
}
