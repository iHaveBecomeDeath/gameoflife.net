using System;
using System.Linq;
using System.Threading;
using GoL.Entities;

namespace GoL.App
{
    class Program
    {
        const int Width = 20;
        const int Height = 20;
        static void Main()
        {
            //Set up
            CellRetainer.CleanSlate();
            CellProcessor.Initialize(Width, Height);

            //Randomize

            RandomizeStartingPositions();
            // Looooop
            var keyPressed = false;
            while (!keyPressed)
            {
                CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);
                Console.WriteLine("Current living cells: " + CellRetainer.LivingCells.Count);
                Thread.Sleep(50);
                if (Console.ReadKey(false).Key == ConsoleKey.Q)
                    keyPressed = true;
            }
            return;
        }

        private static void RandomizeStartingPositions()
        {
            // 1-10 startceller (t ex), slumpa fram om de är grannar eller inte
            var maxLivingCells = new Random().Next(1, 10);
            for (var i = 1; i < maxLivingCells; i++)
            {
                var deadCell = CellRetainer.AllCellsInExistence.SingleOrDefault(cell => cell.Coordinates.X == Width - i - 1 && cell.Coordinates.Y == Height - i - 1);
                if (deadCell != null)
                    CellRetainer.MakeCellLive(deadCell.Id);
            }
        }
    }
}
