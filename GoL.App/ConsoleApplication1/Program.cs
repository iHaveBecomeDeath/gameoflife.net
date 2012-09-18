using System;
using GoL.Entities;

namespace GoL.App
{
    class Program
    {
        const int Width = 20;
        const int Height = 20;
        static void Main(string[] args)
        {
            //Set up
            CellRetainer.CleanSlate();
            CellProcessor.Initialize(Width, Height);

            //Randomize

            RandomizeStartingPositions();
            // Looooop
            while(true)
            {
                CellProcessor.Iterate(CellRetainer.LivingCells, CellRetainer.AllCellsInExistence);
                Console.WriteLine("Current living cells: " + CellRetainer.LivingCells.Count);
            }
            // While(true) Processor.CalculateTransitions(),  Processor.Iterate()
        }

        private static void RandomizeStartingPositions()
        {
            // 1-10 startceller (t ex), slumpa fram om de är grannar eller inte
            for (var i = 0; i < new Random().Next(1, 10); i++)
            {
                var newCellId = Guid.NewGuid();
                CellRetainer
                .AddCell(
                    new Cell(
                        newCellId,
                        Width - i,
                        Height - i
                    )
                );
                CellRetainer.MakeCellLive(newCellId);
            }
        }
    }
}
