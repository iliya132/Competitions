using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DateIntervals.A
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution solution = new Solution();
            solution.ReadInput();
            solution.CalculateDateIntervals();
            using (StreamWriter sw = new StreamWriter("output.txt", false))
            {
                sw.WriteLine(solution.IntervalsCount);
                sw.Write(string.Join("\r\n", solution.Intervals.Select(i => $"{i.Key:yyyy-MM-dd} {i.Value:yyyy-MM-dd}")));
                sw.Close();
            }
        }
    }

    public class Solution
    {
        string IntervalType;
        DateTime StartDate;
        DateTime EndDate;
        bool isCalculated = false;

        Dictionary<DateTime, DateTime> _intervals = new Dictionary<DateTime, DateTime>();
        public Dictionary<DateTime, DateTime> Intervals => _intervals;
        public int IntervalsCount
        {
            get
            {
                if (!isCalculated)
                    throw new InvalidOperationException();
                return _intervals.Count;
            }
        }

        public void ReadInput()
        {
            IntervalType = Console.ReadLine();
            string[] Dates = Console.ReadLine().Split(' ');
            StartDate = DateTime.Parse(Dates[0]);
            EndDate = DateTime.Parse(Dates[1]);
        }

        public void CalculateDateIntervals()
        {
            switch (IntervalType)
            {
                case ("WEEK"):
                    Calculate(i => WeekPredicate(i));
                    break;
                case ("MONTH"):
                    Calculate(i => MonthPredicate(i));
                    break;
                case ("QUARTER"):
                    Calculate(i => QuarterPredicate(i));
                    break;
                case ("YEAR"):
                    Calculate(i => YearPredicate(i));
                    break;
                case ("FRIDAY_THE_13TH"):
                    Calculate(i => FridayThe13Predicate(i));
                    break;
                default:
                    throw new InvalidOperationException();
            }
            isCalculated = true;
        }

        private void Calculate(Func<DateTime,bool> stopRule)
        {
            DateTime startPointer = StartDate;
            DateTime endPointer = startPointer;
            while(endPointer <= EndDate)
            {
                if (stopRule(endPointer) || endPointer == EndDate)
                {
                    _intervals.Add(startPointer, endPointer);
                    startPointer = endPointer.AddDays(1);
                }
                endPointer = endPointer.AddDays(1);
            }
        }

        private bool WeekPredicate(DateTime date) => date.DayOfWeek == DayOfWeek.Sunday;

        private bool MonthPredicate(DateTime date) => IsMonthEnd(date);

        private bool QuarterPredicate(DateTime date)=> IsMonthEnd(date) && date.Month % 3 == 0;

        private bool IsMonthEnd(DateTime date) => date.AddDays(1).Month != date.Month;

        private bool YearPredicate(DateTime date) => date.AddDays(1).Year > date.Year;

        private bool FridayThe13Predicate(DateTime date)=> date.Day == 12 && date.DayOfWeek == DayOfWeek.Thursday;
    }
}


