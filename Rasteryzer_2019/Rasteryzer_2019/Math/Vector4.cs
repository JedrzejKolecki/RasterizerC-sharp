using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    public class Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;
        public float[] vectorTable; //zmieni sie tylko jak zwracany jest Vector4 - w konstruktorze

        public Vector4(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
            vectorTable = new float[4] { x,y,z,w };
        }

        public Vector4(Vector3 v)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z;
            this.W = 1.0f;
            vectorTable = new float[4] { v.X, v.Y, v.Z, 1.0f };
        }

        public Vector4()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
            this.W = 0;
        }

        public void showVector()
        {
            Console.WriteLine("Długosc wektora4: " + vectorTable.Length);
            Console.WriteLine("\nWektor4: " + "(x" + X + "\ty" + Y + "\tz" + Z + "\tw" + W + " )");
        }

        public float[] Vector4ToTable()
        {
            float[] vec = new float[4];
            vec[0] = X;
            vec[1] = Y;
            vec[2] = Z;
            vec[3] = W;
            return vec;
        }

        #region Przeciązanie operatorów

        //suma dwoch wektorow
        public static Vector4 operator +(Vector4 v, Vector4 v2)
        {
            Vector4 vect;
            return vect = new Vector4(v.X + v2.X, v.Y + v2.Y, v.Z + v2.Z, v.W + v2.W);
        }

        //roznica dwoch wektorow
        public static Vector4 operator -(Vector4 v, Vector4 v2)
        {
            Vector4 vect;
            return vect = new Vector4(v.X - v2.X, v.Y - v2.Y, v.Z - v2.Z, v.W - v2.W);
        }

        //przemnozenie wartosci wektora przez skalar
        public static Vector4 operator *(Vector4 v, float scalar)
        {
            Vector4 vect;
            return vect = new Vector4(v.X * scalar, v.Y * scalar, v.Z * scalar, v.W * scalar);
        }

        //czy dwa wektory rozne
        public static bool operator !=(Vector4 v, Vector4 v2)
        {
            return (v.X != v2.X || v.Y != v2.Y || v.Z != v2.Z || v.W != v2.W);
        }

        //czy dwa wektory rowne
        public static bool operator ==(Vector4 v, Vector4 v2)
        {
            return (v.X == v2.X && v.Y == v2.Y && v.Z == v2.Z && v.W == v2.W);
        }

        #endregion

        #region Operacje na wektorach
        void AddVector(Vector4 v)
        {
            this.X += v.X;
            this.Y += v.Y;
            this.Z += v.Z;
            this.Z += v.W;
        }

        Vector4 AddV(Vector4 v)
        {
            Vector4 vect = new Vector4(X + v.X, Y + v.Y, Z + v.Z, W + v.W);
            return vect;
        }

        //przemnozenie wartosci wektora przez skalar
        void MultiplyVector(float scalar)
        {
            this.X *= scalar;
            this.Y *= scalar;
            this.Z *= scalar;
            this.W *= scalar;
        }

        Vector4 MultV(float scalar)
        {
            Vector4 vect = new Vector4(X * scalar, Y * scalar, Z * scalar, W * scalar);
            return vect;
        }

        //iloczyn wektorowy
        Vector4 Cross(Vector4 v)
        {
            Vector4 vect = new Vector4
                (this.Y * v.Z - this.Z * v.Y,
                this.Z * v.X - this.X * v.Z,
                this.X * v.Y - this.Y * v.X,
                this.X * v.Y - this.Y * v.X);
            return vect;
        }

        //iloczyn skalarny dwoch wektorow np. do obliczenia cosinusa 
        float Dot(Vector4 v)
        {
            return this.X * v.X + this.Y * v.Y + this.Z * v.Z + this.W * v.W;
        }

        public void SaturateVector()
        {
            X = Math.Max(0, Math.Min(1, X));
            Y = Math.Max(0, Math.Min(1, Y));
            Z = Math.Max(0, Math.Min(1, Z));
            W = Math.Max(0, Math.Min(1, W));
        }

        //długość wektora - iloczyn skalarny sam z soba i pierwiastek
        public float GetLength()
        {
            return (float)Math.Sqrt(Dot(this));
        }

        const float eps = 0.0001f;
        //normalizacja wektora
        Vector4 Normalize()
        {
            Vector4 v; 
            float len = this.GetLength();
            if (len > eps)
            {
                return (this) * (1 / len);
            }
            else
            {
                return v = new Vector4(.0f, .0f, .0f,.0f);
            }
        }

        void Negative()
        {
            this.X = -X;
            this.Y = -Y;
            this.Z = -Z;
            this.W = -W;
        }

        Vector4 Neg()
        {
            Vector4 vect = new Vector4(-X, -Y, -Z, -W);
            return vect;
        }

        #endregion
    }
}
