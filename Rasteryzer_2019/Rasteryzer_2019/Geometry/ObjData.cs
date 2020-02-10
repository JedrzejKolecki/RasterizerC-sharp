using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019.Geometry
{
    public class ObjData
    {
        struct MeshData
        {
            List<Vector3> verticesPositions;
            List<Vector3> normalVectors;
        }

        struct TriangleData
        {
            int[] verticesIndex;
            int[] normalIndex;
        }

        
        //int textureIndex = 0;
        //bool hasTexture = false;
        //bool clockwise = false;
    }
}
