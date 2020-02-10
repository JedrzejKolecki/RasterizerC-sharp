using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    //macierz 4x4
    public class Matrix4
    {
        //TO DO: Mnożenie macierzy 4x4 przez wektor 4-wymiarowy oraz przez 3 wymiarowy (po automatycznym uzupełnieniu go o w=1)

        public float[,] mat;

        public Matrix4()
        {
            mat = new float[4, 4];
            FillMatrix();
        }

        public Matrix4(Vector4 v1, Vector4 v2, Vector4 v3, Vector4 v4)
        {
            mat = new float[4, 4];

            mat[0, 0] = v1.X;
            mat[1, 0] = v1.Y;
            mat[2, 0] = v1.Z;
            mat[3, 0] = v1.W;

            mat[0, 1] = v2.X;
            mat[1, 1] = v2.Y;
            mat[2, 1] = v2.Z;
            mat[3, 1] = v2.W;

            mat[0, 2] = v3.X;
            mat[1, 2] = v3.Y;
            mat[2, 2] = v3.Z;
            mat[3, 2] = v3.W;

            mat[0, 3] = v4.X;
            mat[1, 3] = v4.Y;
            mat[2, 3] = v4.Z;
            mat[3, 3] = v4.W;

        }

        public Matrix4(float a1, float a2, float a3, float a4,
            float b1, float b2, float b3, float b4,
            float c1, float c2, float c3, float c4,
            float d1, float d2, float d3, float d4)
        {
            //a1 a2 a3 a4
            //b1 b2 b3 b4
            //c1 c2 c3 c4
            //d1 d2 d3 d4

            mat = new float[4, 4];

            mat[0, 0] = a1;
            mat[0, 1] = a2;
            mat[0, 2] = a3;
            mat[0, 3] = a4;

            mat[1, 0] = b1;
            mat[1, 1] = b2;
            mat[1, 2] = b3;
            mat[1, 3] = b4;

            mat[2, 0] = c1;
            mat[2, 1] = c2;
            mat[2, 2] = c3;
            mat[2, 3] = c4;

            mat[3, 0] = d1;
            mat[3, 1] = d2;
            mat[3, 2] = d3;
            mat[3, 3] = d4;
        }
        
        void FillMatrix()
        {
            for (int i=0; i<mat.GetLength(0); i++)
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    mat[i, j] = 0;
                }

            mat[0, 0] = 1;
            mat[1, 1] = 1;
            mat[2, 2] = 1;
            mat[3, 3] = 1;
        }

        public Vector4 MultiplyMatrixByVector(Vector4 vec)
        {
            //Vector4 newVec = new Vector4();
            float[] vector = new float[4];
            //mnozenie tylko jesli ilosc kolumn jednej macierzy jest rowna ilosci wierszy drugiej
            
            for (int i = 0; i < mat.GetLength(0); i++) //m,n
            {
                float a = 0;
                for (int j = 0; j < mat.GetLength(1); j++) //p,q
                {
                    a += vec.vectorTable[j] * mat[j, i];
                }
                vector[i] = a;
            }
            Vector4 newVec = new Vector4(vector[0], vector[1], vector[2], vector[3]);
            //newVec.showVector();
            return newVec;
        }

        public Matrix4 MultiplyMatrixByMatrix(Matrix4 matrix2)
        {
            //https://www.tutorialspoint.com/chash-program-to-multiply-two-matrices
            Matrix4 finalMat = new Matrix4();
            //mnozenie tylko jesli ilosc kolumn jednej macierzy jest rowna ilosci wierszy drugiej
            for (int i = 0; i < mat.GetLength(0); i++) //m,n
            {
                for (int j = 0; j < matrix2.mat.GetLength(1); j++) //p,q
                {
                    finalMat.mat[i, j] = 0;
                    for (int k = 0; k < mat.GetLength(1); k++)
                    {
                        finalMat.mat[i, j] += mat[i, k] * matrix2.mat[k, j];
                    }
                }
            }
            return finalMat;
        }

        public Matrix4 Transpose()
        {
            Matrix4 transposedMatrix = new Matrix4();

            for (int x=0; x<mat.GetLength(0); x++)
                for (int y=0; y<mat.GetLength(1); y++)
                {
                    transposedMatrix.mat[y, x] = this.mat[x, y];
                }

            return transposedMatrix;
        }

        public void ShowMatrix()
        {
            Console.WriteLine("\nMacierz: ");
            Console.WriteLine(mat[0, 0] + "\t" + mat[0, 1] + "\t" + mat[0, 2] + "\t" + mat[0, 3]);
            Console.WriteLine(mat[1, 0] + "\t" + mat[1, 1] + "\t" + mat[1, 2] + "\t" + mat[1, 3]);
            Console.WriteLine(mat[2, 0] + "\t" + mat[2, 1] + "\t" + mat[2, 2] + "\t" + mat[2, 3]);
            Console.WriteLine(mat[3, 0] + "\t" + mat[3, 1] + "\t" + mat[3, 2] + "\t" + mat[3, 3]);
        }
    }
}
