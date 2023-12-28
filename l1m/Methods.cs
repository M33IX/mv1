using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace l1m
{
    public class Methods{
        public static void CalculateVectorF(Vector a, Vector b, Vector c, Vector x, out Vector f, int n) { 
            f = new Vector(n);

            f[1] = b[1] * x[1] + c[1] * x[2];
            f[n] = a[n] *x[n-1] + b[n] *x[n];

            for (int i = 2; i < n; i++)
            {
                f[i] = a[i] * x[i - 1] + b[i] * x[i] + c[i] * x[i + 1];
            }
        }
        public static void Solve(Vector a, Vector b, Vector c, Vector f, out Vector x, int n) { 
            Vector l = new Vector(n + 1);
            Vector m = new Vector(n + 1);
            
            double k;
            l[1] = 0;
            m[1] = 0;
            l[n + 1] = 0;
            l[2] = c[1] / b[1];
            m[2] = f[1] / b[1];

            for (int i = 2; i < n + 1; i++) {
                k = 1 / (b[i] - l[i] * a[i]);
                l[i + 1] = c[i] * k;
                m[i + 1] = (f[i] - a[i] * m[i]) * k;
                if (i == n)
                {
                    m[i + 1] = (f[i] - a[i] * m[i]) / (b[i] - l[i] * a[i]);
                }
            }
            x = new Vector(n);
            x[n] = m[n + 1];
            for (int i = n - 1; i > 0; i--)
            {
                x[i] = m[i + 1] - l[i + 1] * x[i + 1];
            }
        }
        public static double Experiment(int size, int upperBound, int lowerBound, bool type) {
            Vector v = new Vector(size);
            Vector f = new Vector(size);
            Vector x = new Vector(size);
            Matrix m;
            if (!type) m = Matrix.CreateRandomPoorlyConditionMatrix(size, lowerBound, upperBound, out v);
            else m = Matrix.CreateRandomWellConditionMatrix(size, lowerBound, upperBound, out v);
            Methods.CalculateVectorF(m.BottomCodiagonal, m.MainDiagonal, m.TopCodiagonal, v, out f, size);
            Methods.Solve(m.BottomCodiagonal, m.MainDiagonal, m.TopCodiagonal, f, out x, size);
            return Vector.Norm(v - x);
        }
    }
}
