using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l1m
{
    public class Matrix {
        public Vector TopCodiagonal { get; private set; }
        public Vector MainDiagonal { get; private set; }
        public Vector BottomCodiagonal { get; private set; }

        public int Size => MainDiagonal.Length;

        public Matrix(Vector topDiagonal, Vector diagonal, Vector bottomDiagonal)
        {
            MainDiagonal = diagonal.Clone();
            TopCodiagonal = topDiagonal.Clone();
            BottomCodiagonal = bottomDiagonal.Clone();

            TopCodiagonal[Size] = 0;
            BottomCodiagonal[1] = 0;
        }
        public static Matrix operator +(Matrix first, Matrix second)
        {
            if (first.Size != second.Size)
                throw new Exception("Веторы разных длин");
            return new Matrix(
             first.TopCodiagonal + second.TopCodiagonal,
             first.MainDiagonal + second.MainDiagonal,
             first.BottomCodiagonal + first.BottomCodiagonal
             );
        }
        public static Matrix operator -(Matrix first, Matrix second)
        {
            if (first.Size != second.Size)
                throw new Exception("Веторы разных длин");
            return new Matrix(
             first.TopCodiagonal - second.TopCodiagonal,
             first.MainDiagonal - second.MainDiagonal,
             first.BottomCodiagonal - first.BottomCodiagonal
             );
        }
        public static Vector operator *(Matrix matrix, Vector vector)
        {
            if (vector.Length != matrix.Size)
            {
                throw new Exception("Веторы разных длин");
            }
            Vector result = new Vector(vector.Length);
            result[1] = matrix.TopCodiagonal[1] * vector[2]
                + matrix.MainDiagonal[1] * vector[1];
            for (int i = 2; i < matrix.Size; i++)
            {
                result[i] = matrix.TopCodiagonal[i] * vector[i + 1]
                + matrix.MainDiagonal[i] * vector[i]
                + matrix.BottomCodiagonal[i] * vector[i - 1];
            }
            result[matrix.Size] = matrix.MainDiagonal[matrix.Size] * vector[matrix.Size]
                + matrix.BottomCodiagonal[matrix.Size] * vector[matrix.Size - 1];
            return result;
        }
        public static Matrix CreateRandomMatrix(int size, int lowBorder, int upBorder)
        {
            Vector topDiagonal = Vector.CreateRandVector(size, lowBorder, upBorder);
            Vector mainDiagonal = Vector.CreateRandVector(size, lowBorder, upBorder);
            Vector bottomDiagonal = Vector.CreateRandVector(size, lowBorder, upBorder);
            topDiagonal[1] = 0;
            bottomDiagonal[size] = 0;
            return new Matrix(topDiagonal, mainDiagonal, bottomDiagonal);
        }
        public static Matrix Parse(string[] lines)
        {
            if (lines.Length != 3)
            {
                throw new FormatException();
            }
            Vector top = Vector.Parse(lines[0]+" 0");
            Vector main = Vector.Parse(lines[1]);
            Vector bottom = Vector.Parse("0 "+lines[2]);
            return new Matrix(top, main, bottom);
        }
        static Random rand = new Random(DateTimeOffset.Now.Millisecond);
        public static Matrix CreateRandomWellConditionMatrix(int size, int lowBorder, int upBorder, out Vector x)
        {
            rand = new Random();
            Vector mainDiagonal = new Vector(size);
            Vector topDiagonal = new Vector(size);
            Vector bottomDiagonal = new Vector(size);
            x = new Vector(size);
            for (int i = 1; i <= size; i++)
            {
                mainDiagonal[i] = rand.NextDouble() * ((2 * upBorder + lowBorder) - 2 * upBorder) + 2 * upBorder;
                topDiagonal[i] = rand.NextDouble() * (upBorder - lowBorder) + lowBorder;
                bottomDiagonal[i] = rand.NextDouble() * (upBorder - lowBorder) + lowBorder;
                x[i] = rand.NextDouble() * 2 - 1;
            }
            topDiagonal[size] = 0;
            bottomDiagonal[1] = 0;

            return new Matrix(topDiagonal, mainDiagonal, bottomDiagonal);
        }
        public static Matrix CreateRandomPoorlyConditionMatrix(int size, int lowBorder, int upBorder, out Vector x)
        {
            rand = new Random();
            Vector mainDiagonal = new Vector(size);
            Vector topDiagonal = new Vector(size);
            Vector bottomDiagonal = new Vector(size);
            x = new Vector(size);
            for (int i = 1; i <= size; i++)
            {
                mainDiagonal[i] = rand.NextDouble() * (lowBorder / 2 - lowBorder / 4) + lowBorder / 4;
                topDiagonal[i] = rand.NextDouble() * (upBorder - lowBorder) + lowBorder;
                bottomDiagonal[i] = rand.NextDouble() * (upBorder - lowBorder) + lowBorder;
                x[i] = rand.NextDouble() * 2 - 1;
            }
            topDiagonal[size] = 0;
            bottomDiagonal[1] = 0;
            return new Matrix(topDiagonal, mainDiagonal, bottomDiagonal);
        }

        public override string ToString() => BottomCodiagonal + "\n" + MainDiagonal + "\n" + TopCodiagonal;
    }
}
