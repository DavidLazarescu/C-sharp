namespace src
{
    public class Program
    {
        public static int Main()
        {
            Solution solution = new Solution();
            var result = solution.Permutation(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, 8);

            foreach (var list in result)
            {
                foreach (var elem in list)
                {
                    Console.Write(elem + " ");
                }
                Console.WriteLine("");
            }

            return 0;
        }
    }
}