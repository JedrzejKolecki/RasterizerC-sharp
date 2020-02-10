using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019.Lights
{
    class DirectionalLight : Light
    {
        Vector3 position;
        Vector3 diffuse;
        Vector3 specular;
        float shininess;

        public DirectionalLight(Vector3 pos)
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
            shininess = 2f;

            Vector3 diffuseColor = new Vector3(0, 0, 200);
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

        //Celshader

        public Vector3 CelShading(VertexProcessor vert, Vector3 normal)
        {
            //Console.WriteLine("CELSHADING");
            shininess = 2f;
            int _CelTone = 10;

            Vector3 diffuseColor = new Vector3(0, 0, 200);
            Vector3 specularColor = new Vector3(10, 10, 10);

            Vector3 N = vert.tr(normal).Normalize();
            Vector3 V = vert.tr(position).Normalize();
            Vector3 L = (V - position).Normalize();
            Vector3 R = L.Reflect(L, N);

            float diffuseValue = Math.Max(0, L.Dot(N));
            double celShading = Math.Floor((diffuseValue * _CelTone) / (_CelTone - 0.5));
            float specularValue = (float)Math.Pow(R.Dot(V), shininess);

            diffuseColor *= diffuseValue;
            diffuseColor *= (float)celShading;
            specularColor *= specularValue;

            Vector3 col = diffuseColor+ specularColor;// + specular;// + (specular*255);
            return col;
        }

        //shader Gooch

        public Vector3 GoochShading(VertexProcessor vert, Vector3 normal)
        {
            shininess = 2f;
            float alfa = 0.5f;
            float beta = 0.25f;

            Vector3 coolColor = new Vector3(0, 0, 255);
            Vector3 warmColor = new Vector3(255, 0, 0);

            Vector3 diffuseColor = new Vector3(0, 0, 0);
            Vector3 specularColor = new Vector3(50, 50, 50);

            Vector3 N = vert.tr(normal).Normalize();
            Vector3 V = vert.tr(position).Normalize();
            Vector3 L = (V - position).Normalize();
            Vector3 R = L.Reflect(L, N);

            float diffuseValue = Math.Max(0, L.Dot(N));
            float wsp = 0.5f * (1.0f + diffuseValue);

            float specularValue = (float)Math.Pow(R.Dot(V), shininess);

            diffuseColor *= diffuseValue;
            specularColor *= specularValue;

            coolColor = coolColor + diffuseColor * alfa;
            warmColor = warmColor + diffuseColor * beta;

            Vector3 gooch = (warmColor * (1.0f - wsp)) + (coolColor * wsp) + specularColor;
            return gooch;
        }
    }
}
