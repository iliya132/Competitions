using System;
using System.Linq;

namespace SecretaryRobot
{
    class Program
    {

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int len = input.Length;
            int[,] arr = new int[2, len + 1];
            arr[1, 0] = 2 * len + 2;

            for (int i = 0; i < len; i++)
            {
                char c = input[i];
                if (c >= 'a' && c <= 'z')
                {
                    arr[1, i + 1] = arr[1, i] + 1;
                    arr[0, i + 1] = Math.Min(arr[0, i], arr[1, i] + 2);
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    arr[0, i + 1] = arr[0, i] + 1;
                    arr[1, i + 1] = Math.Min(arr[1, i], arr[0, i] + 2);
                }
                else
                {
                    arr[0, i + 1] = arr[0, i];
                    arr[1, i + 1] = arr[1, i];
                }

            }
            int answer = Math.Min(arr[0, len], arr[1, len]) + len;

            Console.WriteLine(answer);
        }
    }
}
