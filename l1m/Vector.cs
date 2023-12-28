using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l1m
{
    public class Vector{
        private double[] valueArray;
        public int Length => valueArray.Length;
        public Vector(int size)
        {
            valueArray = new double[size];
            for (int i = 1; i <= Length; ++i)
                this[i] = 0;
        }
        public Vector() : this(10) { }
        public Vector(double[] other)
        {
            valueArray = new double[other.Length];
            Array.Copy(other, valueArray, Length);
        }
        public Vector(Vector other) : this(other.valueArray) { }
        public double this[int index]
        {
            get
            {
                if (index < 1 || index > Length)
                    throw new Exception("Выход за пределы вектора!");
                return valueArray[index - 1];
            }
            set
            {
                if (index < 1 || index > Length)
                    throw new Exception("Выход за пределы вектора!");
                valueArray[index - 1] = value;
            }
        }
        public static Vector operator +(Vector first, Vector second)
        {
            if (first.Length != second.Length)
                throw new Exception("Векторы разных размеров!");
            Vector resultVector = new Vector(first.Length);
            for (int i = 1; i <= resultVector.Length; ++i)
                resultVector[i] = first[i] + second[i];
            return resultVector;
        }
        public static Vector operator -(Vector first, Vector second)
        {
            if (first.Length != second.Length)
                throw new Exception("Векторы разных размеров!");
            Vector resultVector = new Vector(first.Length);
            for (int i = 1; i <= resultVector.Length; ++i)
                resultVector[i] = first[i] - second[i];
            return resultVector;
        }
        public static Vector operator *(Vector vector, double val)
        {
            Vector resultVector = vector;
            for (int i = 1; i <= resultVector.Length; ++i)
                resultVector[i] *= val;
            return resultVector;
        }
        public static Vector operator *(double val, Vector vector)
        {
            return vector * val;
        }
        public static double Norm(Vector vector)
        {
            double resultNorm = Math.Abs(vector[1]);
            for (int i = 2; i <= vector.Length; ++i)
                resultNorm = Math.Max(resultNorm, Math.Abs(vector[i]));
            return resultNorm;
        }
        static Random rand = new Random(DateTimeOffset.Now.Millisecond);
        public static Vector CreateRandVector(int length, int lowBorder, int upBorder)
        {
            Vector resultVector = new Vector(length);
            for (int i = 1; i <= length; ++i)
                resultVector[i] = rand.Next(lowBorder, upBorder);
            return resultVector;
        }
        public static Vector Parse(string str)
        {
            string[] substr = str.Split(' ');
            Vector result = new Vector();
            result.valueArray = Array.ConvertAll(substr, s => double.Parse(s));
            return result;
        }
        public Vector Clone()
        {
            return new Vector(valueArray);
        }

        public override string ToString() => string.Join(" ", valueArray);
    }
}
