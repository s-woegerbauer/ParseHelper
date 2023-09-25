using System.Diagnostics;

namespace ParseHelper
{
    internal class Program
    {
        static void Main()
        {
            int[] result00 = ParseHelper.Simple.LinesToOneDimensionalArray<int>(@"D:\ParseHelper\ParseHelper\test00.txt");
            
            foreach(int value in result00)
            {
                Console.WriteLine(value);
            }

            int[,] result01 = ParseHelper.Simple.LinesToTwoDimensionalArray<int>(@"D:\ParseHelper\ParseHelper\test01.txt", ',');

            for(int i = 0; i < result01.GetLength(0); i++)
            {
                Console.WriteLine();

                for (int j = 0; j < result01.GetLength(1); j++)
                {
                    if(j != 0)
                    {
                        Console.Write(",");
                    }
                    Console.Write(result01[i,j]);
                }
            }

            Console.WriteLine();
        }
    }
}