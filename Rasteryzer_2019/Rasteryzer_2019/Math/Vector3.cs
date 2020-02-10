using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rasteryzer_2019
{
    public class Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3()
        {

        }

        public Vector3(Vector4 v)
        {
            this.X = v.X;
            this.Y = v.Y;
            this.Z = v.Z;
        }

        public void showVector()
        {
            Console.WriteLine("\n(" + X +" ; " + Y + " ; " + Z +")");
        }

        public float[] Vector3ToTable()
        {
            float[] vec= new float[3];
            vec[0] = X; 
            vec[1] = Y;
            vec[2] = Z;
            return vec;
        }

        #region Przeciązanie operatorów

        public override string ToString()
        {
            return "(" + X + " ; " + Y + " ; " + Z + ")";
        }

        //suma dwoch wektorow
        public static Vector3 operator+(Vector3 v, Vector3 v2)
        {
            Vector3 vect; 
            return vect = new Vector3(v.X + v2.X, v.Y + v2.Y, v.Z + v2.Z);
        }

        public static Vector3 operator/(Vector3 v, float scalar)
        {
            Vector3 vect;
            return vect = new Vector3(v.X/scalar, v.Y / scalar, v.Z / scalar);
        }

        //roznica dwoch wektorow
        public static Vector3 operator -(Vector3 v, Vector3 v2)
        {
            Vector3 vect;
            return vect = new Vector3(v.X - v2.X, v.Y - v2.Y, v.Z - v2.Z);
        }

        //przemnozenie wartosci wektora przez skalar
        public static Vector3 operator *(Vector3 v, float scalar)
        {
            Vector3 vect;
            return vect = new Vector3(v.X * scalar, v.Y * scalar, v.Z * scalar);
        }

        //czy dwa wektory rozne
        public static bool operator !=(Vector3 v, Vector3 v2)
        {
            return (v.X != v2.X || v.Y != v2.Y || v.Z != v2.Z);
        }

        //czy dwa wektory rowne
        public static bool operator ==(Vector3 v, Vector3 v2)
        {
            return (v.X == v2.X && v.Y == v2.Y && v.Z == v2.Z);
        }

        #endregion

        #region Operacje na wektorach
        public void AddVector(Vector3 v)
        {
            this.X += v.X;
            this.Y += v.Y;
            this.Z += v.Z;
        }

        public Vector3 AddV(Vector3 v)
        {
            Vector3 vect = new Vector3(X + v.X, Y + v.Y, Z + v.Z);
            return vect;
        }

        //przemnozenie wartosci wektora przez skalar
        void MultiplyVector(float scalar)
        {
            this.X *= scalar;
            this.Y *= scalar;
            this.Z *= scalar;
        }

        public Vector3 MultV(float scalar)
        {
            Vector3 vect = new Vector3(X * scalar, Y * scalar, Z * scalar);
            return vect;
        }

        //iloczyn wektorowy
        public Vector3 Cross(Vector3 v)
        {
            Vector3 vect = new Vector3
                (this.Y * v.Z - this.Z * v.Y,
                this.Z * v.X - this.X * v.Z,
                this.X * v.Y - this.Y * v.X);
            return vect;
        }

        //iloczyn skalarny dwoch wektorow np. do obliczenia cosinusa 
        public float Dot(Vector3 v)
        {
            return this.X * v.X + this.Y * v.Y + this.Z * v.Z;
        }

        public float SaturateScalar(float x)
        {
            return Math.Max(0, Math.Min(1, x));
        }

        public void SaturateVector()
        {
            X = Math.Max(0, Math.Min(1, X));
            Y = Math.Max(0, Math.Min(1, Y));
            Z = Math.Max(0, Math.Min(1, Z));
        }

        //Returns the reflectiton vector given an incidence vector i and a normal vector n. The resulting vector is the identical number of components as the two input vectors.
        //The normal vector n should be normalized. If n is normalized, the output vector will have the same length as the input incidence vector i.

        public Vector3 Reflect(Vector3 I, Vector3 N) //i - promień przychodzący, n - odbity
        {
            N = N.Normalize();
            return I - (N * 2.0f) * I.Dot(N) ;
            //I - 2.0 * dot(N, I) * N
            //return i - (n * 2.0f) * i.Dot(n);
        }

        //długość wektora
        public float GetLength()
        {
            return (float)Math.Sqrt(Dot(this));
        }

        const float eps = 0.0001f;
        //normalizacja wektora
        public Vector3 Normalize()
        {
            Vector3 v;
            float len = this.GetLength();
            if (len > eps)
            {
                return (this) * (1 / len);
            }
            else
            {
                return v = new Vector3(.0f, .0f, .0f);
            }
        }

        public void Negative()
        {
            this.X = -X;
            this.Y = -Y;
            this.Z = -Z;
        }

        public Vector3 NegZ()
        {
            Vector3 vect = new Vector3(X, Y, Z);
            return vect;
        }

        public Vector3 Neg()
        {
            Vector3 vect = new Vector3(-X, -Y, -Z);
            return vect;
        }

        #endregion

    }
}
