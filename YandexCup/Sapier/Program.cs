using System;
using System.Collections.Generic;
using System.Linq;

namespace Sapier
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
        int width;
        int height;
        int bombsCount;
        List<Point> bombs = new List<Point>();
        int[,] gameField;
        internal void ReadInput()
        {
            var firstRow = Console.ReadLine().Split(' ');
            width = int.Parse(firstRow[0]);
            height = int.Parse(firstRow[1]);
            bombsCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < bombsCount; i++)
            {
                var coordinates = Console.ReadLine().Split(' ').Select(i => int.Parse(i)).ToList();
                bombs.Add(new Point(coordinates[0], coordinates[1]));
            }
            gameField = new int[height, width];
            foreach(Point bomb in bombs)
            {
                gameField[bomb.Y-1, bomb.X - 1] = -1;
            }
        }
        //0 - not pressed
        //1 - pressed
        //-1 - bomb
        internal void Work()
        {
            int count = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int currentCell = gameField[y, x];
                    if (currentCell == -1 || currentCell == 1)
                        continue;
                    else
                    {
                        gameField = Press(gameField, new Point(x, y));
                        count++;
                    }
                }
            }
            Console.WriteLine(count);
        }

        private int[,] Press(int[,] board, Point coordinate)
        {
            int[,] result = board;
            result[coordinate.Y, coordinate.X] = 1;
            //checkTop
            if (coordinate.Y > 0 && result[coordinate.Y - 1, coordinate.X] == 0)
                result = Press(result, new Point(coordinate.X, coordinate.Y - 1));
            //checkLeft
            if (coordinate.X > 0 && result[coordinate.Y, coordinate.X - 1] == 0)
                result = Press(result, new Point(coordinate.X - 1, coordinate.Y));
            //checkBot
            if (coordinate.Y < result.GetLength(0) - 1 && result[coordinate.Y + 1, coordinate.X] == 0)
                result = Press(result, new Point(coordinate.X, coordinate.Y + 1));
            //checkRight
            if (coordinate.X < result.GetLength(1) - 1 && result[coordinate.Y, coordinate.X + 1] == 0)
                result = Press(result, new Point(coordinate.X + 1, coordinate.Y));
            return result;
        }
    }

    internal struct Point
    {
        internal Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        internal int X { get; set; }
        internal int Y { get; set; }
    }
}
