using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    //bufor - rozmiar, color, glebia
    public class Buffer
    {
        public Bitmap colorBuffer;
        public float[,] depthBuffer;

        //konstruktor domyślny - wypełnia cały obraz na biało
        public Buffer()
        {
            colorBuffer = new Bitmap(500, 500);
            FillColor(Color.White);
            depthBuffer = new float[500,500];
        }

        //konstruktor - utworz nowy obraz o podanych rozmiarach i kolorze tła
        public Buffer(int size_X, int size_Y, Color col)
        {
            colorBuffer = new Bitmap(size_X, size_Y);
            FillColor(col);
            depthBuffer = new float[size_X, size_Y];
            FillDepth();
        }
        
        //wypełnij tło
        private void FillColor(Color col)
        {
            for (int i=0; i<colorBuffer.Width; i++)
                for (int j=0; j<colorBuffer.Height; j++)
                {
                    colorBuffer.SetPixel(i, j, col);
                }
        }

        public void ClearColor()
        {
            for (int i = 0; i < colorBuffer.Width; i++)
                for (int j = 0; j < colorBuffer.Height; j++)
                {
                    colorBuffer.SetPixel(i, j, Color.White);
                }
        }

        //wypelnij bufor glebi
        private void FillDepth()
        {
            for (int i = 0; i < depthBuffer.GetLength(0); i++)
                for (int j = 0; j < depthBuffer.GetLength(1); j++)
                {
                    depthBuffer[i, j] = 100; //jaka powinna być wartość początkowa?
                }
        }

        public void ClearDepth()
        {
            for (int i = 0; i < depthBuffer.GetLength(0); i++)
                for (int j = 0; j < depthBuffer.GetLength(1); j++)
                {
                    depthBuffer[i, j] = 0.0f;
                }
        }

        //zapis obrazu
        public void SaveImage()
        {
            //Filter(3);
            colorBuffer.Save("imageRasterizer.bmp", ImageFormat.Bmp);
        }

        public void Filter(int size)
        {
            int[,] mask = new int[size, size];
            Color[,] finalImage = new Color[colorBuffer.Width, colorBuffer.Height];
            List<Color> list = new List<Color>();
            Color median;

            for (int i = 0; i <= finalImage.GetLength(0) - mask.GetLength(0); i++)
            {
                for (int j = 0; j <= finalImage.GetLength(1) - mask.GetLength(1); j++)
                {
                    for (int x = i; x <= (mask.GetLength(0) - 1) + i; x++)
                    {
                        for (int y = j; y <= (mask.GetLength(1) - 1) + j; y++)
                        {
                            list.Add(colorBuffer.GetPixel(x,y));
                        }
                    }

                    median = Median(list); //dla mediany
                    list.Clear();

                    finalImage[i, j] = median;

                    for (int x =0; x< colorBuffer.Width; x++)
                        for (int y = 0; y< colorBuffer.Height; y++)
                        {
                            colorBuffer.SetPixel(x, y, finalImage[x, y]);
                        }
                }
            }
        }

        private Color Median(List<Color> list)
        {
            //najpierw sortuj liste
            List<int> red = new List<int>();
            List<int> green = new List<int>();
            List<int> blue = new List<int>();

            int redMedian;
            int greenMedian;
            int blueMedian;

            //tworzenie list wszystkich wartosci poszczegolnych kanalow
            foreach (Color c in list)
            {
                red.Add(c.R);
                green.Add(c.G);
                blue.Add(c.B);
            }

            //sortowanie wszystkich wartosci w kanalach
            red.Sort();
            green.Sort();
            blue.Sort();

            //wyznaczenie elementu srodkowego
            redMedian = red.ElementAt(red.Count / 2);
            greenMedian = green.ElementAt(red.Count / 2);
            blueMedian = blue.ElementAt(red.Count / 2);

            Color col = Color.FromArgb(redMedian, greenMedian, blueMedian);

            return col;
        }


    }
}
