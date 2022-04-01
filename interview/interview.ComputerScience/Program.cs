using System;
using System.Diagnostics;

namespace interview.ComputerScience
{
      internal class Program
      {
            static int Solution(int[] arr)
            {
                  return 0;
            }

            static void TestSolution()
            {
                  (int[], int)[] tests =
                  {
                        (new[] {9, 2, 5, 2, 1, 15}, 5),
                        (new[] {-1, -6, -3, -5, -99, -4}, -4),
                        (new[] {3, 2, 1, 5, 4}, 3),
                        (new[] {7, 2, 1, 1, 2, 3, 5, 6, 9, -1, 99, 5}, 7)
                  };

                  foreach ((int[] arr, int expected) in tests)
                  {
                        int result = Solution(arr);
                        string arrStr = $"[{string.Join(",", arr)}]";
                        Debug.Assert(result == expected, $"Test failed for array {arrStr}.");
                        Console.WriteLine($"Test for array {arrStr} has passed. Expected value: {expected}, found result: {result}");
                  }
            }
            
            static void Main(string[] args)
            {
                  TestSolution();
            }
      }
}