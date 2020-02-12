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
        float shininess;

        public DirectionalLight(Vector3 pos)
        {
            this.position = pos;
        }

        //dla Gorauda
        public Vector3 Calculate(VertexProcessor vert, Vertex v)
        {
            shininess = 2f;
            Vector3 diffuseColor = new Vector3(0, 0, 200);
            Vector3 specularColor = new Vector3(100, 100, 100);

            Vector3 N = vert.tr(v.normal).Normalize();
            Vector3 V = vert.tr(position).Normalize();
            Vector3 L = (V - position).Normalize();
            Vector3 R = L.Reflect(L, N);

            float diffuseValue = Math.Max(0.0f, L.Dot(N));
            float specularValue = (float)Math.Pow(R.Dot(V), shininess);

            diffuseColor *= diffuseValue;
            specularColor *= specularValue;

            Vector3 col = diffuseColor + specularColor;
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

            float diffuseValue = Math.Max(0.1f, L.Dot(N));
            float specularValue = (float)Math.Pow(R.Dot(V), shininess);

            //attentuation
            //diffuseValue = diffuseValue * (1.0f / (1.0f + (0.25f * L.GetLength() * L.GetLength())));

            diffuseColor *= diffuseValue;
            specularColor *= specularValue;

            Vector3 col = diffuseColor + specularColor;// + specular;// + (specular*255);
            return col;
        }

        //Celshader

        public Vector3 CelShading(VertexProcessor vert, Vector3 normal)
        {
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

            Vector3 col = diffuseColor + specularColor;
            return col;
        }

        

        public Vector3 GoraudShading(VertexProcessor vert, Vertex v1, Vertex v2, Vertex v3)
        {
            Vector3 col = Lerp(v1.light, v2.light, 0.5f);
            Vector3 col2 = Lerp(v1.light, v3.light, 0.5f);
            col = Lerp(col, col2, 0.5f);

            return col;
        }

        Vector3 Lerp(Vector3 first, Vector3 second, float by)
        {
            float retX = LerpValue(first.X, second.Y, by);
            float retY = LerpValue(first.Y, second.Y, by);
            float retZ = LerpValue(first.Z, second.Z, by);

            Vector3 col = new Vector3(retX, retY, retZ);
            return col;
        }

        float LerpValue(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
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
