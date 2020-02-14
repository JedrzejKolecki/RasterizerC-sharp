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
            colorBuffer.Save("finalRender.png", ImageFormat.Png);
        }
    }
}
