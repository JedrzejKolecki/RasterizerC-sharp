using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    public class Fragment
    {
        public Color color;
        public Vertex v;

        public Fragment(Color c, Vertex vert)
        {
            this.color = c;
            this.v = vert;
        }
    }
}
