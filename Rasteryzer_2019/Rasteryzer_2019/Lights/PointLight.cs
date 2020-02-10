using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019.Lights
{
    class PointLight : Light
    {
        Vector3 position;
        Vector3 diffuse;
        Vector3 specular;
        float shininess;

        public PointLight(Vector3 pos)
        {
            this.position = pos;
        }

        public Vector3 Calculate(VertexProcessor vert, Vertex v)
        {
            shininess = 1f;
            Vector3 N = vert.tr(v.normal);
            Vector3 V = vert.tr(position);
            Vector3 L = (V - position).Normalize();
            Vector3 R = L.Reflect(L, N);
            float diffuseValue = Math.Max(0,L.Dot(N));
            float specularValue = (float)Math.Pow(R.Dot(V), shininess);
            diffuse = new Vector3(diffuseValue, diffuseValue, diffuseValue);
            specular = new Vector3(specularValue, specularValue, specularValue);
            Vector3 col = (diffuse * 255) + specular ;
            return col;
        }


        public Vector3 Calculate(VertexProcessor vert, Vector3 normal)
        {
            shininess = 5f;

            Vector3 diffuseColor = new Vector3(20, 10, 200);
            Vector3 specularColor = new Vector3(100, 100, 100);

            Vector3 N = vert.tr(normal).Normalize();
            Vector3 V = vert.tr(position).Normalize();
            Vector3 L = (V - position).Normalize();
            Vector3 R = L.Reflect(L, N);

            float diffuseValue = Math.Max(0, L.Dot(N));
            float specularValue = (float)Math.Pow(R.Dot(V), shininess);

            diffuseColor *= diffuseValue;
            specularColor *= specularValue;

            Vector3 col = diffuseColor + specularColor;// + specular;// + (specular*255);
            return col;
        }
    }
}
