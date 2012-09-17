using System;
using System.Collections.Generic;
using System.Linq;
using GoL.Entities;

namespace GoL.App
{
    public class CellRetainer
    {
        private static List<Cell> _cells;

        public static List<Cell> Cells
        {
            get { return _cells ?? (_cells = new List<Cell>()); }
        }

        public static Cell CreateCell()
        {
            return new Cell(Guid.NewGuid());
        }

        public static void MakeNeighbours(Cell firstCell, Cell secondCell)
        {
            if (firstCell.IsNeighbourWith(secondCell)) return;
            firstCell.Neighbours.Add(secondCell.Id);
            secondCell.Neighbours.Add(firstCell.Id);
        }

    }
}