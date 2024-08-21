namespace Random_Numbers_Generator
{
    internal class Super5
    {
        private static readonly int super5_min = 1;
        private static readonly int super5_max = 45;
        private static readonly int super5_max_drawn = 5;

        public static List<int> Play(int seed)
        {
            var r = new Random(seed);
            var nList = new List<int>();
      
            for (int i = 0; i < super5_max_drawn; i++)
            {
                int n = r.Next(super5_min, super5_max + 1);
         
                if (!nList.Contains(n))
                {
                    nList.Add(n);
                }
                else
                {
                    i--;
                }
            }

            nList.Sort();

            return nList;
        }

        public static double CalculateOdds(int numbers)
        {
            if (numbers == 0) numbers = 1;

            int n = super5_max;
            int r = super5_max_drawn;
            int k = numbers;

            double odds_t = Factorial(n) / (Factorial(r) * Factorial(n - r));
            double odds_i = Factorial(r) / (Factorial(k) * Factorial(r - k));
            double odds_j = Factorial(n - r) / (Factorial((n - r) - (r - k)) * Factorial(r - k));

            return odds_t / (odds_i * odds_j);
        }

        private static double Factorial(int n)
        {
            if (n == 0 || n == 1)
                return 1;
            else
                return n * Factorial(n - 1);
        }
    }
}
