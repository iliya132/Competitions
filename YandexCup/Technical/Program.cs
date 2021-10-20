using System;
using System.Collections.Generic;
using System.Linq;

namespace Technical
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            worker.ReadInput();
            worker.Work();
        }

        internal class Worker
        {
            private int count;
            private int X;
            private int K;
            private int[] deadlines;
            private int currentDay;
            private List<int> robotCame = new List<int>();
            internal void ReadInput()
            {
                string[] firstRow = Console.ReadLine().Split(' ');
                string[] secondRow = Console.ReadLine().Split(' ');
                count = int.Parse(firstRow[0]);
                X = int.Parse(firstRow[1]);
                K = int.Parse(firstRow[2]);
                deadlines = secondRow.Select(i => int.Parse(i)).ToArray();
            }
            private HashSet<int> notificated = new HashSet<int>();

            internal void Work()
            {
                int result = 0;
                
                for (int i = 0; i < K - 1; i++)
                {
                    currentDay = deadlines.Min();
                    result = CalculateNextNotification();
                }
                Console.WriteLine(result);
            }

            private int CalculateNextNotification()
            {
                for (int i = 0; i < deadlines.Length; i++)
                {
                    if(deadlines[i] <= currentDay)
                    {
                        if (!notificated.Contains(deadlines[i]))
                        {
                            notificated.Add(deadlines[i]);
                            deadlines[i] += X;
                            return deadlines[i];
                        }
                        else
                        {
                            deadlines[i] += X;
                        }
                    }
                }
                throw new InvalidOperationException();
            }
        }
    }
}
