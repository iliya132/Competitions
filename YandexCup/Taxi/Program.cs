using System;
using System.Collections.Generic;
using System.Linq;

namespace Taxi
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            worker.ReadInput();
            worker.Work();
        }
    }

    internal class Worker
    {
        List<int> ratings = new List<int>();
        List<int> bonuses = new List<int>();

        internal void ReadInput()
        {
            int count = int.Parse(Console.ReadLine());
            for (int i = 0; i < count; i++)
            {
                ratings.Add(int.Parse(Console.ReadLine()));
                bonuses.Add(1);
            }
        }

        internal void Work()
        {
            List<int> ratingsSorted = ratings.Distinct().OrderBy(i=>i).ToList();
            for (int i = 0; i < ratingsSorted.Count(); i++)
            {
                int curMin = ratingsSorted[i];
                for (int j = 0; j < ratings.Count(); j++)
                {
                    int curRating = ratings[j];

                    if (curMin == curRating)
                    {
                        //Если слева меньше рейтинг
                        if(j > 0 && ratings[j - 1] < curRating)
                        {
                            //Если бонус слева больше текущего
                            if(bonuses[j-1] >= bonuses[j])
                            {
                                bonuses[j] = bonuses[j - 1] + 1;
                            }
                        }
                        //если справа больше рейтинг
                        if (j < ratings.Count() - 1 && ratings[j + 1] < curRating)
                        {
                            //Если бонус справа больше текущего
                            if (bonuses[j + 1] >= bonuses[j])
                            {
                                bonuses[j] = bonuses[j + 1] + 1;
                            }
                        }
                    }
                }
            }
            Console.WriteLine(bonuses.Sum()*500);
        }
    }
}
