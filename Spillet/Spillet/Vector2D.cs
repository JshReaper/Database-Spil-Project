using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillet
{
    /// <summary>
    /// this class belongs to: Jacob Saaby Hansen
    /// </summary>
    internal class Vector2D
    {
        // Denne klasse er som sådan ikke lavet "kun" til dette projekt, men som en del af en størrer samling 
        //af funktioner som man kan gøre med vektorer herunder en som vi bruger (hat-vektore)
        private float x;
        private float y;
        public float X { get { return x; } set {x = value; } }
        public float Y { get { return y; } set { y = value; } }
        public Vector2D(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2D operator *(Vector2D v1, float f)
        {
            return new Vector2D(v1.X * f, v1.Y * f);
        }

        // Normalisere en vektor og giver den normaliserede vektor uden at overskrive den originale
        public Vector2D Normalized()
        {
            float length = (float)Math.Sqrt((this.X * this.X) + (this.Y * this.Y));

            float newX = this.X / length;
            float newY = this.Y / length;

            return new Vector2D(newX, newY);

        }

        // Giver en vinkel mellem 2 vektorer som tit bruges i matematik
        public float AngleToVector2D(Vector2D V1)
        {
            float length = (float)Math.Sqrt((this.X * this.X) + (this.Y * this.Y));
            float lengthV2 = (float)Math.Sqrt((V1.X * V1.X) + (V1.Y * V1.Y));
            float total = ((this.X * V1.X) + (this.Y * V1.Y)) / (length * lengthV2);
            float toReturn = (float)Math.Acos(total);
            toReturn = (float)(toReturn * (180 / Math.PI));
            return toReturn;

        }

        // Denne funktion giver distancen mellem 2 vektor punkter 
        public float Dist(Vector2D vec)
        {
            float length = (float)Math.Sqrt((this.X - vec.X) * (this.X - vec.X) + (this.Y - vec.Y) * (this.Y - vec.Y));

            return length;
        }

        // retunere skalar produktet af en vektors koordinater
        public float Scalar()
        {
            float scalar = this.X * this.X + this.Y * this.Y;
            return scalar;
        }
        // retunere skalar produktet af to vektorers koordinater
        public float Scalar(Vector2D vec)
        {
            float scalar = this.X * vec.X + this.Y * vec.Y;
            return scalar;
        }

        // retunere en nul vektor
        public Vector2D Zero()
        {
            return new Vector2D(0, 0);
        }
       
        // Hatter en vektor (drejer den 90 grader med uret)
        public void Hat()
        {
            float tempX = this.x;
            this.x = -this.y;
            this.y = tempX;
        }

    }
}
