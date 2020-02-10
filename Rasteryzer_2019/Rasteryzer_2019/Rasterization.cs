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
            //p1 trojkata
            float p1x = (pos1.X + 1) * buff.colorBuffer.Width * 0.5f;
            float p1y = (pos1.Y + 1) * buff.colorBuffer.Height * 0.5f;
            
            //p2 trojkata
            float p2x = (pos2.X + 1) * buff.colorBuffer.Width * 0.5f;
            float p2y = (pos2.Y + 1) * buff.colorBuffer.Height * 0.5f;

            //p3 trojkata
            float p3x = (pos3.X+1) * buff.colorBuffer.Width * 0.5f ;
            float p3y = (pos3.Y+1) * buff.colorBuffer.Height * 0.5f;

            //Console.WriteLine("PUNKTY: " + p1x + " " + p1y + " " + p2x + " " + " " + p2y + " " + p3x + " " + p3y);

            //min i max zeby nie sprawdzac calego obrazka a tylko prostokat gdzie jest

            int minx = (int)Math.Min(p1x,Math.Min(p2x,p3x)); 
            int miny = (int)Math.Min(p1y, Math.Min(p2y, p3y));
            int maxx = (int)Math.Max(p1x, Math.Max(p2x, p3x));
            int maxy = (int)Math.Max(p1y, Math.Max(p2y, p3y));

            //jesli trojkat wychodzi poza obraz to go nie sprawdzaj tam
            minx = Math.Max(minx,0);
            maxx = Math.Min(maxx, buff.colorBuffer.Width - 1);
            miny = Math.Max(miny,0);
            maxy = Math.Min(maxy, buff.colorBuffer.Height - 1);

            //Console.WriteLine("Min X: " + minx + " | Min Y: " + miny + " | Max X: " + maxx + " | Max Y: " + maxy + " ");

            bool tl1, tl2, tl3;
            tl1 = tl2 = tl3 = false;

            //topleft
            float dy12 = p1y - p2y;
            float dy23 = p2y - p3y;
            float dy31 = p3y - p1y;

            float dx12 = p1x - p2x;
            float dx23 = p2x - p3x;
            float dx31 = p3x - p1x;

            if (dy12 < 0 || (dy12 == 0 && dx12 > 0)) tl1 = true;
            if (dy23 < 0 || (dy23 == 0 && dx23 > 0)) tl2 = true;
            if (dy31 < 0 || (dy31 == 0 && dx31 > 0)) tl3 = true;

            //Console.WriteLine("TOP-LEFT: " + tl1.ToString() + " " + tl2.ToString() +" " +tl3.ToString());
            
                for (int x = minx; x <= maxx; x++)  //for (int x = 0; x < buff.colorBuffer.Width; x++)
                    for (int y = miny; y <= maxy; y++)   //for (int y=0; y< buff.colorBuffer.Height; y++)
                    {
                        //Console.WriteLine("x: " + x + " y: " + y);
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
                                    //float colorvalue = lambda1 * pos1.X + lambda2 * pos2.Y + lambda3 * pos3.Z;
                                    Vector3 ambient = new Vector3(50,0,0);
                                    Vector3 normal = (v1.normal + v2.normal + v3.normal) / 3;
                                    //Vector3 normal = (v2.normal - v1.normal).Cross(v3.normal - v1.normal); //normalna ściany
                                    normal.Normalize();

                                    //Vector3 light = l.Calculate(vert, normal);
                                    //Vector3 light = l.GoochShading(vert, normal);
                                    Vector3 light = l.CelShading(vert, normal);
                                    
                                    Color col = Color.FromArgb(maxCol((int)(ambient.X + light.X)), maxCol((int)(ambient.Y + (int)light.Y)), maxCol((int)(ambient.Z + (int)light.Z)));
                                    
                                    //OKRESLANIE KOLORU NA PODSTAWIE BARYCENTRYCZNYCH
                                    //
                                    //Color col = Color.FromArgb(maxCol((int)(colorvalue * 255)), maxCol((int)(colorvalue * 255)), maxCol((int)(colorvalue * 255)));
                                    //Color col = Color.Black;
                                    buff.depthBuffer[x, y] = depth;
                                    buff.colorBuffer.SetPixel(x, y, col);
                            }
                        }
                    }
            
        }
    }
}
