using System;
using System.Linq;
using GoL.Entities;

namespace GoL.App
{
    class Program
    {
        const int Width = 120;
        const int Height = 50;
        const int MaxLivingCells = 300;
        private const int MinLivingCells = 3;

        static void Main()
        {
            //Set up
            Console.WindowHeight = Height + 1;
            Console.WindowWidth = Width + 1;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Reset();
            // Looooop
            var exitKeyPressed = false;
            while (!exitKeyPressed)
            {
                //Console.WriteLine("Current living cells: " + CellRetainer.LivingCells.Count);
                DisplayResults();
                //Thread.Sleep(50);
                var consoleKey = Console.ReadKey(false).Key;
                switch (consoleKey)
                {
                    case ConsoleKey.Q:
                        exitKeyPressed = true;
                        break;
                    case ConsoleKey.R:
                        Reset();
                        break;
                }
                CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);
            }
        }

        private static void Reset()
        {
            CellRetainer.CleanSlate();
            CellProcessor.Initialize(Width, Height);
            RandomizeStartingPositions();
        }

        private static void DisplayResults()
        {
            Console.Clear();
            var results = new char[Height, Width];
            foreach (var cell in CellRetainer.AllCellsInExistence)
            {
                results[cell.Coordinates.Y, cell.Coordinates.X]
                    = cell.CurrentState == CellState.Alive
                          ? 'X'
                          : '.';
            }

            for (var i = 0; i < results.GetLength(0); i++)
            {
                for (var j =0;j<results.GetLength(1);j++)
                    Console.Write(results[i,j]);
                Console.WriteLine();
            }
        }

        private static void RandomizeStartingPositions()
        {
            var random = new Random();
            var maxLivingCells = random.Next(MinLivingCells, MaxLivingCells);
            for (var i = 1; i < maxLivingCells; i++)
            {
                var randX = random.Next(0, Width);
                var randY = random.Next(0, Height);
                var deadCell = 
                    CellRetainer
                    .AllCellsInExistence
                    .SingleOrDefault(
                        cell => 
                            cell.Coordinates.X == randX && cell.Coordinates.Y == randY);
                if (deadCell != null)
                    CellRetainer.MakeCellLive(deadCell.Id);
            }
        }

    }
}
