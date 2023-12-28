namespace l1m {
    internal class Program {

        static void Main(string[] args) {

            int up = 10;
            int low = -10;

            Matrix m;
            Vector v;
            Vector r;
            Vector f;

            ReadFromFile(out m, out v);
            r = new Vector(m.Size);
            f = new Vector(m.Size);

            Methods.CalculateVectorF(m.BottomCodiagonal, m.MainDiagonal, m.TopCodiagonal, v,out f, m.Size);
            Methods.Solve(m.BottomCodiagonal, m.MainDiagonal, m.TopCodiagonal, f, out r ,m.Size);

            Console.Write("Истинное решение " + v + "\n");
            Console.Write("Полученное решение " + r + "\n");
            Console.Write("Погрешность " + Vector.Norm(v - r) + "\n");

            Console.WriteLine("\nХорошая обусловленность\n");
            for (int i = 8; i <= 2048; i = 2 * i)
                Console.WriteLine(i + " ----- " + Methods.Experiment(i, up, low, true));
            Console.WriteLine("\nПлохая обусловленность\n");
            for (int i = 8; i <= 2048; i = 2 * i)
                Console.WriteLine(i + " ----- " + Methods.Experiment(i, up, low, false));
        }

        static void ReadFromFile(out Matrix matrix, out Vector vector)
        {
            StreamReader reader = new StreamReader("example.txt");

            string[] lines = { reader.ReadLine(), reader.ReadLine(), reader.ReadLine() };
            matrix = Matrix.Parse(lines);
            vector = Vector.Parse(reader.ReadLine());
        }
    }
}
