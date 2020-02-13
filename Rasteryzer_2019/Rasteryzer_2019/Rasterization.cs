using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    public class Rasterization
    {
        Buffer buff;

        public Rasterization(Buffer Buff)
        {
            this.buff = Buff;
        }

        private int maxCol(int c)
        {
            if (c > 255) return 255;
            if (c < 0) return 0;

            return c;
        }


        public void Triangle(Vector3 pos1, Vector3 pos2, Vector3 pos3, Vertex v1, Vertex v2, Vertex v3, Light l, VertexProcessor vert)
        {
            float p1x = (pos1.X + 1) * buff.colorBuffer.Width * 0.5f;
            float p1y = (pos1.Y + 1) * buff.colorBuffer.Height * 0.5f;
            
            float p2x = (pos2.X + 1) * buff.colorBuffer.Width * 0.5f;
            float p2y = (pos2.Y + 1) * buff.colorBuffer.Height * 0.5f;

            float p3x = (pos3.X+1) * buff.colorBuffer.Width * 0.5f ;
            float p3y = (pos3.Y+1) * buff.colorBuffer.Height * 0.5f;

            int minx = (int)Math.Min(p1x,Math.Min(p2x,p3x)); 
            int miny = (int)Math.Min(p1y, Math.Min(p2y, p3y));
            int maxx = (int)Math.Max(p1x, Math.Max(p2x, p3x));
            int maxy = (int)Math.Max(p1y, Math.Max(p2y, p3y));

            minx = Math.Max(minx,0);
            maxx = Math.Min(maxx, buff.colorBuffer.Width - 1);
            miny = Math.Max(miny,0);
            maxy = Math.Min(maxy, buff.colorBuffer.Height - 1);

            bool tl1, tl2, tl3;
            tl1 = tl2 = tl3 = false;

            float dy12 = p1y - p2y;
            float dy23 = p2y - p3y;
            float dy31 = p3y - p1y;

            float dx12 = p1x - p2x;
            float dx23 = p2x - p3x;
            float dx31 = p3x - p1x;

            if (dy12 < 0 || (dy12 == 0 && dx12 > 0)) tl1 = true;
            if (dy23 < 0 || (dy23 == 0 && dx23 > 0)) tl2 = true;
            if (dy31 < 0 || (dy31 == 0 && dx31 > 0)) tl3 = true;
            
                for (int x = minx; x <= maxx; x++)
                    for (int y = miny; y <= maxy; y++)
                    {
                        if ((((p1x - p2x) * (y - p1y) - (p1y - p2y) * (x - p1x) > 0 && !tl1) || ((p1x - p2x) * (y - p1y) - (p1y - p2y) * (x - p1x) >= 0 && tl1))
                        &&
                            (((p2x - p3x) * (y - p2y) - (p2y - p3y) * (x - p2x) > 0 && !tl2) || ((p2x - p3x) * (y - p2y) - (p2y - p3y) * (x - p2x) >= 0 && tl2))
                        &&
                            (((p3x - p1x) * (y - p3y) - (p3y - p1y) * (x - p3x) > 0 && !tl3) || ((p3x - p1x) * (y - p3y) - (p3y - p1y) * (x - p3x) >= 0 && tl3)))
                        {
                            //WSPOLRZEDNE BARYCENTRYCZNE
                            float lambda1 = (((p2y - p3y) * (x - p3x)) + ((p3x - p2x) * (y - p3y))) /
                                        (((p2y - p3y) * (p1x - p3x)) + ((p3x - p2x) * (p1y - p3y)));

                            float lambda2 = (((p3y - p1y) * (x - p3x)) + ((p1x - p3x) * (y - p3y))) /
                                            (((p3y - p1y) * (p2x - p3x)) + ((p1x - p3x) * (p2y - p3y)));
                            float lambda3 = 1 - lambda1 - lambda2;

                            //bufor głębokości
                            float depth = lambda1 * pos1.Z + lambda2 * pos2.Z + lambda3 * pos3.Z;

                            if (depth < buff.depthBuffer[x, y])
                            {
                                    //OSWIETLENIE PER VERTEX
                                    float colorvalue = lambda1 * pos1.X + lambda2 * pos2.Y + lambda3 * pos3.Z;
                                    Vector3 ambient = new Vector3(50,0,0);
                            //Vector3 normal = (v1.normal + v2.normal + v3.normal) / 3;
                            //normal.Normalize();

                            //v1.light *= lambda1;
                            //v2.light *= lambda2;
                            //v3.light *= lambda3;

                            Vector3 color = (v1.light * lambda1) + (v2.light * lambda2) + (v3.light * lambda3);

                            //--Model oświetlenia
                                    //Vector3 light = l.Calculate(vert, normal);
                                    //Vector3 light = l.GoochShading(vert, normal);
                                    //Vector3 light = l.CelShading(vert, normal);
                            Vector3 light = l.GoraudShading(vert, v1, v2, v3);

                            //Color col = Color.FromArgb(maxCol((int)((ambient.X + light.X) * (1))), maxCol((int)((ambient.Y + (int)light.Y) * (1))), maxCol((int)((ambient.Z + (int)light.Z) * (1))));

                            //OKRESLANIE KOLORU NA PODSTAWIE BARYCENTRYCZNYCH
                            //Color col = Color.FromArgb(maxCol((int)(255 * lambda1)), maxCol((int)(255 * lambda2)), maxCol((int)(255 * lambda3)));
                            Color col = Color.FromArgb(maxCol((int)(color.X + ambient.X)), maxCol((int)(color.Y + ambient.Y)), maxCol((int)(color.Z + ambient.Z)));
                            buff.depthBuffer[x, y] = depth;
                                    buff.colorBuffer.SetPixel(x, y, col);
                            }
                        }
                    }
            
        }
    }
}
