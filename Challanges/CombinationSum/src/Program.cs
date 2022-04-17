public class Program
{
    public static int Main()
    {
        Solution solution = new Solution();
        var result = solution.CombinationSum(new int[]{1,2}, 4);

        foreach(var item in result)
        {
            foreach(int element in item)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine("");
        }

        return 0;
    }
}