using System;
using System.Linq;

namespace YandexCup
{
    class Program
    {
        static void Main(string[] args)
        {
            string J = Console.ReadLine();
            string S = Console.ReadLine();

            int result = S.Where(i => J.IndexOf(i) >= 0).Count();
            Console.WriteLine(result);
        }
    }
}
